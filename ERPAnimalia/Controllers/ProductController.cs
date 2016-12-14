using ERPAnimalia.Models;
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
        public ProductManager Manager { get; set; }

        public ProductController()
        {
            Manager = Factory.Factory.CreateProducManager();
        }

        // GET: Product
        [Route("Product")]
        public ActionResult Index()
        {   
            var model= Manager.GetAllProduct();
            return View(model);
        }

        // GET: Product/Details/5
        public ActionResult Details(int id)
        {

            return View();
        }

        // GET: Product/Create
        public ActionResult Create(ProductModels product)
        {
            
                return View("AddProduct"); 
        }

        
        public ActionResult SaveProduct(ProductModels product)
        {
            Manager.SaveProduct(product);
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
           var productEdit = Manager.GetProductById(id);
           return View(productEdit);
        }

        // POST: Product/Edit/5
        [HttpPost]
        public ActionResult Edit(Guid id, ProductModels product)
        {
            try
            {
                Manager.EditProduct(product);

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
