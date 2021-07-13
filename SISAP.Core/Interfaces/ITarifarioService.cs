using SISAP.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SISAP.Core.Interfaces
{
	public interface ITarifarioService
	{
		IEnumerable<Tarifario> getTarifario();
		void Update(Tarifario objTarifario);
		void Delete(int TarifarioId);
		Tarifario Save(Tarifario objTarifario);
		IEnumerable<Tarifario> ListarTarifario(int pageSize, int skip, out int nroTotalRegistros);
	}
}
