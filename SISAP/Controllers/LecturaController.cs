using SISAP.Core.Interfaces;
using SISAP.Infrastructure.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SISAP.Controllers
{
    public class LecturaController : Controller
    {
        private readonly ILecturaService _lecturaService;

        public LecturaController()
		{
            _lecturaService = new LecturaService();
		}
        // GET: Lectura
        public ActionResult Lectura()
        {
            return View();
        }

        [HttpPost]
        public JsonResult ListaLecturaByFilters(int? UrbanizacionId)
		{
            var draw = Request.Form.GetValues("draw").FirstOrDefault();
            var start = Request.Form.GetValues("start").FirstOrDefault();
            var length = Request.Form.GetValues("length").FirstOrDefault();

            int pageSize = length != null ? Convert.ToInt32(length) : 0;
            int skip = start != null ? Convert.ToInt32(start) : 0;
            int nroTotalRegistros = 0;

            var lecturas = _lecturaService.ListarClienteLectura(UrbanizacionId, pageSize, skip, out nroTotalRegistros);

            return Json(new { draw = draw, recordsFiltered = nroTotalRegistros, recordsTotal = nroTotalRegistros, data = lecturas }, JsonRequestBehavior.AllowGet);
        }
    }
}