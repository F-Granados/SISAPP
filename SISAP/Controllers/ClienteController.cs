using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SISAP.Core.Interfaces;
using SISAP.Infrastructure.Helper;
using SISAP.Infrastructure.Service;

namespace SISAP.Controllers
{
    public class ClienteController : Controller
    {
        // GET: Cliente

        private readonly IClienteService _clienteService;

        public ClienteController()
        {
            _clienteService = new ClienteService();

        }
        public ActionResult Clientes()
        {
            return View();
        }
        [HttpPost]
        public JsonResult ListarClientes()
        {
            var cliente = SessionHelper.Get<string>("Codigo, Nombre, Apellido");
            var clientes = _clienteService.GetAll();
            return Json(clientes, JsonRequestBehavior.AllowGet);

        }
        public ActionResult RegistrarClientes()
        {
            return View();
        }
    }
    }
