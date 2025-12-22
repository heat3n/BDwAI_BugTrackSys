using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BDwAI_BugTrackSys.Data;
using BDwAI_BugTrackSys.Models;

namespace BDwAI_BugTrackSys.Controllers
{
    public class PriorytetyController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PriorytetyController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Priorytety
        public async Task<IActionResult> Index()
        {
            return View(await _context.Priorytety.ToListAsync());
        }

        // GET: Priorytety/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var priorytet = await _context.Priorytety
                .FirstOrDefaultAsync(m => m.Id == id);
            if (priorytet == null)
            {
                return NotFound();
            }

            return View(priorytet);
        }

        // GET: Priorytety/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Priorytety/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nazwa")] Priorytet priorytet)
        {
            if (ModelState.IsValid)
            {
                _context.Add(priorytet);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(priorytet);
        }

        // GET: Priorytety/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var priorytet = await _context.Priorytety.FindAsync(id);
            if (priorytet == null)
            {
                return NotFound();
            }
            return View(priorytet);
        }

        // POST: Priorytety/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nazwa")] Priorytet priorytet)
        {
            if (id != priorytet.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(priorytet);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PriorytetExists(priorytet.Id))
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
            return View(priorytet);
        }

        // GET: Priorytety/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var priorytet = await _context.Priorytety
                .FirstOrDefaultAsync(m => m.Id == id);
            if (priorytet == null)
            {
                return NotFound();
            }

            return View(priorytet);
        }

        // POST: Priorytety/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var priorytet = await _context.Priorytety.FindAsync(id);
            if (priorytet != null)
            {
                _context.Priorytety.Remove(priorytet);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PriorytetExists(int id)
        {
            return _context.Priorytety.Any(e => e.Id == id);
        }
    }
}
