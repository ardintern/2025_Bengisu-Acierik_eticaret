using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EcommerceWebSite.Entity;

namespace EcommerceWebSite.Controllers
{
    // videodaki davranışla aynı: sadece admin rolü erişir
    [Authorize(Roles = "admin")]
    // tüm yazma işlemlerinde antiforgery otomatik kontrol edilsin
    [AutoValidateAntiforgeryToken]
    public class CategoryController : Controller
    {
        private readonly DataContext _context;

        public CategoryController(DataContext context)
        {
            _context = context;
        }

        // GET: Category
        public async Task<IActionResult> Index()
        {
            var list = await _context.Categories
                                     .OrderBy(c => c.Name)
                                     .ToListAsync();
            return View(list);
        }

        // GET: Category/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id is null) return BadRequest();

            var category = await _context.Categories
                                         .AsNoTracking()
                                         .FirstOrDefaultAsync(m => m.Id == id);
            if (category is null) return NotFound();

            return View(category);
        }

        // GET: Category/Create
        public IActionResult Create() => View();

        // POST: Category/Create
        [HttpPost]
        public async Task<IActionResult> Create([Bind("Id,Name,Description")] Category category)
        {
            if (!ModelState.IsValid) return View(category);

            _context.Add(category);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // GET: Category/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null) return BadRequest();

            var category = await _context.Categories.FindAsync(id);
            if (category is null) return NotFound();

            return View(category);
        }

        // POST: Category/Edit/5
        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description")] Category category)
        {
            if (id != category.Id) return BadRequest();
            if (!ModelState.IsValid) return View(category);

            try
            {
                _context.Update(category); // mvc5’teki db.Entry(...).State = Modified karşılığı
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _context.Categories.AnyAsync(e => e.Id == id))
                    return NotFound();

                throw; // başka eşzamanlılık hatasını üst katmana fırlat
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: Category/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null) return BadRequest();

            var category = await _context.Categories
                                         .AsNoTracking()
                                         .FirstOrDefaultAsync(m => m.Id == id);
            if (category is null) return NotFound();

            return View(category);
        }

        // POST: Category/Delete/5
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category != null)
            {
                _context.Categories.Remove(category);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}

