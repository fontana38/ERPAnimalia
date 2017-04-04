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

        private VoucherController()
        {

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
            var listClient = HeadVoucherManager.GetClient();
            var voucherModel =Factory.VoucherFactory.CreateVoucherHeadModel();
            
            return View();
        }

        // GET: Voucher
        [HttpPost]
        public JsonResult GetClient()
        {
            var listClient = HeadVoucherManager.GetClient();
            var voucherModel = Factory.VoucherFactory.CreateVoucherHeadModel();

            return Json(voucherModel, JsonRequestBehavior.AllowGet);
        }
    }
}