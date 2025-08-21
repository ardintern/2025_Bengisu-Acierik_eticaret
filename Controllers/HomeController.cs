using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using EcommerceWebSite.Models;
using EcommerceWebSite.Entity;
using Microsoft.EntityFrameworkCore;
using System.Linq;



namespace EcommerceWebSite.Controllers;

public class HomeController : Controller
{
   private readonly ILogger<HomeController> _logger;
        private readonly DataContext _context;

        public HomeController(ILogger<HomeController> logger, DataContext context)
        {
            _logger = logger;
            _context = context;  
        }

    public IActionResult Index()
    {
            var urunler = _context.Products
                .Where(i => i.IsHome && i.IsApproved)
                .Select(i => new ProductModel()
                {
                    Id = i.Id,
                    Name = i.Name.Length > 50 ? i.Name.Substring(0, 47) + "..." : i.Name,
                    Description = i.Description.Length > 50 ? i.Description.Substring(0, 47) + "..." : i.Description,
                    Price = i.Price,
                    Stock = i.Stock,
                    Image = i.Image,
                    CategoryId = i.CategoryId
                }).AsNoTracking().ToList();

            return View(urunler);
    
    }
    public IActionResult Details(int id)
    {
        return View(_context.Products.Where(i=> i.Id== id).AsNoTracking().ToList().FirstOrDefault());
    }

    public IActionResult List(int? id)
    {
        IQueryable<ProductModel> urunler = _context.Products
            .Where(i => i.IsApproved)
            .Select(i => new ProductModel
            {
                Id = i.Id,
                Name = i.Name.Length > 50 ? i.Name.Substring(0, 47) + "..." : i.Name,
                Description = i.Description.Length > 50 ? i.Description.Substring(0, 47) + "..." : i.Description,
                Price = i.Price,
                Stock = i.Stock,
                Image = i.Image ?? "1.jpg",
                CategoryId = i.CategoryId
            });

        if (id.HasValue)
            urunler = urunler.Where(i => i.CategoryId == id.Value);

        var model = urunler.AsNoTracking().ToList();
        return View(model);   // List.cshtml -> @model IEnumerable<EcommerceWebSite.Models.ProductModel>
    }

    public PartialViewResult GetCategories()
        {
            return PartialView(_context.Categories.ToList());
        }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
