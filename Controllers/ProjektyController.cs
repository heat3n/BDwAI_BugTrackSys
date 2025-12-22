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
    public class ProjektyController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProjektyController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Projekty
        public async Task<IActionResult> Index()
        {
            return View(await _context.Projekty.ToListAsync());
        }

        // GET: Projekty/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var projekt = await _context.Projekty
                .FirstOrDefaultAsync(m => m.Id == id);
            if (projekt == null)
            {
                return NotFound();
            }

            return View(projekt);
        }

        // GET: Projekty/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Projekty/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nazwa,Opis")] Projekt projekt)
        {
            if (ModelState.IsValid)
            {
                _context.Add(projekt);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(projekt);
        }

        // GET: Projekty/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var projekt = await _context.Projekty.FindAsync(id);
            if (projekt == null)
            {
                return NotFound();
            }
            return View(projekt);
        }

        // POST: Projekty/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nazwa,Opis")] Projekt projekt)
        {
            if (id != projekt.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(projekt);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProjektExists(projekt.Id))
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
            return View(projekt);
        }

        // GET: Projekty/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var projekt = await _context.Projekty
                .FirstOrDefaultAsync(m => m.Id == id);
            if (projekt == null)
            {
                return NotFound();
            }

            return View(projekt);
        }

        // POST: Projekty/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var projekt = await _context.Projekty.FindAsync(id);
            if (projekt != null)
            {
                _context.Projekty.Remove(projekt);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProjektExists(int id)
        {
            return _context.Projekty.Any(e => e.Id == id);
        }
    }
}
