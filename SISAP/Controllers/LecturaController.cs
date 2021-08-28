using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using SISAP.Core.Entities;
using SISAP.Core.Enum;
using SISAP.Core.Interfaces;
using SISAP.Infrastructure.Service;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SISAP.Controllers
{
    public class LecturaController : Controller
    {
        private readonly ILecturaService _lecturaService;
        private readonly ICiclosService _ciclosService;
        private readonly ITarifarioService _tarifarioService;
        private readonly IClienteService _clienteService;
        private readonly IFacturaService _facturaService;

        public LecturaController()
		{
            _lecturaService = new LecturaService();
            _ciclosService = new CiclosService();
            _tarifarioService = new TarifarioService();
            _clienteService = new ClienteService();
            _facturaService = new FacturaService();
		}
        // GET: Lectura
        public ActionResult Lectura()
        {
            return View();
        }

        #region "Lectura"

        public ActionResult ReporteLectura(int urb)
        {

            string connectionString = ConfigurationManager.ConnectionStrings["SISAPDBContext"].ConnectionString;
            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/ReportesCR"), "rptLectura.rpt"));
            rd.SetParameterValue("@urbanizacionId", urb);
            //rd.DataSourceConnections[0].IntegratedSecurity = true;
            //rd.DataSourceConnections[0].SetConnection("DESKTOP-KTMHKON", "SISAP-DEV", true);
            Response.Buffer = false;
            Response.ClearContent();
            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            return File(stream, "application/pdf"/*"facturas.pdf"*/);

        }
        #endregion



        [HttpPost]
        public JsonResult ListLecturaMain(int? Annio, int? Mes, int? UrbanizacionId, string FilterNombre)
        {
            var draw = Request.Form.GetValues("draw").FirstOrDefault();
            var start = Request.Form.GetValues("start").FirstOrDefault();
            var length = Request.Form.GetValues("length").FirstOrDefault();

            int pageSize = length != null ? Convert.ToInt32(length) : 0;
            int skip = start != null ? Convert.ToInt32(start) : 0;
            int nroTotalRegistros = 0;

            var lecturas = _lecturaService.ListLecturaMain(Annio, Mes, UrbanizacionId, FilterNombre, pageSize, skip, out nroTotalRegistros);

            return Json(new { draw = draw, recordsFiltered = nroTotalRegistros, recordsTotal = nroTotalRegistros, data = lecturas }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult UpdateProcessLectura(ValidateLectura objValidate)
		{
            int Annio = objValidate.Annio;
            int Mes = objValidate.Mes;
            int UrbanizacionId = objValidate.UrbanizacionId;

            int newY = Annio;
            int newM = Mes + 1;
        
            var dataNextYar = _lecturaService.ValidateNextYearUpdateLectura(Annio, Mes, UrbanizacionId);
            var existNextYear = dataNextYar.Count();
            var dataFromLecturaActually = _lecturaService.ValidateValueNoNullable(Annio, Mes, UrbanizacionId);
            if(existNextYear >0)
			{
                newM = newM - 12;
                newY = Annio + 1;
			}
            decimal? value = 0;
            int ClienteId = 0;

            foreach (var item in dataFromLecturaActually)
			{
                value = item.CantidadLectura;
                ClienteId = item.ClienteId;
                //var dataCliente = _clienteService.GetById(item.ClienteId);
                //if (dataCliente.Count()>0)
                //{
                    
                //}
                var updateLectura = new UpdateLectura()
                {
                    Annio = newY,
                    Mes = newM,
                    ClienteId = ClienteId,
                   
                    CantidadLecturaActualizar = value
                };

                _lecturaService.UpdateProcessLectura(updateLectura);
            }

            return Json(new { mensaje = "success"}, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public JsonResult ExistLectura(ValidateLectura objValidate)
		{
            int Annio = objValidate.Annio;
            int Mes = objValidate.Mes;
            int UrbanizacionId = objValidate.UrbanizacionId;
            
            var dato = _lecturaService.CheckIfExistLectura(Annio, Mes, UrbanizacionId);
            var nroRegistros = dato.Count();
            
            return Json(new { mensaje = nroRegistros }, JsonRequestBehavior.AllowGet);
            
        }
        [HttpPost]
        public JsonResult ProcesarLectura(ValidateLectura objValidate)
		{

            //Modificar este metodo , que de acuerdo al cliente , si llega al tope maximo reiniciar a 0
            int Annio = objValidate.Annio;
            int Mes = objValidate.Mes;
            int UrbanizacionId = objValidate.UrbanizacionId;
            
            var output = "";
            var datos = _ciclosService.EnableToNextPrecess(Annio, Mes);
            var datosnullable = _lecturaService.ValidateNullRow(Annio, Mes, UrbanizacionId);
            

            if (datos.Count() == 0)
			{
                output = "10";
			}
			else if (datosnullable.Count() != 0)
			{
				output = "15";
			}
			else
			{
                int nextM = 0;
                int nextY = 0;
                decimal? cantAntigua = 0;
                var netxtYM = _ciclosService.EnableToNextPrecess(Annio, Mes);
                foreach (var item in netxtYM)
                {
                    nextY = item.Annio;
                    nextM = item.Mes;
                }
                int? ClienteId = 0;
                var objLectura = _lecturaService.ValidateValueNoNullable(Annio, Mes, UrbanizacionId);
                foreach (var item in objLectura)
                {
                    var updateLecturaProcess = new UpdateLecturaProcess()
                    {
                        Annio = Annio,
                        Mes = Mes,
                        ClienteId = item.ClienteId,
                        Procesado = 1
                    };
                    _lecturaService.UpdateLecturaProcesada(updateLecturaProcess);
                    //var dataCliente = _clienteService.GetById(item.ClienteId);
                    cantAntigua = item.CantidadLectura;
                    //validar 
                    //if (cantAntigua > dataCliente.First().CapacidadMaxima)
                    //{
                    //    cantAntigua = item.CantidadLectura - dataCliente.First().CapacidadMaxima;
                    //}
                    item.CantidadLecturaAntigua = cantAntigua;
                    item.Annio = nextY;
                    item.Mes = nextM;
                    item.CantidadLectura = 0;
                    item.Consumo = 0;
                    item.Promedio = 0;
                    item.FechaRegistro = DateTime.Now;
                    item.Actualizado = 0;
                    _lecturaService.Create(item);

                }
                output = "success";
			}
            
            return Json(new { msg = output }, JsonRequestBehavior.AllowGet);

		}
        [HttpPost]
        public JsonResult ValidateEnableNextMonth(int? Annio, int? Mes)
		{

            var datos = _ciclosService.EnableToNextPrecess(Annio, Mes);
            var count = datos.Count();
            return Json(new { mensaje = count }, JsonRequestBehavior.AllowGet);
		}

        [HttpPost]
        public JsonResult ValidateNullableRow(ValidateLectura objValidate)
		{
            int Annio = objValidate.Annio;
            int Mes = objValidate.Mes;
            int UrbanizacionId = objValidate.UrbanizacionId;
            var datos = _lecturaService.ValidateValueNoNullable(Annio, Mes, UrbanizacionId);
            var msg = 0;
            foreach(var item in datos)
			{
                if(item.CantidadLectura == 0)
				{
                    msg = 1;
				}
			}
            return Json(new { mensaje = msg }, JsonRequestBehavior.AllowGet);
		}

        //EN MODIFICACION
        [HttpPost]
        public JsonResult UpdateDataExistLectura(Lectura objLectura)
		{
            // Cuando procesa la lectura
            
            int ClienteId = objLectura.ClienteId;
            var top6 = _lecturaService.GetFirst6Data(ClienteId);
            var cliente = _clienteService.GetById(ClienteId);
            var top6Count = top6.Count();

            if (objLectura.CantidadLectura == 0)
                objLectura.CantidadLectura = objLectura.CantidadLecturaAntigua;

            var consumo = objLectura.CantidadLectura - objLectura.CantidadLecturaAntigua;
            if (consumo<0)
            {
                consumo= cliente.First().CapacidadMaxima- objLectura.CantidadLecturaAntigua+ objLectura.CantidadLectura;
            }

            objLectura.Consumo = consumo;
            objLectura.FechaRegistro = DateTime.Now;
            decimal? c = 0;

            if(top6Count >5)
			{
                foreach(var items in top6)
				{
                    var value = items.Consumo;
                    c += value;
				}

                objLectura.Promedio = (c / 6);
			}
            _lecturaService.UpdateDataExistLectura(objLectura);

            var facturaExistente = _facturaService.ValidateIfExists(objLectura.Annio, objLectura.Mes, ClienteId);
            var FacturacionId = facturaExistente.First().FacturacionId;
            
            int CategoriaId = cliente.First().CategoriaId;
            int ServicioId = cliente.First().ServicioId;

            //Reglas de negocio
            var tarifarioItem = _tarifarioService.GetDataTarifario(CategoriaId, objLectura.Consumo);
            if (facturaExistente != null && facturaExistente.Count()!=0)
            {
                    if (ServicioId == (int)Servicios.AguaAlcantarillado)
                    {
                        var objFacturacion = new Facturacion()
                        {
                            FacturacionId = FacturacionId,
                            ClienteId = ClienteId,
                            Annio = objLectura.Annio,
                            Mes = objLectura.Mes,
                            SubTotal = (objLectura.Consumo * tarifarioItem.TarifaAgua + objLectura.Consumo * tarifarioItem.TarifaAlcantarillado) + tarifarioItem.CargoFijo,
                            Total = (objLectura.Consumo * tarifarioItem.TarifaAgua + objLectura.Consumo * tarifarioItem.TarifaAlcantarillado) + tarifarioItem.CargoFijo,
                            EstadoPagado = (int)EstadoPay.Pendiente

                        };
                        _facturaService.UpdateDataExistFactura(objFacturacion);
                    }
                    if (ServicioId == (int)Servicios.Agua)
                    {
                        var objFacturacion = new Facturacion()
                        {
                            FacturacionId = FacturacionId,
                            ClienteId = ClienteId,
                            Annio = objLectura.Annio,
                            Mes = objLectura.Mes,
                            SubTotal = (objLectura.Consumo * tarifarioItem.TarifaAgua) + tarifarioItem.CargoFijo,
                            Total = (objLectura.Consumo * tarifarioItem.TarifaAgua) + tarifarioItem.CargoFijo,
                            EstadoPagado = (int)EstadoPay.Pendiente

                        };
                        _facturaService.UpdateDataExistFactura(objFacturacion);
                    }

            }

  

            return Json(new { msg = "success" }, JsonRequestBehavior.AllowGet);


        }
        //EN MODIFICACION
        

        [HttpPost]
        public JsonResult SaveFirstDataLectura(Lectura objLectura)
		{

            int ClienteId = objLectura.ClienteId;
            var top6 = _lecturaService.GetFirst6Data(ClienteId);
            var top6Count = top6.Count();
            var fecchaRegistro = DateTime.Now;
            var consumo = objLectura.CantidadLectura - objLectura.CantidadLecturaAntigua;
            objLectura.Consumo = consumo;
            objLectura.FechaRegistro = fecchaRegistro;
            decimal? c = 0;
            if(top6Count >5)
			{
                foreach(var items in top6)
				{
                    var value = items.Consumo;
                    c += value;
				}

                objLectura.Promedio = (c / 6);
			}

            _lecturaService.Create(objLectura);

            var cliente = _clienteService.GetById(ClienteId);

            int CategoriaId = cliente.First().CategoriaId;
            int ServicioId = cliente.First().ServicioId;


            var tarifarioItem = _tarifarioService.GetDataTarifario(CategoriaId, objLectura.Consumo);

         
                if (ServicioId == (int)Servicios.AguaAlcantarillado)
                {
                    var objFacturacion = new Facturacion()
                    {
                        ClienteId = ClienteId,
                        Annio = objLectura.Annio,
                        Mes = objLectura.Mes,
                        SubTotal = (objLectura.Consumo * tarifarioItem.TarifaAgua + objLectura.Consumo * tarifarioItem.TarifaAlcantarillado)+ tarifarioItem.CargoFijo,
                        Total = (objLectura.Consumo * tarifarioItem.TarifaAgua + objLectura.Consumo * tarifarioItem.TarifaAlcantarillado) + tarifarioItem.CargoFijo,
                        EstadoPagado = (int)EstadoPay.Pendiente

                    };
                    _facturaService.Create(objFacturacion);
                }
                if (ServicioId == (int)Servicios.Agua)
                {
                    var objFacturacion = new Facturacion()
                    {
                        ClienteId = ClienteId,
                        Annio = objLectura.Annio,
                        Mes = objLectura.Mes,
                        SubTotal =( objLectura.Consumo * tarifarioItem.TarifaAgua)+ tarifarioItem.CargoFijo,
                        Total = (objLectura.Consumo * tarifarioItem.TarifaAgua) + tarifarioItem.CargoFijo,
                        EstadoPagado = (int)EstadoPay.Pendiente

                    };
                   _facturaService.Create(objFacturacion);
                }

                         

            return Json(new { msg = "success" }, JsonRequestBehavior.AllowGet);


        }

        [HttpPost]
        public JsonResult ListaLecturaByFilters(int? Annio, int?Mes, int? UrbanizacionId, string FilterNombre)
		{
            var draw = Request.Form.GetValues("draw").FirstOrDefault();
            var start = Request.Form.GetValues("start").FirstOrDefault();
            var length = Request.Form.GetValues("length").FirstOrDefault();

            int pageSize = length != null ? Convert.ToInt32(length) : 0;
            int skip = start != null ? Convert.ToInt32(start) : 0;
            int nroTotalRegistros = 0;

            var lecturas = _lecturaService.ListarClienteLectura(Annio, Mes, UrbanizacionId, FilterNombre, pageSize, skip, out nroTotalRegistros);

            return Json(new { draw = draw, recordsFiltered = nroTotalRegistros, recordsTotal = nroTotalRegistros, data = lecturas }, JsonRequestBehavior.AllowGet);
        }
    }
}