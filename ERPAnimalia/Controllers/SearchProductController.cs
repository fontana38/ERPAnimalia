using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ERPAnimalia.Controllers
   
{
    using ERPAnimalia.Models;
    public class SearchProductController : Controller
    {
        
        public ProductManager Manager { get; set; }

        public SearchProductController()
        {
            Manager = Factory.Factory.CreateProducManager();
        }


        // GET: SearchProduct
        public ActionResult SearchProduct ( )
        {
           var  model= Manager.NewProductModel();
            return PartialView("SearchProduct",model); ;
        }
    }
}