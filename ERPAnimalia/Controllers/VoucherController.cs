using ERPAnimalia.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ERPAnimalia.Controllers
{

    [RoutePrefix("Factura")]
    public class VoucherController : Controller
    {
        public IVoucherHeadManager HeadVoucherManager { get; set; }
        public IVoucherDetailManager VoucherDetailManager { get; set; }

        public VoucherController()
        {
            HeadVoucherManager = Factory.VoucherFactory.CreateVoucherHeadManager();
            VoucherDetailManager = Factory.VoucherFactory.CreateVoucherDetailManager();
        }
        // GET: Voucher
        public ActionResult Index()
        {
            return View();
        }

        // GET: Voucher
        [Route("CrearFactura")]
        public ActionResult HeadVoucher()
        {
         
            var voucherModel =Factory.VoucherFactory.CreateVoucherHeadModel();
            var voucherDetailModel = Factory.VoucherFactory.CreateVoucherDetailModel();

            return View("~/Views/Voucher/Voucher.cshtml");
        }

        // GET: Voucher
        [HttpPost]
        public JsonResult GetClient(string term)
        {
            
            var voucherModel = Factory.VoucherFactory.CreateVoucherHeadModel();
            var listClient = HeadVoucherManager.GetClient();
            voucherModel.clientModel = listClient;

            var clientName = (from N in listClient
                              where N.Nombre.StartsWith(term) || N.Apellido.StartsWith(term)
                              select new {N.NombreCompleto }).ToList();

            return Json(clientName, JsonRequestBehavior.AllowGet);

           
        }

        [HttpPost]
        public JsonResult GetProduct(string term)
        {

           var voucherDetailModel = Factory.VoucherFactory.CreateVoucherDetailModel();
            var listProduct = VoucherDetailManager.GetProduct();
            voucherDetailModel.ProductModel = listProduct;

            var clientName = (from N in listProduct
                              where N.Descripcion1.StartsWith(term) || N.Codigo.StartsWith(term)
                              select new { N.Descripcion1 }).ToList();

            return Json(clientName, JsonRequestBehavior.AllowGet);


        }
    }
}