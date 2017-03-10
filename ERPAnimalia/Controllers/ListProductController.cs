using ERPAnimalia.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ERPAnimalia.Controllers
{


    [RoutePrefix("Cliente/ListProduct")]
    public class ListProductController : Controller
    {
        public IListProduct ListProductManagers { get; set; }

        public ProductManager ProductManagers { get; set; }
        public ManagerListOfAmount ManagerList { get; set; }

        public ListProductController()
        {
            ListProductManagers = Factory.Factory.CreateListProducManager();
        }

        // GET: ListProduct
       
        [HttpPost]
        public ActionResult GetProduct()

        {

            return View();
        }
    }
}