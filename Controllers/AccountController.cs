using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;                 // ← ADDED
using EcommerceWebSite.Identity;
using EcommerceWebSite.Models;
using EcommerceWebSite.Entity;                      // ← ADDED (Orders & EnumOrderState)

namespace EcommerceWebSite.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<ApplicationRole> _roleManager;

        private readonly DataContext _context;       // ← ADDED

        public AccountController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            RoleManager<ApplicationRole> roleManager,
            DataContext context)                     // ← ADDED
        {
            _userManager   = userManager;
            _signInManager = signInManager;
            _roleManager   = roleManager;
            _context       = context;                // ← ADDED
        }

        // ---------- /Account (Sipariş Bilgileriniz) ----------
        [Authorize]                                   // ← ADDED
        [HttpGet]                                     // ← ADDED
        public async Task<IActionResult> Index()      // ← ADDED
        {
            var username = User!.Identity!.Name!;
            var orders = await _context.Orders
                .AsNoTracking()
                .Where(o => o.Username == username)
                .OrderByDescending(o => o.OrderDate)
                .Select(o => new UserOrderModel
                {
                    Id          = o.Id,
                    OrderNumber = o.OrderNumber,
                    Total       = o.Total,          // double
                    OrderState  = o.OrderState,     // EnumOrderState
                    OrderDate   = o.OrderDate
                })
                .ToListAsync();

            return View(orders); // Views/Account/Index.cshtml
        }

        // ---------- /Account/Details/{id} ----------
        [Authorize]                                   // ← ADDED
        [HttpGet]                                     // ← ADDED
        public async Task<IActionResult> Details(int id) // ← ADDED
        {
            var username = User!.Identity!.Name!;
            var entity = await _context.Orders
                .AsNoTracking()
                .Where(o => o.Id == id && o.Username == username)
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
            return View(entity); // Views/Account/Details.cshtml
        }

        // ---------- REGISTER ----------
        [HttpGet]
        public IActionResult Register() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(Register model, string? returnUrl = null)
        {
            if (!ModelState.IsValid) return View(model);

            var user = new ApplicationUser
            {
                Name     = model.Name,
                Surname  = model.Surname,   // <-- Surname
                Email    = model.Email,
                UserName = model.Username   // <-- Username
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                if (await _roleManager.RoleExistsAsync("user"))
                    await _userManager.AddToRoleAsync(user, "user");

                return LocalRedirectSafe(returnUrl) ?? RedirectToAction(nameof(Login));
            }

            foreach (var err in result.Errors)
                ModelState.AddModelError(string.Empty, err.Description);

            return View(model);
        }

        // ---------- LOGIN ----------
        [HttpGet]
        public IActionResult Login(string? returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(Login model, string? returnUrl = null)
        {
            if (!ModelState.IsValid) return View(model);

            var user = await _userManager.FindByNameAsync(model.UserName);
            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Kullanıcı adı veya şifre hatalı. (user not found)");
                return View(model);
            }

            var passwordOk = await _userManager.CheckPasswordAsync(user, model.Password);
            if (!passwordOk)
            {
                ModelState.AddModelError(string.Empty, "Kullanıcı adı veya şifre hatalı. (wrong password)");
                return View(model);
            }

            var result = await _signInManager.PasswordSignInAsync(
                userName: model.UserName,
                password: model.Password,
                isPersistent: model.RememberMe,
                lockoutOnFailure: false);

            if (result.Succeeded)
                return LocalRedirectSafe(returnUrl) ?? RedirectToAction("Index", "Home");

            if (result.IsLockedOut)
                ModelState.AddModelError(string.Empty, "Hesabınız kilitlendi.");
            else if (result.IsNotAllowed)
                ModelState.AddModelError(string.Empty, "Girişe izin verilmiyor (e-posta onayı gerekebilir).");
            else
                ModelState.AddModelError(string.Empty, "Kullanıcı adı veya şifre hatalı. (sign-in failed)");

            return View(model);
        }

        // ---------- LOGOUT ----------
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        // Helper
        private IActionResult? LocalRedirectSafe(string? returnUrl)
        {
            if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                return LocalRedirect(returnUrl);
            return null;
        }
    }
}
