using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ERPAnimalia.Controllers
{
    public class HomeController : Controller
    {

        public ActionResult Index()
        {
            
            return View();
        }

        [HttpGet]
        public JsonResult GetSell(int? page, int? limit, string sortBy, string direction, string searchString = null)
        {
            int total=20;
            var manager = Factory.Factory.NewHomeManager();
            var records = manager.GetVentasDiaria();

                foreach (var item in records)
                {
                    item.Dia =(item.Dia != null)? Math.Round(item.Dia.Value, 2):0;
                    item.Mes = Math.Round(item.Mes.Value, 2);
                }
            
            return Json(new { records, total }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}