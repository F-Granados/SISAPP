using CrystalDecisions.CrystalReports.Engine;
using SISAP.Core.Entities;
using SISAP.Core.Enum;
using SISAP.Core.Interfaces;
using SISAP.Infrastructure.Service;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SISAP.Controllers
{
    public class PagosController : Controller
    {
        private readonly IPagoService _pagoService;

        public PagosController()
		{
            _pagoService = new PagoService();
		}
        // GET: Pagos
        public ActionResult Pagos()
        {
            return View();
        }
        public ActionResult PagosCliente()
        {
            return View();
        }
        public ActionResult PagosRecibo()
        {
            return View();
        }
        [HttpPost]
        public JsonResult Pay(int? ClienteId)
        {
            var draw = Request.Form.GetValues("draw").FirstOrDefault();
            var start = Request.Form.GetValues("start").FirstOrDefault();
            var length = Request.Form.GetValues("length").FirstOrDefault();

            int pageSize = length != null ? Convert.ToInt32(length) : 0;
            int skip = start != null ? Convert.ToInt32(start) : 0;
            int nroTotalRegistros = 0;

            var dPagos = _pagoService.GetPay(ClienteId, pageSize, skip, out nroTotalRegistros);

            return Json(new { draw = draw, recordsFiltered = nroTotalRegistros, recordsTotal = nroTotalRegistros, data = dPagos }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Pagar(Pago objPago)
		{
            objPago.FechaPago = DateTime.Now;
            objPago.EstadoPago = (int)EstadoPay.Pagado;
            objPago.EstadoPagoDesc = "Pagado";

            _pagoService.Pagar(objPago);

            return Json(new { mensaje = "success" }, JsonRequestBehavior.AllowGet);
		}
        [HttpPost]
        public JsonResult PayAllMonths(int? ClienteId)
        {
            var dPagos = _pagoService.PayAllMonth(ClienteId);


            int[] retorno = new int[dPagos.Count()];


            int i = 0;
            foreach (var item in dPagos)
            {
                var objPago = new Pago()
                {
                    ClienteId = item.ClienteId,
                    Total = item.Total,
                    PeriodoAnnio = item.Annio,
                    PeriodoMes = item.Mes,
                    FacturacionId = item.FacturacionId,
                    FechaPago = DateTime.Now,
                    EstadoPago = (int)EstadoPay.Pagado,
                    EstadoPagoDesc = "Pagado",
                    Observaciones = "Pago masivo",
                };
                _pagoService.Pagar(objPago);
                retorno[i] = item.FacturacionId;
                i++;
            }
            return Json(new { mensaje = retorno }, JsonRequestBehavior.AllowGet);

        }

        [HttpPost] 
        public JsonResult TotalPagar(int? ClienteId)
		{
            var dPagos = _pagoService.PayAllMonth(ClienteId);
            decimal? deudaT = 0;

            foreach(var item in dPagos)
			{
                deudaT += item.Total;
			}

            return Json(deudaT, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ListPayByCliente(int? ClienteId)
		{
            var draw = Request.Form.GetValues("draw").FirstOrDefault();
            var start = Request.Form.GetValues("start").FirstOrDefault();
            var length = Request.Form.GetValues("length").FirstOrDefault();

            int pageSize = length != null ? Convert.ToInt32(length) : 0;
            int skip = start != null ? Convert.ToInt32(start) : 0;
            int nroTotalRegistros = 0;

            var dPagos = _pagoService.GetClienteDeudor(ClienteId, pageSize, skip, out nroTotalRegistros);

            return Json(new { draw = draw, recordsFiltered = nroTotalRegistros, recordsTotal = nroTotalRegistros, data = dPagos }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult ListMain(int? UrbanizacionId, string FilterNombre)
		{
            var draw = Request.Form.GetValues("draw").FirstOrDefault();
            var start = Request.Form.GetValues("start").FirstOrDefault();
            var length = Request.Form.GetValues("length").FirstOrDefault();

            int pageSize = length != null ? Convert.ToInt32(length) : 0;
            int skip = start != null ? Convert.ToInt32(start) : 0;
            int nroTotalRegistros = 0;

            var dPagos = _pagoService.GetAllCF(UrbanizacionId, FilterNombre, pageSize, skip, out nroTotalRegistros);

            return Json(new { draw = draw, recordsFiltered = nroTotalRegistros, recordsTotal = nroTotalRegistros, data = dPagos }, JsonRequestBehavior.AllowGet);
        }
        #region "Reporte Pago"

        public ActionResult ReportePago(int id, int idCliente, string idPago)
        {

            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/ReportesCR"), "rptPagos.rpt"));
            rd.SetParameterValue("@usuarioId", id);
            rd.SetParameterValue("@clienteId", idCliente);
            rd.SetParameterValue("@pagoId", idPago);
            Response.Buffer = false;
            Response.ClearContent();
            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            return File(stream, "application/pdf" /*"pagos" + idCliente + ".pdf"*/);


        }
        #endregion
    }
}