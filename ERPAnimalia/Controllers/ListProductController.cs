using ERPAnimalia.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ERPAnimalia.Controllers
{


    [RoutePrefix("Producto")]
    public class ListProductController : Controller
    {
        public IListProduct ListProductManagers { get; set; }

        public ProductManager ProductManagers { get; set; }

        public List<ProductModels> records { get; set; }

     

        public ListProductController()
        {
            ListProductManagers = Factory.Factory.CreateListProducManager();
            ProductManagers = Factory.Factory.CreateProducManager();
        }

        // GET: ListProduct
        [Route("Index")]
        public ActionResult Index()
        {
            return View();
        }
        // GET: ListProduct
        [Route("Suelto")]
        public ActionResult Suelto ()
        {
            return View("/views/ListProduct/PassToLoose.cshtml");
        }

        [HttpGet]
        public JsonResult GetProduct(int? page, int? limit, string sortBy, string direction, string searchString = null)
        {
            int total;
            sortBy =(sortBy==null) ? "Codigo" : sortBy;
            direction = (direction == null) ? "asc" : direction;
            var records = ProductManagers.GetProductList(page, limit, sortBy, direction, searchString, out total);

            return Json(new { records, total }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetProductBug(int? page, int? limit, string sortBy, string direction, string searchString = null)
        {
           
            int total=0;
            sortBy = (sortBy == null) ? "Codigo" : sortBy;
            direction = (direction == null) ? "asc" : direction;
            var records = ProductManagers.GetProductList(page, limit, sortBy, direction, searchString, out total);
            
          
            records = ProductManagers.GetProducBug(records);
            foreach (var item in records)
            {
                item.PrecioCosto = Math.Round(item.PrecioCosto.Value, 2);
                item.PrecioVenta = Math.Round(item.PrecioVenta.Value, 2);
            }
            return Json(new { records, total }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetProductLoose(int? page, int? limit, string sortBy, string direction, string searchString = null)
        {
            int total=0;
            sortBy = (sortBy == null) ? "Codigo" : sortBy;
            direction = (direction == null) ? "asc" : direction;
            var records = ProductManagers.GetProductList(page, limit, sortBy, direction, searchString, out total);
            records = ProductManagers.GetProducLooseList(records);
            foreach (var item in records)
            {
                item.PrecioCosto = Math.Round(item.PrecioCosto.Value, 2);
                item.PrecioVenta = Math.Round(item.PrecioVenta.Value, 2);
            }

            return Json(new { records, total }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Save(Guid? idBug, Guid? idLoose, string precioCosto,  string precioVenta)
        {
            ProductManagers.SaveProductToLoose(idBug, idLoose, precioCosto,  precioVenta);
            return Json(true);
        }


    }
}