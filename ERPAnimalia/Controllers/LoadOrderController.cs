using ERPAnimalia.Helper;
using ERPAnimalia.Models.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ERPAnimalia.Controllers
{
    [RoutePrefix("Pedido")]
    public class LoadOrderController : Controller
    {
        public LoadOrderManager _LoadOrderManager { get; set; }

        public LoadOrderController()
        {
            _LoadOrderManager = Factory.LoadOrderFacory.CreateOrderManager();
         
         }
        // GET: LoadOrder
        public ActionResult Index()
        {
            
            var LoadOrderModel = Factory.LoadOrderFacory.CreateVoucherHeadLoadOrder();
            

            ViewData["TipoComprobante"] = "Nota Pedido";
            ViewData["listFormaPago"] = Common.GetFormaPagoLoadOrder();

            return View();
        }
   
        [HttpPost]
        public JsonResult GetProveedor(string term)
        {
            var listProveedor = _LoadOrderManager.GetProveedor();
            var LoadOrderModel = Factory.LoadOrderFacory.CreateVoucherHeadLoadOrder();
            LoadOrderModel.ProveedorModel = listProveedor;



            var proveedorName = (from N in listProveedor
                                 where N.Nombre.StartsWith(term) || N.Apellido.StartsWith(term)
                                 select new { N.NombreCompleto}).ToList();

            return Json(proveedorName, JsonRequestBehavior.AllowGet);

        }

    }
}