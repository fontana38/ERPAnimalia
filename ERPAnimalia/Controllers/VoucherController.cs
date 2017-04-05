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

        public VoucherController()
        {
            HeadVoucherManager = Factory.VoucherFactory.CreateVoucherHeadManager();
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
    }
}