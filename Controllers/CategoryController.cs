using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjektDT191G.Data;
using ProjektDT191G.Models;

namespace ProjektDT191G.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CategoryController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Category
        [Authorize(Roles = "Administrator, Speaker")]   // Admin och Speaker ska komma åt get
        public async Task<IActionResult> Index()
        {
            // Kontroll om null-värde
            if (_context.Categories == null)
            {
                return NotFound();
            }

            return View(await _context.Categories.ToListAsync());
        }

        // GET: Category/Details/5
        [Authorize(Roles = "Administrator, Speaker")]   // Admin och Speaker ska komma åt detaljer
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Kontroll om null-värde
            if (_context.Categories == null)
            {
                return NotFound();
            }

            var category = await _context.Categories
                .FirstOrDefaultAsync(m => m.CategoryId == id);

            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // GET: Category/Create
        [Authorize(Roles = "Administrator")]   // Admin ska ha tillgång
        public IActionResult Create()
        {
            return View();
        }

        // POST: Category/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]   // Admin ska ha tillgång
        public async Task<IActionResult> Create([Bind("CategoryId,Name,Description")] Category category)
        {
            if (ModelState.IsValid)
            {
                _context.Add(category);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        // GET: Category/Edit/5
        [Authorize(Roles = "Administrator")]   // Admin ska ha tillgång
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Kontroll om null-värde
            if (_context.Categories == null)
            {
                return NotFound();
            }

            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        // POST: Category/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]   // Admin ska ha tillgång
        public async Task<IActionResult> Edit(int id, [Bind("CategoryId,Name,Description")] Category category)
        {
            if (id != category.CategoryId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(category);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryExists(category.CategoryId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        // GET: Category/Delete/5
        [Authorize(Roles = "Administrator")]   // Admin ska ha tillgång
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Kontroll om null-värde
            if (_context.Categories == null)
            {
                return NotFound();
            }

            var category = await _context.Categories
                .FirstOrDefaultAsync(m => m.CategoryId == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // POST: Category/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]   // Admin ska ha tillgång
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            // Kontroll om null-värde
            if (_context.Categories == null)
            {
                return NotFound();
            }

            var category = await _context.Categories
            .Include(c => c.Lectures)                       // Inkludera föreläsningar för att kunna kolla relationen
            .FirstOrDefaultAsync(c => c.CategoryId == id);


            if (category == null)
            {
                return NotFound();
            }

            // Kontrollera om kategorin har föreläsningar knutna till sig
            if (category.Lectures != null && category.Lectures.Count != 0)
            {
                // Meddelande vid relation till lecture, ska ej gå att radera
                TempData["ErrorMessage"] = "The category cannot be deleted because it has lectures associated with it!";

                return View(category);  // Stanna kvar på delete-sidan
            }

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        private bool CategoryExists(int id)
        {
            // Kontroll om null-värde
            if (_context.Categories == null)
            {
                return false;
            }

            return _context.Categories.Any(e => e.CategoryId == id);
        }
    }
}
