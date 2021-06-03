using SISAP.Core.Interfaces;
using SISAP.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SISAP.Infrastructure.Service
{
	public class LecturaService : _BaseContext, ILecturaService
	{

		public IEnumerable<Cliente> ListarClienteLectura(int? UrbanizacionId, int pageSize, int skip, out int nroTotalRegistros)
		{
			using (var dbContext = GetSISAPDBContext())
			{
				var sql = (from c in dbContext.Clientes
						   join l in dbContext.Lecturas on c.ClienteId equals l.ClienteId into dcl
						   from lec in dcl.DefaultIfEmpty()
						   join u in dbContext.Urbanizacions on c.UrbanizacionId equals u.UrbanizacionId
						   join m in dbContext.Manzanas on c.ManzanaId equals m.ManzanaId
						   where (c.UrbanizacionId == UrbanizacionId || null == UrbanizacionId)
						   orderby c.Nombre
						   select new
						   {
							   c.ClienteId,
							   c.Nombre,
							   c.Apellido,
							   c.Telefono,
							   c.Direccion,
							   c.UrbanizacionId,
							   c.ManzanaId,
							   c.Complemento,
							   c.NumeroMedidor,
							   u.NombreUrbanizacion,
							   m.NombreManzana,
							   Annio = lec ==null? 0 : lec.Annio,
							   Mes = lec ==null? 0 : lec.Mes,
							   CantidadLectura = lec ==null? 0 : lec.CantidadLectura,
							   Consumo = lec ==null? 0 : lec.Consumo,
							   Promedio = lec ==null? 0 : lec.Promedio,
							   Alerta = lec ==null? String.Empty : lec.Alerta

						   });
				nroTotalRegistros = sql.Count();
				sql = sql.Skip(skip).Take(pageSize);

				var ListadoFinal = (from c in sql.ToList()
									select new Cliente()
									{
										ClienteId = c.ClienteId,
										Nombre = c.Nombre,
										Apellido = c.Apellido,
										Telefono = c.Telefono,
										Direccion = c.Direccion,
										UrbanizacionId = c.UrbanizacionId,
										ManzanaId = c.ManzanaId,
										NumeroMedidor = c.NumeroMedidor,
										UrbanizacionNombre = c.NombreUrbanizacion,
										ManzanaNombre = c.NombreManzana,
										Complemento=c.Complemento,
										Annio = c.Annio,
										Mes = c.Mes,
										CantidadLectura = c.CantidadLectura,
										Consumo = c.Consumo,
										Promedio = c.Promedio,
										Alerta = c.Alerta
									}).ToList();

				return ListadoFinal;
			}
		}
	}
}
