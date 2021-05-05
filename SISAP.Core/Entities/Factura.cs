using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SISAP.Core.Entities
{
    public class Factura
    {
        public int FacturaId { get; set; }
        public int ClienteId { get; set; }
        public int LecturaId { get; set; }
        public int ImpuestoId { get; set; }
        public int DetalleFacturaId { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public string Descuentos { get; set; }
        public string Subtotal { get; set; }
        public string ValorCobrar { get; set; }
        public string TotalImpuesto { get; set; }
        public string TotalFactura { get;set; }
        public string Observaciones { get; set; }
    }
}
