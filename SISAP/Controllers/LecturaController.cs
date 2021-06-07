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
    public class LecturaController : Controller
    {
        private readonly ILecturaService _lecturaService;
        private readonly ICiclosService _ciclosService;

        public LecturaController()
		{
            _lecturaService = new LecturaService();
            _ciclosService = new CiclosService();
		}
        // GET: Lectura
        public ActionResult Lectura()
        {
            return View();
        }
        [HttpPost]
        public JsonResult UpdateProcessLectura(ValidateLectura objValidate)
		{
            int Annio = objValidate.Annio;
            int Mes = objValidate.Mes;
            int UrbanizacionId = objValidate.UrbanizacionId;

            int newY = Annio;
            int newM = Mes + 1;

            var dataNextYar = _lecturaService.ValidateNextYearUpdateLectura(Annio, Mes, UrbanizacionId);
            var existNextYear = dataNextYar.Count();
            var dataFromLecturaActually = _lecturaService.ValidateValueNoNullable(Annio, Mes, UrbanizacionId);
            if(existNextYear >0)
			{
                newY = Annio + 1;
			}
            decimal? value = 0;
            int ClienteId = 0;

            foreach (var item in dataFromLecturaActually)
			{
                value = item.CantidadLectura;
                ClienteId = item.ClienteId;

                var updateLectura = new UpdateLectura()
                {
                    Annio = newY,
                    Mes = newM,
                    ClienteId = ClienteId,
                    CantidadLecturaActualizar = value
                };

                _lecturaService.UpdateProcessLectura(updateLectura);
            }

            return Json(new { mensaje = "success"}, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public JsonResult ExistLectura(ValidateLectura objValidate)
		{
            int Annio = objValidate.Annio;
            int Mes = objValidate.Mes;
            int UrbanizacionId = objValidate.UrbanizacionId;

            var dato = _lecturaService.CheckIfExistLectura(Annio, Mes, UrbanizacionId);
            var nroRegistros = dato.Count();
            
            return Json(new { mensaje = nroRegistros }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ProcesarLectura(ValidateLectura objValidate)
		{
            int Annio = objValidate.Annio;
            int Mes = objValidate.Mes;
            int nextM = 0;
            int nextY = 0;
            decimal? cantAntigua = 0;
            int UrbanizacionId = objValidate.UrbanizacionId;
            var netxtYM = _ciclosService.EnableToNextPrecess(Annio, Mes);
            foreach(var item in netxtYM)
			{
                nextY = item.Annio;
                nextM = item.Mes;
            }

            var objLectura = _lecturaService.ValidateValueNoNullable(Annio, Mes, UrbanizacionId);
            foreach(var item in objLectura)
			{
                cantAntigua = item.CantidadLectura;
                item.CantidadLecturaAntigua = cantAntigua;
                item.Annio = nextY;
                item.Mes = nextM;
                item.CantidadLectura = 0;
                item.Consumo = 0;
                item.Promedio = 0;
                item.FechaRegistro = DateTime.Now;
                _lecturaService.Create(item);

            }
            return Json(new { msg = "success" }, JsonRequestBehavior.AllowGet);

		}

        [HttpPost]
        public JsonResult ValidateEnableNextMonth(int? Annio, int? Mes)
		{

            var datos = _ciclosService.EnableToNextPrecess(Annio, Mes);
            var count = datos.Count();
            return Json(new { mensaje = count }, JsonRequestBehavior.AllowGet);
		}

        [HttpPost]
        public JsonResult ValidateNullableRow(ValidateLectura objValidate)
		{
            int Annio = objValidate.Annio;
            int Mes = objValidate.Mes;
            int UrbanizacionId = objValidate.UrbanizacionId;
            var datos = _lecturaService.ValidateValueNoNullable(Annio, Mes, UrbanizacionId);
            var msg = 0;
            foreach(var item in datos)
			{
                if(item.CantidadLectura == 0)
				{
                    msg = 1;
				}
			}
            return Json(new { mensaje = msg }, JsonRequestBehavior.AllowGet);
		}

        [HttpPost]
        public JsonResult UpdateDataExistLectura(Lectura objLectura)
		{
            var top6 = _lecturaService.GetFirst6Data();
            var top6Count = top6.Count();
            var consumo = objLectura.CantidadLectura - objLectura.CantidadLecturaAntigua;
            objLectura.Consumo = consumo;
            decimal? c = 0;
            if(top6Count >5)
			{
                foreach(var items in top6)
				{
                    var value = items.CantidadLectura;
                    c += value;
				}

                objLectura.Promedio = (c / 6);
			}
            _lecturaService.UpdateDataExistLectura(objLectura);

            return Json(new { msg = "success" }, JsonRequestBehavior.AllowGet);


        }
        
        [HttpPost]
        public JsonResult SaveFirstDataLectura(Lectura objLectura)
		{
            var top6 = _lecturaService.GetFirst6Data();
            var top6Count = top6.Count();
            var fecchaRegistro = DateTime.Now;
            var consumo = objLectura.CantidadLectura - objLectura.CantidadLecturaAntigua;
            objLectura.Consumo = consumo;
            objLectura.FechaRegistro = fecchaRegistro;
            decimal? c = 0;
            if(top6Count >5)
			{
                foreach(var items in top6)
				{
                    var value = items.CantidadLectura;
                    c += value;
				}

                objLectura.Promedio = (c / 6);
			}

            _lecturaService.Create(objLectura);

            return Json(new { msg = "success" }, JsonRequestBehavior.AllowGet);


        }

        [HttpPost]
        public JsonResult ListaLecturaByFilters(int? Annio, int?Mes, int? UrbanizacionId, string FilterNombre)
		{
            var draw = Request.Form.GetValues("draw").FirstOrDefault();
            var start = Request.Form.GetValues("start").FirstOrDefault();
            var length = Request.Form.GetValues("length").FirstOrDefault();

            int pageSize = length != null ? Convert.ToInt32(length) : 0;
            int skip = start != null ? Convert.ToInt32(start) : 0;
            int nroTotalRegistros = 0;

            var lecturas = _lecturaService.ListarClienteLectura(Annio, Mes, UrbanizacionId, FilterNombre, pageSize, skip, out nroTotalRegistros);

            return Json(new { draw = draw, recordsFiltered = nroTotalRegistros, recordsTotal = nroTotalRegistros, data = lecturas }, JsonRequestBehavior.AllowGet);
        }
    }
}