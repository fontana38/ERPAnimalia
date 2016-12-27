﻿using ERPAnimalia.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ERPAnimalia.Controllers
{
    [RoutePrefix("Product")]
    public class ProductController : Controller
    {
        public ProductManager ProductManagers { get; set; }
        public ManagerListOfAmount ManagerList { get; set; }

        public ProductController()
        {
            ProductManagers = Factory.Factory.CreateProducManager();
            ManagerList = Factory.Factory.CreateManagerListOfAmount();
        }

        // GET: Product
        [Route("Product")]
        public ActionResult Index(string sortOrder, string CurrentSort, int? page, int pageSize = 25)
        {
            ViewData["Category"] = ProductManagers.GetCategory();

            ViewData["SubCategory"] = ProductManagers.GetSubCategory();

            page = page > 0 ? page : 1;
            pageSize = pageSize > 0 ? pageSize : 25;

            sortOrder = String.IsNullOrEmpty(sortOrder) ? "date" : sortOrder;


            ViewBag.CurrentSort = sortOrder;

            var model = ProductManagers.SortGrid(sortOrder, CurrentSort);
            
            
            int pageNumber = (page ?? 1);
          
            IPagedList<ProductModels> productModel = new StaticPagedList<ProductModels>(model, pageSize + 1, 5, 25);
            
            return View(productModel);
            
        }

        // GET: Product/Details/5
        public ActionResult Details(int id)
        {

            return View();
        }

        // GET: Product/Create
        public ActionResult Create(ProductModels product)
        {
           product.Category= ProductManagers.GetCategory();
           product.SubCategory = ProductManagers.GetSubCategory();
           product.ListaPrecio = ManagerList.GetListOfAmount();
            var lista = product.ListaPrecio.Find(x => x.IdLitaPrecio== product.IdListaPrecio);
            product.ListaPrecioItem = lista;

           return View("Add",product); 
        }

        
        public ActionResult SaveProduct(ProductModels product)
        {

            ProductManagers.SaveProduct(product);
            return RedirectToAction("Index"); 
        }

        // POST: Product/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Product/Edit/5
        public ActionResult Edit(Guid id)
        {
           var productEdit = ProductManagers.GetProductById(id);
           return View(productEdit);
        }

        // POST: Product/Edit/5
        [HttpPost]
        public ActionResult Edit(Guid id, ProductModels product)
        {
            try
            {
                ProductManagers.EditProduct(product);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Product/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        public ActionResult SearchProduct(ProductModels product)
        {
            ViewData["Category"] = ProductManagers.GetCategory();
            ViewData["SubCategory"] = ProductManagers.GetSubCategory();
            product.Category= ViewData["Category"] as List<CategoryModel>;
            product.SubCategory= ViewData["SubCategory"] as List<SubCategoryModel>;
            var productList = ProductManagers.SearchProduct(product);
            return View("Index",productList);
        }
        // POST: Product/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
