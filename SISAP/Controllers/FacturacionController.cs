using CrystalDecisions.CrystalReports.Engine;
using SISAP.Core.Interfaces;
using SISAP.Infrastructure.Service;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SISAP.Controllers
{
    public class FacturacionController : Controller
    {
        private readonly IFacturaService facturaService;

        public FacturacionController()
		{
            facturaService = new FacturaService();
		}

        public ActionResult Facturacion()
        {
            return View();
        }

        [HttpPost]
        public JsonResult ListDetalleFacturacion(int? ClienteId)
        {
            var draw = Request.Form.GetValues("draw").FirstOrDefault();
            var start = Request.Form.GetValues("start").FirstOrDefault();
            var length = Request.Form.GetValues("length").FirstOrDefault();

            int pageSize = length != null ? Convert.ToInt32(length) : 0;
            int skip = start != null ? Convert.ToInt32(start) : 0;
            int nroTotalRegistros = 0;

            var dlecturas = facturaService.ListDetalleFacturacion(ClienteId, pageSize, skip, out nroTotalRegistros);

            return Json(new { draw = draw, recordsFiltered = nroTotalRegistros, recordsTotal = nroTotalRegistros, data = dlecturas }, JsonRequestBehavior.AllowGet);
        }
        
        [HttpPost]
        public JsonResult ListMainFactura(int? Annio, int? Mes, int? UrbanizacionId, string FilterNombre)
        {
            var draw = Request.Form.GetValues("draw").FirstOrDefault();
            var start = Request.Form.GetValues("start").FirstOrDefault();
            var length = Request.Form.GetValues("length").FirstOrDefault();

            int pageSize = length != null ? Convert.ToInt32(length) : 0;
            int skip = start != null ? Convert.ToInt32(start) : 0;
            int nroTotalRegistros = 0;

            var lecturas = facturaService.ListFactura(Annio, Mes, UrbanizacionId, FilterNombre, pageSize, skip, out nroTotalRegistros);

            return Json(new { draw = draw, recordsFiltered = nroTotalRegistros, recordsTotal = nroTotalRegistros, data = lecturas }, JsonRequestBehavior.AllowGet);
        }

        #region "Facturacion"

        public ActionResult ReporteFactura(int? id, int idCliente, int mes, int annio, int urb)
        {


            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/ReportesCR"), "rptFacturasMasivas.rpt"));
            rd.SetParameterValue("@usuarioId", id);
            rd.SetParameterValue("@clienteId", idCliente);
            rd.SetParameterValue("@urbanizacionId", urb);
            if (mes == 1)
            {
                mes = 12;
                annio--;
            }
            rd.SetParameterValue("@mes", mes);
            rd.SetParameterValue("@annio", annio);
            Response.Buffer = false;
            Response.ClearContent();
            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            return File(stream, "application/pdf"/*"facturas.pdf"*/);


        }
        // pasos para insertar el reporte
        //1.- Crear el controlador como ejemplo ReporteFacturaMasivo
        //2.- Cambiar los parametros de acuerdo al SP.
        //3.- Enviar de la vista al controlador la cantidad de parametos necesarios ejemplo id,idCliente,Param1,param3,eetc
        //4.- agregar en la vista la siguiente funcionaldiad , tomar como ejemplo "PrintMasivo"
        public ActionResult ReporteFacturaMasivo( int mes, int annio, int urb)
        {


            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/ReportesCR"), "rptFacturasMasivo.rpt"));

            rd.SetParameterValue("@mes", mes);
            rd.SetParameterValue("@annio", annio);
            rd.SetParameterValue("@urbanizacionId", urb);

            Response.Buffer = false;
            Response.ClearContent();
            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            return File(stream, "application/pdf" ,"facturas.pdf");

            

        }
        #endregion
    }
}


#region Ejemplo de reporte en excel 

//ReportDocument rpt = new ReportDocument();
//rpt.Load(Path.Combine(Server.MapPath("~/ReportesCR"), "rptFacturas.rpt"));
//rpt.SetParameterValue("@usuarioId", id);
//rpt.SetParameterValue("@clienteId", idCliente);
//if (mes == 1)
//{
//    mes = 12;
//    annio--;
//}
//rpt.SetParameterValue("@mes", mes);
//rpt.SetParameterValue("@annio", annio);


//Response.Buffer = false;
//Response.ClearContent();
//Response.ClearHeaders();

//try
//{
//    Stream stream = rpt.ExportToStream(CrystalDecisions.Shared.ExportFormatType.Excel);
//    stream.Seek(0, SeekOrigin.Begin);
//    return File(stream, "application/vnd.ms-excel", "eNtsaRegistrationForm.xls");
//}
//catch
//{
//    throw;
//    //return View();
//}
#endregion