// Components/CategoriesViewComponent.cs
using EcommerceWebSite.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class CategoriesViewComponent : ViewComponent
{
    private readonly DataContext _context;
    public CategoriesViewComponent(DataContext context) => _context = context;

    public IViewComponentResult Invoke(int? selectedId)
    {
        var cats = _context.Categories.AsNoTracking().ToList();
        ViewBag.SelectedId = selectedId;
        return View(cats); // -> Views/Shared/Components/Categories/Default.cshtml
    }
}
