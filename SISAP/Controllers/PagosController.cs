using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SISAP.Controllers
{
    public class PagosController : Controller
    {
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
    }
}