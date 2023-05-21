using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Actividad18.Shared.Models
{
    public class Pagos
    {
        public int Id { get; set; }
        public int Total { get; set; }
        public int Pago { get; set; }
        public DateTime Fecha { get; set; }
        public int CitasId { get; set; }
        public virtual Citas? Citas { get; set; }
    }
}
