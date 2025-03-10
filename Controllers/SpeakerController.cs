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
    public class SpeakerController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SpeakerController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Speaker
        public async Task<IActionResult> Index()
        {
            // Kontroll om null-värde
            if (_context.Speakers == null)
            {
                return NotFound();
            }

            return View(await _context.Speakers.ToListAsync());
        }

        // GET: Speaker/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Kontroll om null-värde
            if (_context.Speakers == null)
            {
                return NotFound();
            }

            var speaker = await _context.Speakers
                .FirstOrDefaultAsync(m => m.SpeakerId == id);
            if (speaker == null)
            {
                return NotFound();
            }

            return View(speaker);
        }

        // GET: Speaker/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Speaker/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SpeakerId,Name")] Speaker speaker)
        {
            if (ModelState.IsValid)
            {
                _context.Add(speaker);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(speaker);
        }

        // GET: Speaker/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Kontroll om null-värde
            if (_context.Speakers == null)
            {
                return NotFound();
            }

            var speaker = await _context.Speakers.FindAsync(id);
            if (speaker == null)
            {
                return NotFound();
            }
            return View(speaker);
        }

        // POST: Speaker/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SpeakerId,Name")] Speaker speaker)
        {
            if (id != speaker.SpeakerId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(speaker);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SpeakerExists(speaker.SpeakerId))
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
            return View(speaker);
        }

        // GET: Speaker/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Kontroll om null-värde
            if (_context.Speakers == null)
            {
                return NotFound();
            }

            var speaker = await _context.Speakers
                .FirstOrDefaultAsync(m => m.SpeakerId == id);
            if (speaker == null)
            {
                return NotFound();
            }

            return View(speaker);
        }

        // POST: Speaker/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            // Kontroll om null-värde
            if (_context.Speakers == null)
            {
                return NotFound();
            }

            var speaker = await _context.Speakers.FindAsync(id);
            if (speaker != null)
            {
                _context.Speakers.Remove(speaker);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SpeakerExists(int id)
        {
            // Kontroll om null-värde
            if (_context.Speakers == null)
            {
                return false;
            }

            return _context.Speakers.Any(e => e.SpeakerId == id);
        }
    }
}
