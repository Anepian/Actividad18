using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Actividad18.Server.Contexto;
using Actividad_18.Shared.Models;

namespace Actividad18.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PacientesController : ControllerBase
    {
        private readonly Contextos _context;

        public PacientesController(Contextos context)
        {
            _context = context;
        }

        // GET: api/Pacientes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Paciente>>> GetPaciente()
        {
            var pacientes = await _context.Paciente
                .Include(p => p.Citas)
                .ThenInclude(c => c.Pagos)
                .ToListAsync();

            return pacientes;
        }

        // GET: api/Pacientes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Paciente>> GetPaciente(int? id)
        {
            var paciente = await _context.Paciente
                .Include(p => p.Citas)
                .ThenInclude(c => c.Pagos)
                .FirstOrDefaultAsync(p => p.id == id);

            if (paciente == null)
            {
                return NotFound();
            }

            return paciente;
        }

        // PUT: api/Pacientes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPaciente(int? id, Paciente paciente)
        {
            if (id != paciente.id)
            {
                return BadRequest();
            }

            _context.Entry(paciente).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PacienteExists(id))
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

        // POST: api/Pacientes
        [HttpPost]
        public async Task<ActionResult<Paciente>> PostPaciente(Paciente paciente)
        {
            if (_context.Paciente == null)
            {
                return Problem("Entity set 'Contextos.Paciente' is null.");
            }

            _context.Paciente.Add(paciente);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPaciente", new { id = paciente.id }, paciente);
        }

        // DELETE: api/Pacientes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePaciente(int? id)
        {
            var paciente = await _context.Paciente
                .Include(p => p.Citas)
                .ThenInclude(c => c.Pagos)
                .FirstOrDefaultAsync(p => p.id == id);

            if (paciente == null)
            {
                return NotFound();
            }

            _context.Pagos.RemoveRange(paciente.Citas.SelectMany(c => c.Pagos));
            _context.Citas.RemoveRange(paciente.Citas);
            _context.Paciente.Remove(paciente);

            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PacienteExists(int? id)
        {
            return (_context.Paciente?.Any(e => e.id == id)).GetValueOrDefault();
        }
    }
}
