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
	public class PagoService : _BaseContext, IPagoService
	{
        public IEnumerable<Cliente> GetPay(int? ClienteId, int pageSize, int skip, out int nroTotalRegistros)
        {
            using (var dbContest = GetSISAPDBContext())
            {
                var sql = (from c in dbContest.Clientes
                           join fa in dbContest.Facturacions on c.ClienteId equals fa.ClienteId
                           join cat in dbContest.Categorias on c.CategoriaId equals cat.CategoriaId
                           join serv in dbContest.servicios on c.ServicioId equals serv.ServicioId
                           join p in dbContest.Pagos on c.ClienteId equals p.ClienteId into bt
                           from pa in bt.DefaultIfEmpty()
                           where c.ClienteId == ClienteId
                                 && pa.Estado == (int)EstadoPay.Pagado
                           orderby c.Nombre descending
                           select new
                           {
                               c.ClienteId,
                               fa.FacturacionId,
                               c.Nombre,
                               c.Apellido,
                               fa.Annio,
                               fa.Mes,
                               cat.CategoriaId,
                               cat.TipoCategoria,
                               serv.ServicioId,
                               serv.ServicioNombre,
                               fa.Total,
                               PagoId = pa == null ? 0 : pa.PagoId,
                               EstadoInt = pa == null ? 0 : pa.PagoId,
                               ObservacionesPago = pa == null ? string.Empty : pa.Observaciones,
                               Estado = (pa.Estado == 1 ? "Pagado" : "Pendiente"),
                               pa.FechaPago

                           });
                nroTotalRegistros = sql.Count();
                sql = sql.Skip(skip).Take(pageSize);
                var ListadoFinal = (from c in sql.ToList()
                                    select new Cliente()
                                    {
                                        ClienteId = c.ClienteId,
                                        FacturacionId = c.FacturacionId,
                                        Apellido = c.Apellido,
                                        Nombre = c.Nombre,
                                        Annio = c.Annio,
                                        Mes = c.Mes,
                                        CategoriaId = c.CategoriaId,
                                        TipoCategoria = c.TipoCategoria,
                                        ServicioId = c.ServicioId,
                                        ServicioNombre = c.ServicioNombre,
                                        Total = c.Total,
                                        PagoId = c.PagoId,
                                        Estado = c.Estado,
                                        EstadoInt = c.EstadoInt,
                                        ObservacionesPago = c.ObservacionesPago,
                                        FechaPago = string.Format("{0:dd/MM/yyyy}", c.FechaPago),

                                    }).ToList();
                return ListadoFinal;
            }
        }

        public Pago Pagar(Pago objPago)
        {
            using (var dbContext = GetSISAPDBContext())
            {
                dbContext.Pagos.Add(objPago);
                dbContext.SaveChanges();
            }
            return objPago;
        }

        public IEnumerable<Cliente> GetAllCF(int? UrbanizacionId, string FilterNombre, int pageSize, int skip, out int nroTotalRegistros)
		{
            using (var dbContext = GetSISAPDBContext())
            {
                var sql = (from c in dbContext.Clientes
                           join srv in dbContext.servicios on c.ServicioId equals srv.ServicioId
                           join cat in dbContext.Categorias on c.CategoriaId equals cat.CategoriaId
                           join u in dbContext.Urbanizacions on c.UrbanizacionId equals u.UrbanizacionId
                           join m in dbContext.Manzanas on c.ManzanaId equals m.ManzanaId
                           where (u.UrbanizacionId == UrbanizacionId || UrbanizacionId == null) &&
                                (string.IsNullOrEmpty(FilterNombre) || (c.Nombre + " " + c.Apellido + c.NumeroMedidor + "" + c.CodigoCliente + ""+c.DNI).Contains(FilterNombre.ToUpper()))
                           orderby c.Nombre ascending
                           select new
                           {
                               c.ClienteId,
                               c.UsuarioCreacion,
                               c.CodigoCliente,
                               c.Nombre,
                               c.Apellido,
                               c.DNI,
                               c.Telefono,
                               c.Direccion,
                               c.UrbanizacionId,
                               c.ManzanaId,
                               c.Complemento,
                               c.ServicioId,
                               c.CategoriaId,
                               c.NumeroMedidor,
                               c.EstadoServicioId,
                               c.Observaciones,
                               u.NombreUrbanizacion,
                               m.NombreManzana,
                               srv.ServicioNombre,
                               cat.TipoCategoria,
                               cat.ClaseId

                           });
                nroTotalRegistros = sql.Count();
                sql = sql.Skip(skip).Take(pageSize);

                var ListadoFinal = (from c in sql.ToList()
                                    select new Cliente()
                                    {
                                        ClienteId = c.ClienteId,
                                        CodigoCliente = c.CodigoCliente,
                                        Nombre = c.Nombre,
                                        Apellido = c.Apellido,
                                        DNI = c.DNI,
                                        Telefono = c.Telefono,
                                        Direccion = c.Direccion,
                                        UrbanizacionId = c.UrbanizacionId,
                                        ManzanaId = c.ManzanaId,
                                        ServicioId = c.ServicioId,
                                        CategoriaId = c.CategoriaId,
                                        Complemento = c.Complemento,
                                        NumeroMedidor = c.NumeroMedidor,
                                        EstadoServicioId = c.EstadoServicioId,
                                        UrbanizacionNombre = c.NombreUrbanizacion,
                                        ManzanaNombre = c.NombreManzana,
                                        Observaciones = c.Observaciones,
                                        ServicioNombre = c.ServicioNombre,
                                        TipoCategoria = c.TipoCategoria,
                                        ClaseId = c.ClaseId,

                                    }).ToList();
                return ListadoFinal;
            }
        }

		public IEnumerable<Cliente> GetClienteDeudor(int? ClienteId, int pageSize, int skip, out int nroTotalRegistros)
		{
			using (var dbContest = GetSISAPDBContext())
			{
				var sql = (from c in dbContest.Clientes
						   join fa in dbContest.Facturacions on c.ClienteId equals fa.ClienteId
						   join cat in dbContest.Categorias on c.CategoriaId equals cat.CategoriaId
						   join serv in dbContest.servicios on c.ServicioId equals serv.ServicioId
                           join p in dbContest.Pagos on c.ClienteId equals p.ClienteId into bt
                           from pa in bt.DefaultIfEmpty()
						   where c.ClienteId == ClienteId
                                && pa.Estado !=(int)EstadoPay.Pagado
						   orderby c.Nombre descending
						   select new
						   {
							   c.ClienteId,
							   fa.FacturacionId,
                               c.Nombre,
                               c.Apellido,
                               fa.Annio, 
							   fa.Mes,
							   cat.CategoriaId,
							   cat.TipoCategoria,
							   serv.ServicioId,
							   serv.ServicioNombre,
							   fa.Total,
                               PagoId = pa == null ? 0 : pa.PagoId,
                               EstadoInt = pa == null ? 0 : pa.PagoId,
                               ObservacionesPago = pa == null ? string.Empty : pa.Observaciones,
                               Estado = (pa.Estado == 1 ? "Pagado" : "Pendiente"),

                           });
				nroTotalRegistros = sql.Count();
				sql = sql.Skip(skip).Take(pageSize);
				var ListadoFinal = (from c in sql.ToList()
									select new Cliente()
									{
										ClienteId = c.ClienteId,
										FacturacionId = c.FacturacionId,
                                        Apellido = c.Apellido,
                                        Nombre = c.Nombre,
										Annio = c.Annio,
										Mes = c.Mes,
										CategoriaId = c.CategoriaId,
										TipoCategoria = c.TipoCategoria,
										ServicioId = c.ServicioId,
										ServicioNombre = c.ServicioNombre,
										Total = c.Total,
                                        PagoId = c.PagoId,
                                        Estado = c.Estado,
                                        EstadoInt = c.EstadoInt,
                                        ObservacionesPago = c.ObservacionesPago
                                    }).ToList();
				return ListadoFinal;
			}
		}
	}
}
