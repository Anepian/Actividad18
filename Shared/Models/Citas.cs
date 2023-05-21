using Actividad_18.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Actividad18.Shared.Models
{
    public class Citas
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public string Medico { get; set; }
        public int PacienteId { get; set; }
        public virtual Paciente? Paciente { get; set; }

        public virtual ICollection<Pagos>? Pagos { get; set; }
    }
}
