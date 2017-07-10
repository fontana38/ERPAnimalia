using ERPAnimalia.Helper;
using ERPAnimalia.Models;
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
        public ProductManager ProductManagers { get; set; }

        public LoadOrderController()
        {
            _LoadOrderManager = Factory.LoadOrderFacory.CreateOrderManager();
            ProductManagers = Factory.Factory.CreateProducManager();
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

        [HttpGet]
        public JsonResult GetProduct(int? page, int? limit, string sortBy, string direction, string searchString = null)
        {
            int total;


            var records = ProductManagers.GetProductList(page, limit, sortBy, direction, searchString, out total);

            records = _LoadOrderManager.GetRecordsNewQuantity(records);

            return Json(new { records, total }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Save(string cliente, string date, string fechaPago, int formaDePago, string[]precioCosto,int[]cantidad,Guid[]idProducto,string[] precioVenta)
        {

            _LoadOrderManager.Save(cliente, date, fechaPago, formaDePago, precioCosto, cantidad, idProducto, precioVenta);
            return Json(true);
        }

    }
}