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
    }
}