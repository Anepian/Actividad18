using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Actividad18.Server.Contexto;
using Actividad18.Shared.Models;

namespace Actividad18.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CitasController : ControllerBase
    {
        private readonly Contextos _context;

        public CitasController(Contextos context)
        {
            _context = context;
        }

        // GET: api/Citas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Citas>>> GetCitas()
        {
            var citas = await _context.Citas
                .Include(c => c.Paciente)
                .Include(c => c.Pagos)
                .ToListAsync();

            return citas;
        }

        // GET: api/Citas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Citas>> GetCitas(int id)
        {
            var citas = await _context.Citas
                .Include(c => c.Paciente)
                .Include(c => c.Pagos)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (citas == null)
            {
                return NotFound();
            }

            return citas;
        }

        // PUT: api/Citas/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCitas(int id, Citas citas)
        {
            if (id != citas.Id)
            {
                return BadRequest();
            }

            _context.Entry(citas).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CitasExists(id))
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

        // POST: api/Citas
        [HttpPost]
        public async Task<ActionResult<Citas>> PostCitas(Citas citas)
        {
            if (_context.Citas == null)
            {
                return Problem("Entity set 'Contextos.Citas' is null.");
            }

            _context.Citas.Add(citas);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCitas", new { id = citas.Id }, citas);
        }

        // DELETE: api/Citas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCitas(int id)
        {
            var citas = await _context.Citas.FindAsync(id);
            if (citas == null)
            {
                return NotFound();
            }

            _context.Citas.Remove(citas);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CitasExists(int id)
        {
            return (_context.Citas?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
