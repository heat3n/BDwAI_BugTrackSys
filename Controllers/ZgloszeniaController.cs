using BDwAI_BugTrackSys.Data;
using BDwAI_BugTrackSys.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;

namespace BDwAI_BugTrackSys.Controllers
{
    [Authorize]
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
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var zgloszenia = _context.Zgloszenia
                .Include(z => z.Priorytet)
                .Include(z => z.Projekt)
                .Include(z => z.Status)
                .Include(z => z.Uzytkownik);

            if (User.IsInRole("Admin")) return View(await zgloszenia.ToListAsync());
            else return View(await zgloszenia.Where(z => z.UzytkownikId == userId).ToListAsync());

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
                .Include(z => z.Uzytkownik)
                .Include(z => z.Komentarze)
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
            // ViewData["StatusId"] = new SelectList(_context.Statusy, "Id", "Nazwa");
            return View();
        }

        // POST: Zgloszenia/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Temat,Opis,DataUtworzenia,ProjektId,PriorytetId")] Zgloszenie zgloszenie)
        {
            zgloszenie.DataUtworzenia = DateTime.Now;
            zgloszenie.UzytkownikId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            zgloszenie.StatusId = 1;
            if (ModelState.IsValid)
            {
                _context.Add(zgloszenie);
                await _context.SaveChangesAsync();

                var adminRole = await _context.Roles.FirstOrDefaultAsync(r => r.Name == "Admin");

                if (adminRole != null)
                {
                    var adminUserIds = await _context.UserRoles
                        .Where(ur => ur.RoleId == adminRole.Id)
                        .Select(ur => ur.UserId)
                        .ToListAsync();

                    foreach (var adminId in adminUserIds)
                    {
                        if (adminId != zgloszenie.UzytkownikId)
                        {
                            var powiadomienie = new Powiadomienie
                            {
                                UzytkownikId = adminId,
                                ZgloszenieId = zgloszenie.Id,
                                Tresc = $"Nowe zgłoszenie: {zgloszenie.Temat} (od {User.Identity.Name})",
                                Data = DateTime.Now,
                                CzyPrzeczytane = false
                            };
                            _context.Add(powiadomienie);
                        }
                    }
                    await _context.SaveChangesAsync();
                }
                return RedirectToAction(nameof(Success));
            }
            ViewData["PriorytetId"] = new SelectList(_context.Priorytety, "Id", "Nazwa", zgloszenie.PriorytetId);
            ViewData["ProjektId"] = new SelectList(_context.Projekty, "Id", "Nazwa", zgloszenie.ProjektId);
            return View(zgloszenie);
        }

        // GET: Zgloszenia/Edit/5
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Temat,Opis,DataUtworzenia,ProjektId,PriorytetId,StatusId")] Zgloszenie zgloszenie)
        {
            if (id != zgloszenie.Id)
            {
                return NotFound();
            }

            ModelState.Remove("UzytkownikId");
            ModelState.Remove("DataUtworzenia");
            ModelState.Remove("Projekt");
            ModelState.Remove("Priorytet");
            ModelState.Remove("Status");
            ModelState.Remove("Uzytkownik");

            if (ModelState.IsValid)
            {
                try
                {
                    var oldZgloszenie = await _context.Zgloszenia
                        .AsNoTracking()
                        .FirstOrDefaultAsync(z => z.Id == id);

                    if (oldZgloszenie != null)
                    {
                        zgloszenie.UzytkownikId = oldZgloszenie.UzytkownikId;
                        zgloszenie.DataUtworzenia = oldZgloszenie.DataUtworzenia;

                        if (oldZgloszenie.StatusId != zgloszenie.StatusId)
                        {
                            var nowyStatus = await _context.Statusy.FindAsync(zgloszenie.StatusId);
                            string nazwaStatusu = nowyStatus?.Nazwa ?? "Nieznany";

                            var powiadomienie = new Powiadomienie
                            {
                                UzytkownikId = zgloszenie.UzytkownikId,
                                ZgloszenieId = zgloszenie.Id,
                                Tresc = $"Status zgłoszenia '{zgloszenie.Temat}' został zmieniony na: {nazwaStatusu}",
                                Data = DateTime.Now,
                                CzyPrzeczytane = false
                            };
                            _context.Add(powiadomienie);
                        }
                    }

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
        //POST: Komentarze
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddComment(int zgloszenieId, string tresc)
        {
            if (string.IsNullOrWhiteSpace(tresc))
            {
                return RedirectToAction("Details", new { id = zgloszenieId });
            }

            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var currentUserName = User.Identity.Name ?? "Nieznany";

            var komentarz = new Komentarz
            {
                ZgloszenieId = zgloszenieId,
                Tresc = tresc,
                DataDodania = DateTime.Now,
                Autor = currentUserName
            };
            _context.Add(komentarz);

            var zgloszenie = await _context.Zgloszenia.FindAsync(zgloszenieId);

            if (zgloszenie != null)
            {
                string trescPowiadomienia = $"Nowy komentarz w zgłoszeniu '{zgloszenie.Temat}' od użytkownika {currentUserName}";

                if (zgloszenie.UzytkownikId != currentUserId)
                {
                    var powiadomienie = new Powiadomienie
                    {
                        UzytkownikId = zgloszenie.UzytkownikId,
                        ZgloszenieId = zgloszenie.Id,
                        Tresc = trescPowiadomienia,
                        Data = DateTime.Now,
                        CzyPrzeczytane = false
                    };
                    _context.Add(powiadomienie);
                }
                if (!User.IsInRole("Admin"))
                {
                    var adminRole = await _context.Roles.FirstOrDefaultAsync(r => r.Name == "Admin");
                    if (adminRole != null)
                    {
                        var adminUserIds = await _context.UserRoles
                            .Where(ur => ur.RoleId == adminRole.Id)
                            .Select(ur => ur.UserId)
                            .ToListAsync();

                        foreach (var adminId in adminUserIds)
                        {
                            if (adminId != zgloszenie.UzytkownikId)
                            {
                                var powiadomienieAdmin = new Powiadomienie
                                {
                                    UzytkownikId = adminId,
                                    ZgloszenieId = zgloszenie.Id,
                                    Tresc = trescPowiadomienia,
                                    Data = DateTime.Now,
                                    CzyPrzeczytane = false
                                };
                                _context.Add(powiadomienieAdmin);
                            }
                        }
                    }
                }
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("Details", new { id = zgloszenieId });
        }
        public IActionResult Success()
        {
            return View();
        }
    }
}
