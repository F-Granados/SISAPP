using SISAP.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SISAP.Core.Interfaces
{
	public interface IPagoService
	{
		IEnumerable<Cliente> GetPay(int? ClienteId, int pageSize, int skip, out int nroTotalRegistros);
		Pago Pagar(Pago objPago);
		IEnumerable<Cliente> GetAllCF(int? UrbanizacionId, string FilterNombre, int pageSize, int skip, out int nroTotalRegistros);
		IEnumerable<Cliente> GetClienteDeudor(int? ClienteId, int pageSize, int skip, out int nroTotalRegistros);
	}
}
