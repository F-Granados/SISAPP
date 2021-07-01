using SISAP.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SISAP.Core.Interfaces
{
	public interface IReportesService
	{
		decimal? GetDeudaDistrito(int? Annio);
		decimal? GetDeudaRuta(int? Annio, int? Mes, int? UrbanizacionId);
		decimal? GetIngresoAnual(int? Annio);
		decimal? GetIngresoMensual(int? Annio, int? Mes);
		decimal? GetProcessLectura(int? Annio, int? Mes);
		IEnumerable<Cliente> ListReporte(int? Annio, string FilterNombre, int pageSize, int skip, out int nroTotalRegistros);
		IEnumerable<Cliente> GetAllCF(int? UrbanizacionId, string FilterNombre, int pageSize, int skip, out int nroTotalRegistros);
	}
}
