using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EcommerceWebSite.Entity;
using EcommerceWebSite.Models;   // AdminOrderModel, OrderDetailsModel, OrderLineModel

namespace EcommerceWebSite.Controllers
{
    [Authorize(Roles = "admin")]
    public class OrderController : Controller
    {
        private readonly DataContext _context;

        public OrderController(DataContext context)
        {
            _context = context;
        }
        // GET: /Order
        public async Task<IActionResult> Index()
        {
            var orders = await _context.Orders
                .AsNoTracking()
                .Select(i => new AdminOrderModel
                {
                    Id          = i.Id,
                    OrderNumber = i.OrderNumber,
                    OrderDate   = i.OrderDate,
                    OrderState  = i.OrderState,
                    Total       = i.Total,
                    Count       = i.Orderlines.Count
                })
                .OrderByDescending(i => i.OrderDate)
                .ToListAsync();

            return View(orders);
        }

        // GET: /Order/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var entity = await _context.Orders
                .AsNoTracking()
                .Where(i => i.Id == id)
                .Select(i => new OrderDetailsModel
                {
                    OrderId     = i.Id,
                    UserName    = i.Username,
                    OrderNumber = i.OrderNumber,
                    Total       = i.Total,
                    OrderDate   = i.OrderDate,
                    OrderState  = i.OrderState,
                    AdresBasligi= i.AdresBasligi,
                    Adres       = i.Adres,
                    Sehir       = i.Sehir,
                    Semt        = i.Semt,
                    Mahalle     = i.Mahalle,
                    PostaKodu   = i.PostaKodu,
                    Orderlines  = i.Orderlines.Select(a => new OrderLineModel
                    {
                        ProductId   = a.ProductId,
                        ProductName = a.Product.Name.Length > 50
                                      ? a.Product.Name.Substring(0, 47) + "..."
                                      : a.Product.Name,
                        Image       = a.Product.Image,
                        Quantity    = a.Quantity,
                        Price       = a.Price
                    }).ToList()
                })
                .FirstOrDefaultAsync();

            if (entity == null) return NotFound();
            return View(entity);
        }

        // POST: /Order/UpdateOrderState
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateOrderState(int orderId, EnumOrderState orderState)
        {
            var order = await _context.Orders.FirstOrDefaultAsync(i => i.Id == orderId);
            if (order == null) return RedirectToAction(nameof(Index));

            order.OrderState = orderState;
            await _context.SaveChangesAsync();

            TempData["message"] = "Bilgileriniz kayıt edildi.";
            return RedirectToAction(nameof(Details), new { id = orderId });
        }
    }
}
