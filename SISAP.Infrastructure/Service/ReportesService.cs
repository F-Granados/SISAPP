using SISAP.Core.Entities;
using SISAP.Core.Enum;
using SISAP.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SISAP.Infrastructure.Service
{
	public class ReportesService : _BaseContext, IReportesService
	{

		public decimal? getDeudaRuta(int? Annio, int? Mes, int? UrbanizacionId)
		{
			using (var dbContext = GetSISAPDBContext())
			{
				var sql = (from c in dbContext.Clientes
						   join p in dbContext.Pagos on c.ClienteId equals p.ClienteId
						   join u in dbContext.Urbanizacions on c.UrbanizacionId equals u.UrbanizacionId
						   where p.PeriodoAnnio == Annio && p.PeriodoMes == Mes && u.UrbanizacionId == UrbanizacionId && p.EstadoPago == (int)EstadoPay.Pendiente
						   select p).Sum(p => p.Total);

				return sql;
			}
		}
		public decimal? getDeudaDistrito(int? Annio)
		{
			using (var dbContext = GetSISAPDBContext())
			{

				var total = dbContext.Pagos.Where(l=>l.PeriodoAnnio == Annio && l.EstadoPago == (int)EstadoPay.Pendiente).Sum(l => l.Total);

				return total;
			}
		}
		public decimal? getIngresoAnual(int? Annio)
		{
			using (var dbContext = GetSISAPDBContext())
			{
				var total = dbContext.Pagos.Where(l=>l.PeriodoAnnio == Annio && l.EstadoPago == (int)EstadoPay.Pagado).Sum(l => l.Total);

				return total;
			}
		}
		public decimal? getIngresoMensual(int? Annio, int? Mes)
		{
			using (var dbContext = GetSISAPDBContext())
			{
				var total = dbContext.Pagos.Where(l=>l.PeriodoAnnio == Annio && l.PeriodoMes == Mes && l.EstadoPago == (int)EstadoPay.Pagado).Sum(l => l.Total);

				return total;
			}
		}
		public decimal? getProcessLectura(int? Annio, int? Mes)
		{
			using (var dbContext = GetSISAPDBContext())
			{
				var total = dbContext.Lecturas.Where(l=>l.Annio == Annio && l.Mes == Mes).Sum(l => l.Consumo);

				return total;
			}
		}

		public IEnumerable<Cliente> ListReporte(int? Annio, int? Mes, int? UrbanizacionId, string FilterNombre, int pageSize, int skip, out int nroTotalRegistros)
		{
			using (var dbContext = GetSISAPDBContext())
			{
				var sql = (from c in dbContext.Clientes
						   join srv in dbContext.servicios on c.ServicioId equals srv.ServicioId
						   join cat in dbContext.Categorias on c.CategoriaId equals cat.CategoriaId
						   join l in dbContext.Lecturas on c.ClienteId equals l.ClienteId
						   join u in dbContext.Urbanizacions on c.UrbanizacionId equals u.UrbanizacionId
						   join m in dbContext.Manzanas on c.ManzanaId equals m.ManzanaId
						   join fa in dbContext.Facturacions on c.ClienteId equals fa.ClienteId

						   where (c.UrbanizacionId == UrbanizacionId)
								&& (fa.Annio == Annio)
								&& (fa.Mes == Mes)
								&& (l.Annio == Annio)
								&& (l.Mes == Mes)
								&& (String.IsNullOrEmpty(FilterNombre) || c.Nombre.Contains(FilterNombre))
						   orderby c.Nombre
						   select new
						   {
							   c.CodigoCliente,
							   c.ClienteId,
							   c.Nombre,
							   c.Apellido,
							   c.Telefono,
							   c.Direccion,
							   c.UrbanizacionId,
							   c.ManzanaId,
							   c.NumeroMedidor,
							   c.Complemento,
							   u.NombreUrbanizacion,
							   m.NombreManzana,
							   l.LecturaId,
							   l.Annio,
							   l.Mes,
							   l.CantidadLectura,
							   l.Consumo,
							   l.CantidadLecturaAntigua,
							   fa.FacturacionId,
							   fa.Total,
							   srv.ServicioId,
							   srv.ServicioNombre,
							   cat.CategoriaId,
							   cat.TipoCategoria,
							   cat.ClaseId
							   /*
							   LecturaId = lec == null ? 0 : lec.LecturaId,
							   Annio = lec == null ? 00 : lec.Annio,
							   Mes = lec == null ? 00 : lec.Mes,
							   CantidadLectura = lec == null ? 0 : lec.CantidadLectura,
							   Consumo = lec == null ? 00 : lec.Consumo,
							   Promedio = lec == null ? 00 : lec.Promedio,
							   Alerta = lec == null ? String.Empty : lec.Alerta,
							   CantidadLecturaAntigua = lec == null ? 00 : lec.CantidadLecturaAntigua
							   */
						   });
				nroTotalRegistros = sql.Count();
				sql = sql.Skip(skip).Take(pageSize);

				var ListadoFinal = (from c in sql.ToList()
									select new Cliente()
									{
										CodigoCliente = c.CodigoCliente,
										ClienteId = c.ClienteId,
										Nombre = c.Nombre,
										Apellido = c.Apellido,
										Telefono = c.Telefono,
										Direccion = c.Direccion,
										UrbanizacionId = c.UrbanizacionId,
										ManzanaId = c.ManzanaId,
										NumeroMedidor = c.NumeroMedidor,
										UrbanizacionNombre = c.NombreUrbanizacion,
										Complemento = c.Complemento,
										ManzanaNombre = c.NombreManzana,
										LecturaId = c.LecturaId,
										Annio = c.Annio,
										Mes = c.Mes,
										CantidadLectura = c.CantidadLectura,
										Consumo = c.Consumo,
										CantidadLecturaAntigua = c.CantidadLecturaAntigua,
										FacturacionId = c.FacturacionId,
										Total = c.Total,
										ServicioId = c.ServicioId,
										ServicioNombre = c.ServicioNombre,
										CategoriaId = c.CategoriaId,
										TipoCategoria = c.TipoCategoria,
										ClaseId = c.ClaseId

									}).ToList();
				return ListadoFinal;
			}
		}
	}
}
