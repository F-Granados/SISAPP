using SISAP.Core.Interfaces;
using SISAP.Infrastructure.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SISAP.Controllers
{
    public class ReportesController : Controller
    {
        private readonly IReportesService _reportesService;
        public ReportesController ()
		{
            _reportesService = new ReportesService();

        }
        // GET: Reportes
        public ActionResult Reportes()
        {
            return View();
        }
        public ActionResult ReportesDeudas()
        {
            return View();
        }
        public ActionResult ReportesDeudasDistrito()
        {
            return View();
        }

        [HttpPost]
        public JsonResult DeudaRuta(int? Annio, int? Mes, int? UrbanizacionId)
		{
             var cantidad = _reportesService.getDeudaRuta(Annio, Mes, UrbanizacionId);
            return Json(new { respuesta = cantidad }, JsonRequestBehavior.AllowGet);
		}
        
        [HttpPost]
        public JsonResult DeudaDistrito(int? Annio)
		{
             var cantidad = _reportesService.getDeudaDistrito(Annio);
            return Json(new { respuesta = cantidad }, JsonRequestBehavior.AllowGet);
		}
        
        [HttpPost]
        public JsonResult IngresosAnuales(int? Annio)
		{
             var cantidad = _reportesService.getIngresoAnual(Annio);
            return Json(new { respuesta = cantidad }, JsonRequestBehavior.AllowGet);
		}
                
        [HttpPost]
        public JsonResult IngresosMensuales(int? Annio, int? Mes)
		{
             var cantidad = _reportesService.getIngresoMensual(Annio, Mes);
            return Json(new { respuesta = cantidad }, JsonRequestBehavior.AllowGet);
		}
        
        [HttpPost]
        public JsonResult AllLecturas(int? Annio, int? Mes)
		{
             var cantidad = _reportesService.getProcessLectura(Annio, Mes);
            return Json(new { respuesta = cantidad }, JsonRequestBehavior.AllowGet);
		}



        [HttpPost]
        public JsonResult ListMainReporte(int? Annio, int? Mes, int? UrbanizacionId, string FilterNombre)
        {
            var draw = Request.Form.GetValues("draw").FirstOrDefault();
            var start = Request.Form.GetValues("start").FirstOrDefault();
            var length = Request.Form.GetValues("length").FirstOrDefault();

            int pageSize = length != null ? Convert.ToInt32(length) : 0;
            int skip = start != null ? Convert.ToInt32(start) : 0;
            int nroTotalRegistros = 0;

            var lecturas = _reportesService.ListReporte(Annio, Mes, UrbanizacionId, FilterNombre, pageSize, skip, out nroTotalRegistros);

            return Json(new { draw = draw, recordsFiltered = nroTotalRegistros, recordsTotal = nroTotalRegistros, data = lecturas }, JsonRequestBehavior.AllowGet);
        }
    }
}