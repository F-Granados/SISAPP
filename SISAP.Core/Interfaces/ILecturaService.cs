using SISAP.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SISAP.Core.Interfaces
{
	public interface ILecturaService
	{
		IEnumerable<Cliente> ListarClienteLectura(int? UrbanizacionId, int pageSize, int skip, out int nroTotalRegistros);
	}
}
