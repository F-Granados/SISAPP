
using SISAP.Core.Interfaces;
using SISAP.Infrastructure.Helper;
using SISAP.Infrastructure.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SISAP.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly IUsuarioService _usuarioService;
        public UsuarioController()
        {
           _usuarioService = new UsuarioService();
        }
        public ActionResult Usuarios()
        {
            return View();
        }
        [HttpPost]
        public JsonResult ListaUsuarios()
        {
            var user = SessionHelper.Get<string>("Nombre, Apellido, Codigo");
            var usuarios = _usuarioService.GetAll();

            return Json(usuarios, JsonRequestBehavior.AllowGet);
        }
        public ActionResult RegistrarUsuarios()
        {
            return View();
        }
    }
}