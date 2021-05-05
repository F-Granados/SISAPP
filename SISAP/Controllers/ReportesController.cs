using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SISAP.Controllers
{
    public class ReportesController : Controller
    {
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
    }
}