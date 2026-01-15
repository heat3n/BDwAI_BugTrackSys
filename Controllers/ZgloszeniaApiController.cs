using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BDwAI_BugTrackSys.Data;
using BDwAI_BugTrackSys.Models;

namespace BDwAI_BugTrackSys.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ZgloszeniaApiController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ZgloszeniaApiController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/ZgloszeniaApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Zgloszenie>>> GetZgloszenia()
        {
            return await _context.Zgloszenia.ToListAsync();
        }

        // GET: api/ZgloszeniaApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Zgloszenie>> GetZgloszenie(int id)
        {
            var zgloszenie = await _context.Zgloszenia.FindAsync(id);

            if (zgloszenie == null)
            {
                return NotFound();
            }

            return zgloszenie;
        }

        // PUT: api/ZgloszeniaApi/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutZgloszenie(int id, Zgloszenie zgloszenie)
        {
            if (id != zgloszenie.Id)
            {
                return BadRequest();
            }

            _context.Entry(zgloszenie).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ZgloszenieExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/ZgloszeniaApi
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Zgloszenie>> PostZgloszenie(Zgloszenie zgloszenie)
        {
            _context.Zgloszenia.Add(zgloszenie);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetZgloszenie", new { id = zgloszenie.Id }, zgloszenie);
        }

        // DELETE: api/ZgloszeniaApi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteZgloszenie(int id)
        {
            var zgloszenie = await _context.Zgloszenia.FindAsync(id);
            if (zgloszenie == null)
            {
                return NotFound();
            }

            _context.Zgloszenia.Remove(zgloszenie);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ZgloszenieExists(int id)
        {
            return _context.Zgloszenia.Any(e => e.Id == id);
        }
    }
}
