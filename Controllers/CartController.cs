using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using EcommerceWebSite.Entity;
using EcommerceWebSite.Models;
using Microsoft.AspNetCore.Authorization; // Cart & CartLine burada

namespace EcommerceWebSite.Controllers
{
    public class CartController : Controller
    {
        private readonly DataContext _context;
        private const string CartSessionKey = "CART";

        public CartController(DataContext context)
        {
            _context = context;
        }

        // GET /Cart
        public IActionResult Index()
        {
            return View(GetCart());
        }

        // POST /Cart/AddToCart
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddToCart(int id, int quantity = 1)
        {
            var product = _context.Products.FirstOrDefault(p => p.Id == id);
            if (product == null) return NotFound();

            var cart = GetCart();
            cart.AddProduct(product, quantity);

            SaveCart(cart);   // ← ASIL KRİTİK NOKTA: Session’a yazıyoruz
            return RedirectToAction(nameof(Index));
        }

        // POST /Cart/RemoveFromCart
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RemoveFromCart(int id)
        {
            var product = _context.Products.FirstOrDefault(p => p.Id == id);
            if (product != null)
            {
                var cart = GetCart();
                cart.DeleteProduct(product);
                SaveCart(cart);  // ← silince de kaydet
            }
            return RedirectToAction(nameof(Index));
        }

        // (Navbar’daki küçük sepet özeti için)
        public PartialViewResult Summary()
        {
            return PartialView(GetCart());
        }

        // ---------- helpers ----------


        private void SaveCart(Cart cart)
        {
            var json = JsonSerializer.Serialize(cart);
            HttpContext.Session.SetString(CartSessionKey, json);
        }

        private Cart GetCart()
        {
            var json = HttpContext.Session.GetString(CartSessionKey);
            return string.IsNullOrEmpty(json)
                ? new Cart()
                : (JsonSerializer.Deserialize<Cart>(json) ?? new Cart());
        }

        public IActionResult CartSummary()
        {
            var cart = GetCart(); // Session'dan veya DB'den cart objesi al
            return PartialView(cart);
        }

        public IActionResult Checkout()
        {
            return View(new ShippingDetails());
        }

        [HttpPost]
        public ActionResult Checkout(ShippingDetails entity)
        {
            var cart = GetCart();

            if (cart.CartLines.Count == 0)
            {
                ModelState.AddModelError("UrunYokError", "Sepetinizde ürün bulunmamaktadır.");
            }

            if (ModelState.IsValid)
            {
                SaveOrder(cart, entity);
                cart.Clear();
                return View("Completed");
            }
            else
            {
                return View(entity);
            }
        }

        private void SaveOrder(Cart cart, ShippingDetails entity)
        {
             var order = new Order
            {
                OrderNumber = "A" + new Random().Next(11111, 99999).ToString(),
                Total       = (double)cart.Total(),                 // decimal -> double
                OrderDate   = DateTime.Now,
                OrderState  = EnumOrderState.Waiting,
                Username    = User?.Identity?.Name ?? "anonymous",

                AdresBasligi = entity.AdresBasligi,
                Adres        = entity.Adres,
                Sehir        = entity.Sehir,
                Semt         = entity.Semt,
                Mahalle      = entity.Mahalle,
                PostaKodu    = entity.PostaKodu,

                Orderlines = new List<OrderLine>()
            };

            foreach (var pr in cart.CartLines)
            {
                var orderline = new OrderLine
                {
                    Quantity  = pr.Quantity,
                    // Birim fiyatı kaydediyoruz; satır toplamını raporda Quantity*Price hesaplayabilirsin
                    Price     = (double)pr.Product.Price,           // decimal -> double
                    ProductId = pr.ProductId                        // CartLine'da ProductId var
                };

                order.Orderlines.Add(orderline);

            }
            _context.Add(order);   // db değil, _context
            _context.SaveChanges();

        }
    }

}
