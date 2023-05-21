using Actividad_18.Shared.Models;
using Actividad18.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace Actividad18.Server.Contexto
{
    public class Contextos : DbContext
    {
        public Contextos(DbContextOptions<Contextos> options):base (options) 
        {
            
        }
        public DbSet<Paciente> Paciente { get; set;}
        public DbSet<Citas> Citas { get; set;}
        public DbSet<Pagos> Pagos { get; set;}
    }
}
