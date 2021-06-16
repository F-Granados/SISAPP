using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SISAP.Core.Entities
{
    public class Pago
    {
        public int PagoId { get; set; }
        public int ClienteId { get; set; }
        public int Estado { get; set; }
        public decimal? Total { get; set; }
        public int PeriodoAnnio { get; set; }
        public int PeriodoMes { get; set; }
        public DateTime? FechaPago { get; set; }
        public int? ServicioId { get; set; }
        public string Observaciones { get; set; }
        public string EstadoPDesc { get; set; }
    }
}
