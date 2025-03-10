using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjektDT191G.Data;
using ProjektDT191G.Models;

namespace ProjektDT191G.Controllers
{
    public class QuoteRequestController : Controller
    {
        private readonly ApplicationDbContext _context;

        public QuoteRequestController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: QuoteRequest
        public async Task<IActionResult> Index()
        {
            // Kontroll om null-värde
            if (_context.QuoteRequests == null)
            {
                return NotFound();
            }

            var applicationDbContext = _context.QuoteRequests.Include(q => q.Lecture).Include(q => q.Speaker);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: QuoteRequest/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Kontroll om null-värde
            if (_context.QuoteRequests == null)
            {
                return NotFound();
            }

            var quoteRequest = await _context.QuoteRequests
                .Include(q => q.Lecture)
                .Include(q => q.Speaker)
                .FirstOrDefaultAsync(m => m.QuoteRequestId == id);
            if (quoteRequest == null)
            {
                return NotFound();
            }

            return View(quoteRequest);
        }

        // GET: QuoteRequest/Create
        public IActionResult Create()
        {
            ViewData["LectureId"] = new SelectList(_context.Lectures, "LectureId", "Description");
            ViewData["SpeakerId"] = new SelectList(_context.Speakers, "SpeakerId", "Name");
            return View();
        }

        // POST: QuoteRequest/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("QuoteRequestId,Name,Email,Phone,Address,Message,RequestDate,IsProcessed,LectureId,SpeakerId")] QuoteRequest quoteRequest)
        {
            if (ModelState.IsValid)
            {
                _context.Add(quoteRequest);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["LectureId"] = new SelectList(_context.Lectures, "LectureId", "Description", quoteRequest.LectureId);
            ViewData["SpeakerId"] = new SelectList(_context.Speakers, "SpeakerId", "Name", quoteRequest.SpeakerId);
            return View(quoteRequest);
        }

        // GET: QuoteRequest/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Kontroll om null-värde
            if (_context.QuoteRequests == null)
            {
                return NotFound();
            }

            var quoteRequest = await _context.QuoteRequests.FindAsync(id);
            if (quoteRequest == null)
            {
                return NotFound();
            }
            ViewData["LectureId"] = new SelectList(_context.Lectures, "LectureId", "Description", quoteRequest.LectureId);
            ViewData["SpeakerId"] = new SelectList(_context.Speakers, "SpeakerId", "Name", quoteRequest.SpeakerId);
            return View(quoteRequest);
        }

        // POST: QuoteRequest/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("QuoteRequestId,Name,Email,Phone,Address,Message,RequestDate,IsProcessed,LectureId,SpeakerId")] QuoteRequest quoteRequest)
        {
            if (id != quoteRequest.QuoteRequestId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(quoteRequest);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!QuoteRequestExists(quoteRequest.QuoteRequestId))
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
            ViewData["LectureId"] = new SelectList(_context.Lectures, "LectureId", "Description", quoteRequest.LectureId);
            ViewData["SpeakerId"] = new SelectList(_context.Speakers, "SpeakerId", "Name", quoteRequest.SpeakerId);
            return View(quoteRequest);
        }

        // GET: QuoteRequest/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Kontroll om null-värde
            if (_context.QuoteRequests == null)
            {
                return NotFound();
            }

            var quoteRequest = await _context.QuoteRequests
                .Include(q => q.Lecture)
                .Include(q => q.Speaker)
                .FirstOrDefaultAsync(m => m.QuoteRequestId == id);
            if (quoteRequest == null)
            {
                return NotFound();
            }

            return View(quoteRequest);
        }

        // POST: QuoteRequest/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            // Kontroll om null-värde
            if (_context.QuoteRequests == null)
            {
                return NotFound();
            }

            var quoteRequest = await _context.QuoteRequests.FindAsync(id);
            if (quoteRequest != null)
            {
                _context.QuoteRequests.Remove(quoteRequest);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool QuoteRequestExists(int id)
        {
            // Kontroll om null-värde
            if (_context.QuoteRequests == null)
            {
                return false;
            }

            return _context.QuoteRequests.Any(e => e.QuoteRequestId == id);
        }
    }
}
