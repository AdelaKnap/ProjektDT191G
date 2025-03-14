using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjektDT191G.Models;
using ProjektDT191G.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ProjektDT191G.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        // Slå ihop så att både ILogger och ApplicationDbContext injiceras
        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index(int? categoryId)
        {
            if (_context.Categories == null)
            {
                return NotFound();
            }

            // Hämta alla kategorier och sortera dem
            var categories = await _context.Categories
                .OrderBy(c => c.Name)
                .ToListAsync();

            // Lägg till en "Show All"-kategori högst upp
            categories.Insert(0, new Category { CategoryId = 0, Name = "Show All" });

            // Skicka kategorier och vald kategori till vyn
            ViewData["Categories"] = new SelectList(categories, "CategoryId", "Name", categoryId);
            ViewData["SelectedCategory"] = categoryId?.ToString() ?? "0"; // Standardvärde "Show All"

            if (_context.Lectures == null)
            {
                return NotFound();
            }

            // Hämta föreläsningar och filtrera om en kategori valts
            var lectures = _context.Lectures.Include(l => l.Category).AsQueryable();

            if (categoryId.HasValue && categoryId > 0)
            {
                lectures = lectures.Where(l => l.CategoryId == categoryId);
            }

            return View(await lectures.ToListAsync());
        }

        // About page
        [Authorize]
        public IActionResult About()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
