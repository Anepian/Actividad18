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
    public class PagosController : ControllerBase
    {
        private readonly Contextos _context;

        public PagosController(Contextos context)
        {
            _context = context;
        }

        // GET: api/Pagos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Pagos>>> GetPagos()
        {
            var pagos = await _context.Pagos
                .Include(p => p.Citas)
                .ToListAsync();

            return pagos;
        }

        // GET: api/Pagos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Pagos>> GetPagos(int id)
        {
            var pagos = await _context.Pagos
                .Include(p => p.Citas)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (pagos == null)
            {
                return NotFound();
            }

            return pagos;
        }

        // PUT: api/Pagos/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPagos(int id, Pagos pagos)
        {
            if (id != pagos.Id)
            {
                return BadRequest();
            }

            _context.Entry(pagos).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PagosExists(id))
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

        // POST: api/Pagos
        [HttpPost]
        public async Task<ActionResult<Pagos>> PostPagos(Pagos pagos)
        {
            if (_context.Pagos == null)
            {
                return Problem("Entity set 'Contextos.Pagos' is null.");
            }

            _context.Pagos.Add(pagos);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPagos", new { id = pagos.Id }, pagos);
        }

        // DELETE: api/Pagos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePagos(int id)
        {
            var pagos = await _context.Pagos.FindAsync(id);
            if (pagos == null)
            {
                return NotFound();
            }

            _context.Pagos.Remove(pagos);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PagosExists(int id)
        {
            return (_context.Pagos?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
