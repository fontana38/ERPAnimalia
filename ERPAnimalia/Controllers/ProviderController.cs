using ERPAnimalia.Interface;
using ERPAnimalia.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ERPAnimalia.Interfaces;
using ERPAnimalia;

namespace ERPAnimalia.Controllers
{
    [RoutePrefix("Proveedor")]
    public class ProviderController : Controller
    {
        public IProviderManager  _providerManager { get; set; }
        public ProductManager _productManager { get; set; }
        public IListProduct ListProductManagers { get; set; }


        public ProviderController()
        {
            _providerManager = Factory.Factory.NewProviderManager();
            _productManager = Factory.Factory.CreateProducManager();
            ListProductManagers = Factory.Factory.CreateListProducManager();
        }
        // GET: Provider
        public ActionResult Index()
        {
            return View("Provider");
        }

        [HttpPost]
        public JsonResult Save(ProviderModel provider)
        {
            _providerManager.SaveProvider(provider);
            return Json(true);
        }

        [HttpGet]  
        public JsonResult GetProvider(int? page, int? limit, string sortBy, string direction, string searchString = null)
        {
            int total;


            var records = _providerManager.GetProvider(page, limit, sortBy, direction, searchString, out total);


            return Json(new { records, total }, JsonRequestBehavior.AllowGet);
        }



        [HttpGet]
        public JsonResult GetProduct(Guid? idProvider,int? page, int? limit, string sortBy, string direction, string searchString = null)
        {
            int total;


            var records = _productManager.GetProductNotSelectedProvider( idProvider,page, limit, sortBy, direction, searchString, out total);


            return Json(new { records, total }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Remove(Guid idProvider)
        {
            _providerManager.DeleteProvider(idProvider);
            return Json(true);
        }

        [HttpGet]
        public JsonResult GetProductByIdProvider(Guid? idProvider, int? page, int? limit, string sortBy, string direction, string searchString = null)
        {
            int total;


            var records = _productManager.GetProductListByIdProvider(idProvider, page, limit, sortBy, direction, searchString, out total);


            return Json(new { records, total }, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult RemoveProduct(Guid idProvider, Guid idProducto)
        {
            _providerManager.DeleteProductProvider(idProvider, idProducto);
            return Json(true);
        }
    }
}