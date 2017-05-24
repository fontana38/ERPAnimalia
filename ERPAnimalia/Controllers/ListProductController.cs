﻿using ERPAnimalia.Models;
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

        [HttpGet]
        public JsonResult GetProduct(int? page, int? limit, string sortBy, string direction, string searchString = null)
        {
            int total;
            var records = ProductManagers.GetProductList(page, limit, sortBy, direction, searchString, out total);

            return Json(new { records, total }, JsonRequestBehavior.AllowGet);
        }
    }
}