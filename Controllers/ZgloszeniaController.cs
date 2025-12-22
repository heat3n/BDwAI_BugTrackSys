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
    public class ZgloszeniaController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ZgloszeniaController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Zgloszenia
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Zgloszenia.Include(z => z.Priorytet).Include(z => z.Projekt).Include(z => z.Status);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Zgloszenia/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var zgloszenie = await _context.Zgloszenia
                .Include(z => z.Priorytet)
                .Include(z => z.Projekt)
                .Include(z => z.Status)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (zgloszenie == null)
            {
                return NotFound();
            }

            return View(zgloszenie);
        }

        // GET: Zgloszenia/Create
        public IActionResult Create()
        {
            ViewData["PriorytetId"] = new SelectList(_context.Priorytety, "Id", "Nazwa");
            ViewData["ProjektId"] = new SelectList(_context.Projekty, "Id", "Nazwa");
            ViewData["StatusId"] = new SelectList(_context.Statusy, "Id", "Nazwa");
            return View();
        }

        // POST: Zgloszenia/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Temat,Opis,DataUtworzenia,ProjektId,PriorytetId,StatusId")] Zgloszenie zgloszenie)
        {
            zgloszenie.DataUtworzenia = DateTime.Now;
            if (ModelState.IsValid)
            {
                _context.Add(zgloszenie);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PriorytetId"] = new SelectList(_context.Priorytety, "Id", "Nazwa", zgloszenie.PriorytetId);
            ViewData["ProjektId"] = new SelectList(_context.Projekty, "Id", "Nazwa", zgloszenie.ProjektId);
            ViewData["StatusId"] = new SelectList(_context.Statusy, "Id", "Nazwa", zgloszenie.StatusId);
            return View(zgloszenie);
        }

        // GET: Zgloszenia/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var zgloszenie = await _context.Zgloszenia.FindAsync(id);
            if (zgloszenie == null)
            {
                return NotFound();
            }
            ViewData["PriorytetId"] = new SelectList(_context.Priorytety, "Id", "Nazwa", zgloszenie.PriorytetId);
            ViewData["ProjektId"] = new SelectList(_context.Projekty, "Id", "Nazwa", zgloszenie.ProjektId);
            ViewData["StatusId"] = new SelectList(_context.Statusy, "Id", "Nazwa", zgloszenie.StatusId);
            return View(zgloszenie);
        }

        // POST: Zgloszenia/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Temat,Opis,DataUtworzenia,ProjektId,PriorytetId,StatusId")] Zgloszenie zgloszenie)
        {
            if (id != zgloszenie.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(zgloszenie);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ZgloszenieExists(zgloszenie.Id))
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
            ViewData["PriorytetId"] = new SelectList(_context.Priorytety, "Id", "Nazwa", zgloszenie.PriorytetId);
            ViewData["ProjektId"] = new SelectList(_context.Projekty, "Id", "Nazwa", zgloszenie.ProjektId);
            ViewData["StatusId"] = new SelectList(_context.Statusy, "Id", "Nazwa", zgloszenie.StatusId);
            return View(zgloszenie);
        }

        // GET: Zgloszenia/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var zgloszenie = await _context.Zgloszenia
                .Include(z => z.Priorytet)
                .Include(z => z.Projekt)
                .Include(z => z.Status)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (zgloszenie == null)
            {
                return NotFound();
            }

            return View(zgloszenie);
        }

        // POST: Zgloszenia/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var zgloszenie = await _context.Zgloszenia.FindAsync(id);
            if (zgloszenie != null)
            {
                _context.Zgloszenia.Remove(zgloszenie);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ZgloszenieExists(int id)
        {
            return _context.Zgloszenia.Any(e => e.Id == id);
        }
    }
}
