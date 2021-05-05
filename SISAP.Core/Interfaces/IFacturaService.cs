using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SISAP.Core.Entities;

namespace SISAP.Core.Interfaces
{
    public interface IFacturaService
    {
        IEnumerable<Factura> GetAll();
        IFacturaService ObtenerFactura(int FacturaId, int ClienteId, int LecturaId, int ImpuestoId, int DetalleFacturaId, 
            DateTime FechaCreacion, DateTime FechaModificacion, string Descuentos, string SubTotal, string ValorCobrar, 
            string TotalImpuesto, string TotalFactura, string Observaciones);
    }
}
