using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SISAP.Controllers
{
    public class LecturaController : Controller
    {
        // GET: Lectura
        public ActionResult Lectura()
        {
            return View();
        }
       public ActionResult LecturaRegistro()
        { 
            return View();
        }
        public ActionResult LecturaHistorial()
        {
            return View();
        }
    }
}