using SISAP.Core.Entities;
using SISAP.Core.Enum;
using SISAP.Core.Interfaces;
using SISAP.Infrastructure.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SISAP.Controllers
{
    public class RefacturacionController : Controller
    {
        private readonly ILecturaService _lecturaService;
        private readonly ICiclosService _ciclosService;
        private readonly ITarifarioService _tarifarioService;
        private readonly IClienteService _clienteService;
        private readonly IFacturaService _facturaService;
        // GET: Refacturacion

        public RefacturacionController()
        {
            _lecturaService = new LecturaService();
            _ciclosService = new CiclosService();
            _tarifarioService = new TarifarioService();
            _clienteService = new ClienteService();
            _facturaService = new FacturaService();
        }
        public ActionResult Refacturacion()
        {
            return View();
        }
        [HttpPost]
        public JsonResult ListarLectura(string numeroMedidor, string codCliente)
        {

            var draw = Request.Form.GetValues("draw").FirstOrDefault();
            var start = Request.Form.GetValues("start").FirstOrDefault();
            var length = Request.Form.GetValues("length").FirstOrDefault();

            int pageSize = length != null ? Convert.ToInt32(length) : 0;
            int skip = start != null ? Convert.ToInt32(start) : 0;
            int nroTotalRegistros = 0;

            var lecturas = _lecturaService.ListarLecturasPendientesPago(numeroMedidor,codCliente, pageSize, skip, out nroTotalRegistros);
            decimal? cantidadLectura = 0;
            foreach (var item in lecturas)
            {
                cantidadLectura = _lecturaService.ObtenerLecturaAnterior(item.ClienteId, item.LecturaId);
                item.CantidadLecturaAntigua = cantidadLectura == null ? 0 : cantidadLectura;
            }
            return Json(new { draw = draw, recordsFiltered = nroTotalRegistros, recordsTotal = nroTotalRegistros, data = lecturas }, JsonRequestBehavior.AllowGet);

        }


        [HttpPost]
        public JsonResult Actualizar(Lectura objLectura)
        {
            string salida = "";
            string msj = "";
            Lectura lecturaProxima = new Lectura();
            string resultado1 = ActualizarLecturaFactura(objLectura);
            int mes = 0;
            int annio = 0;
            if (resultado1=="01")
            {
                if (objLectura.Mes==12)
                {
                    mes = 1;
                    annio = objLectura.Annio + 1;
                }
                else
                {
                    mes = objLectura.Mes + 1;
                    annio = objLectura.Annio;
                }
                var lecturaProx = _lecturaService.ListarLecturaAnnioMesCliente(annio, mes, objLectura.ClienteId);
                if (lecturaProx != null && lecturaProx.Count()>0)
                {
                    lecturaProxima = _lecturaService.ListarLecturaAnnioMesCliente(annio, mes, objLectura.ClienteId).First();
                    lecturaProxima.CantidadLecturaAntigua = _lecturaService.ObtenerLecturaAnterior(lecturaProxima.ClienteId, lecturaProxima.LecturaId);
                    string resultad2 = ActualizarLecturaFactura(lecturaProxima);
                    salida = resultad2;
                    if (salida == "01")
                        msj = "Se guardo Correctamente!";
                    else
                        msj = "Hubo un error al guardar";
                }
                else
                {
                    salida = resultado1;
                    if (salida == "01")
                        msj = "Se guardo Correctamente!";
                    else
                        msj = "Hubo un error al guardar";
                }
               
            }
            else
            {
                salida = "00";
                   msj = "Hubo un error al guardar";
            }

            return Json(new { msg = msj, errorCode = salida }, JsonRequestBehavior.AllowGet);
        }


        public string ActualizarLecturaFactura(Lectura objLectura)
        {

             
            string resultado = "";
            int ClienteId = objLectura.ClienteId;
            var top6 = _lecturaService.GetFirst6Data(ClienteId);
            var cliente = _clienteService.GetById(ClienteId);
            var top6Count = top6.Count();
            if (objLectura.CantidadLectura == 0)
                objLectura.CantidadLectura = objLectura.CantidadLecturaAntigua;
            var consumo = objLectura.CantidadLectura - objLectura.CantidadLecturaAntigua;
            if (consumo < 0)
            {
                consumo = cliente.First().CapacidadMaxima - objLectura.CantidadLecturaAntigua + objLectura.CantidadLectura;
            }
            objLectura.Consumo = consumo;
            objLectura.FechaRegistro = DateTime.Now;
            decimal? c = 0;
            if (top6Count > 5)
            {
                foreach (var items in top6)
                {
                    var value = items.Consumo;
                    c += value;
                }

                objLectura.Promedio = (c / 6);
            }
            var facturaExistente = _facturaService.ValidateIfExists(objLectura.Annio, objLectura.Mes, ClienteId);
            if (facturaExistente != null && facturaExistente.Count() == 1)
            {
                if (facturaExistente.First().EstadoPagado != 1)
                {
                    DateTime fechaActual = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 01);
                    DateTime fechaFactura = new DateTime(facturaExistente.First().Annio, facturaExistente.First().Mes, 01);
                    decimal restaFechas = MonthDifference(fechaActual, fechaFactura);
                    if (restaFechas <= 5)
                    {
                        _lecturaService.UpdateDataExistLectura(objLectura);
                        var FacturacionId = facturaExistente.First().FacturacionId;
                        int CategoriaId = cliente.First().CategoriaId;
                        int ServicioId = cliente.First().ServicioId;

                        //Reglas de negocio
                        var tarifarioItem = _tarifarioService.GetDataTarifario(CategoriaId, objLectura.Consumo);

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

                        resultado = "01";
                       
                    }
                    else
                    {
                        resultado = "00";
                       
                    }
                }
                else
                {
                    resultado = "00";
                    
                }

            }
            return resultado;
        }

        public decimal MonthDifference(DateTime FechaFin, DateTime FechaInicio)
        {
            return Math.Abs((FechaFin.Month - FechaInicio.Month) + 12 * (FechaFin.Year - FechaInicio.Year));

        }

    }
}