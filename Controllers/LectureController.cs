using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjektDT191G.Data;
using ProjektDT191G.Models;

namespace ProjektDT191G.Controllers
{
    public class LectureController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LectureController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Lecture
        [Authorize(Roles = "Administrator, Speaker")]   // Admin och Speaker ska komma åt get
        public async Task<IActionResult> Index()
        {
            // Kontroll om null-värde
            if (_context.Lectures == null)
            {
                return NotFound();
            }

            var applicationDbContext = _context.Lectures.Include(l => l.Category);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Lecture/Details/5
        [Authorize(Roles = "Administrator, Speaker")]   // Admin och Speaker ska komma åt get
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Kontroll om null-värde
            if (_context.Lectures == null)
            {
                return NotFound();
            }

            var lecture = await _context.Lectures
                .Include(l => l.Category)
                .FirstOrDefaultAsync(m => m.LectureId == id);
            if (lecture == null)
            {
                return NotFound();
            }

            return View(lecture);
        }

        // GET: Lecture/Create
        [Authorize(Roles = "Administrator")]   // Endast admin har åtkomst
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "Name");
            return View();
        }

        // POST: Lecture/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]   // Endast admin har åtkomst
        public async Task<IActionResult> Create([Bind("LectureId,Name,Description,Price,CategoryId")] Lecture lecture)
        {
            if (ModelState.IsValid)
            {
                _context.Add(lecture);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "Name", lecture.CategoryId);
            return View(lecture);
        }


        // GET: Lecture/Edit/5
        [Authorize(Roles = "Administrator")]   // Endast admin har åtkomst
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Kontroll om null-värde
            if (_context.Lectures == null)
            {
                return NotFound();
            }

            var lecture = await _context.Lectures.FindAsync(id);
            if (lecture == null)
            {
                return NotFound();
            }

            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "Name", lecture.CategoryId);

            return View(lecture);
        }

        // POST: Lecture/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]   // Endast admin har åtkomst
        public async Task<IActionResult> Edit(int id, [Bind("LectureId,Name,Description,Price,CategoryId")] Lecture lecture)
        {
            if (id != lecture.LectureId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(lecture);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LectureExists(lecture.LectureId))
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
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "Name", lecture.CategoryId);
            return View(lecture);
        }

        // GET: Lecture/Delete/5
        [Authorize(Roles = "Administrator")]   // Endast admin har åtkomst
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Kontroll om null-värde
            if (_context.Lectures == null)
            {
                return NotFound();
            }

            var lecture = await _context.Lectures
                .Include(l => l.Category)
                .FirstOrDefaultAsync(m => m.LectureId == id);
            if (lecture == null)
            {
                return NotFound();
            }

            return View(lecture);
        }

        // POST: Lecture/Delete/5
        [Authorize(Roles = "Administrator")]   // Endast admin har åtkomst
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            // Kontroll om null-värde
            if (_context.Lectures == null)
            {
                return NotFound();
            }

            var lecture = await _context.Lectures
            .Include(q => q.QuoteRequests)
            .FirstOrDefaultAsync(l => l.LectureId == id);

            if (lecture == null)
            {
                return NotFound();
            }

            // Kontrollera om föreläsningen har offerter knutna till sig
            if (lecture.QuoteRequests != null && lecture.QuoteRequests.Count != 0)
            {
                // Meddelande vid relation till offert, ska ej gå att radera
                TempData["ErrorMessage"] = "The lecture cannot be deleted because it has QuoteRequests associated with it!";

                return View(lecture);  // Stanna kvar på delete-sidan
            }

            _context.Lectures.Remove(lecture);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));

            // var lecture = await _context.Lectures.FindAsync(id);
            // if (lecture != null)
            // {
            //     _context.Lectures.Remove(lecture);
            // }

            // await _context.SaveChangesAsync();
            // return RedirectToAction(nameof(Index));
        }

        private bool LectureExists(int id)
        {
            // Kontroll om null-värde
            if (_context.Lectures == null)
            {
                return false;
            }

            return _context.Lectures.Any(e => e.LectureId == id);
        }
    }
}
