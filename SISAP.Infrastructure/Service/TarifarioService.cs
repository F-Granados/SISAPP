using SISAP.Core.Entities;
using SISAP.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SISAP.Infrastructure.Service
{
	public class TarifarioService : _BaseContext, ITarifarioService
	{
		public IEnumerable<Tarifario> getTarifario()
		{
			using (var dbContext = GetSISAPDBContext())
			{
				return dbContext.Tarifarios.OrderBy(t => t.TarifarioId).ToList();
			}
		}


		public IEnumerable<Tarifario> ListarTarifario(int pageSize, int skip, out int nroTotalRegistros)
        {
			using (var dbContext = GetSISAPDBContext())
					{
				var sql = (from t in dbContext.Tarifarios
						   orderby t.TarifarioId ascending
						   select new
						   {
							   t.TarifarioId,
							   t.CategoriaId,
							   t.RangoMin,
							   t.RangoMax,
							   t.TarifaAgua,
							   t.TarifaAlcantarillado,
							   t.CargoFijo,
							   t.ClaseId

						   });
				nroTotalRegistros = sql.Count();
				sql = sql.Skip(skip).Take(pageSize);
				var ListFinal = (from t in sql.ToList()
								 select new Tarifario()
								 {
									 TarifarioId = t.TarifarioId,
									 CategoriaId = t.CategoriaId,
									 RangoMin = t.RangoMin,
									 RangoMax = t.RangoMax,
									 TarifaAgua = t.TarifaAgua,
									 TarifaAlcantarillado = t.TarifaAlcantarillado,
									 CargoFijo = t.CargoFijo,
									 ClaseId = t.ClaseId
								 }).ToList();
				return ListFinal;

            }

        }

		public Tarifario Save(Tarifario objTarifario)
        {
			using (var dbContext = GetSISAPDBContext())
            {
				dbContext.Tarifarios.Add(objTarifario);
				dbContext.SaveChanges();

            }
			return objTarifario;
        }
		public void Update(Tarifario tarifario)
		{
			using (var dbContext = GetSISAPDBContext())
			{
				dbContext.Tarifarios.Attach(tarifario);
				dbContext.Entry(tarifario).State = EntityState.Modified;
				dbContext.SaveChanges();
			}
		}


		public void Delete(int TarifarioId)
        {
			using (var dbContext = GetSISAPDBContext())
            {
				var tarifarios = dbContext.Tarifarios;
				var tarifario = tarifarios.FirstOrDefault(o => o.TarifarioId == TarifarioId);
				dbContext.Tarifarios.Remove(tarifario);
				dbContext.SaveChanges();
            }
        }

		public IEnumerable<Categoria> GetAllCategoria()
		{
			using (var dbContext = GetSISAPDBContext())
			{
				return dbContext.Categorias.OrderBy(o => o.CategoriaId).ToList();
			}
		}


		public IEnumerable<Clase> GetAllCategoria()
		{
			using (var dbContext = GetSISAPDBContext())
			{
				return dbContext.Categorias.OrderBy(o => o.CategoriaId).ToList();
			}
		}
	}
}
