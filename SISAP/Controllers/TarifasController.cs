using SISAP.Core.Entities;
using SISAP.Core.Interfaces;
using SISAP.Infrastructure.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SISAP.Controllers
{
    public class TarifasController : Controller
    {
        private readonly ITarifarioService _tarifarioService;
        public TarifasController()
        {
            _tarifarioService = new TarifarioService();
        }
        // GET: Tarifas
        public ActionResult Tarifas()
        {
            return View();
        }
        [HttpPost]
        public JsonResult ListarTarifas()
        {

            var draw = Request.Form.GetValues("draw").FirstOrDefault();
            var start = Request.Form.GetValues("start").FirstOrDefault();
            var length = Request.Form.GetValues("length").FirstOrDefault();

            int pageSize = length != null ? Convert.ToInt32(length) : 0;
            int skip = start != null ? Convert.ToInt32(start) : 0;
            int nroTotalRegistros = 0;

            var tarifarios = _tarifarioService.ListarTarifario(pageSize, skip, out nroTotalRegistros);

            return Json(new { draw = draw, recordsFiltered = nroTotalRegistros, recordsTotal1 = nroTotalRegistros, data = tarifarios }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult RegistrarTarifario(Tarifario objTarifario)
        {
            _tarifarioService.Save(objTarifario);
            return Json(new { msg = "success" }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult Update(Tarifario objTarifario)
        {
            _tarifarioService.Update(objTarifario);
            return Json(new { msg = "success" }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Delete(int TarifarioId)
        {
            _tarifarioService.Delete(TarifarioId);
            return Json(new { msg = "success" }, JsonRequestBehavior.AllowGet);
        }



    }
}