using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using BDwAI_BugTrackSys.Data;
using System.Security.Claims;
using System.Linq;
using System.Threading.Tasks;

namespace BDwAI_BugTrackSys.Controllers
{
    [Authorize]
    public class PowiadomieniaController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PowiadomieniaController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Powiadomienia
        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var powiadomienia = await _context.Powiadomienia
                .Where(p => p.UzytkownikId == userId)
                .OrderByDescending(p => p.Data)
                .ToListAsync();

            return View(powiadomienia);
        }

        // GET: Powiadomienia/MarkAsReadAndGo/5
        public async Task<IActionResult> MarkAsReadAndGo(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var powiadomienie = await _context.Powiadomienia.FindAsync(id);

            if (powiadomienie != null && powiadomienie.UzytkownikId == userId)
            {
                powiadomienie.CzyPrzeczytane = true;
                await _context.SaveChangesAsync();

                return RedirectToAction("Details", "Zgloszenia", new { id = powiadomienie.ZgloszenieId });
            }
            return RedirectToAction(nameof(Index));
        }

        // POST: Powiadomienia/ClearRead
        [HttpPost]
        public async Task<IActionResult> ClearRead()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var toDelete = _context.Powiadomienia
                .Where(p => p.UzytkownikId == userId && p.CzyPrzeczytane);

            _context.Powiadomienia.RemoveRange(toDelete);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}