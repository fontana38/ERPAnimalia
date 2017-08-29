using ERPAnimalia.Models;
using OnBarcode.Barcode.BarcodeScanner;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ERPAnimalia.Controllers
{
    [RoutePrefix("Producto")]
    public class ProductController : Controller
    {
        public ProductManager ProductManagers { get; set; }
      

        public ProductController()
        {
            ProductManagers = Factory.Factory.CreateProducManager();          
        }

        //// GET: Product
        //[Route("Product")]
        //public ActionResult Index(string sortOrder, string CurrentSort, int? page, int pageSize = 25)
        //{
        //    ViewData["Category"] = ProductManagers.GetCategory();

        //    ViewData["SubCategory"] = ProductManagers.GetSubCategory();

        //    page = page > 0 ? page : 1;
        //    pageSize = pageSize > 0 ? pageSize : 25;

        //    sortOrder = String.IsNullOrEmpty(sortOrder) ? "date" : sortOrder;


        //    ViewBag.CurrentSort = sortOrder;

        //    //var model = ProductManagers.SortGrid(sortOrder, CurrentSort);

            
        //    int pageNumber = (page ?? 1);
          
        //    IPagedList<ProductModels> productModel = new StaticPagedList<ProductModels>(model, pageSize + 1, 5, 25);
            
        //    return View(productModel);
            
        //}

        // GET: Product/Details/5
        public ActionResult Details(int id)
        {

            return View();
        }

        // GET: Product/Create
        public ActionResult Create(ProductModels product)
        {
            try
            {
                product.Category = ProductManagers.GetCategory();
                product.SubCategory = ProductManagers.GetSubCategory();
               
                ModelState.Remove("Codigo");
                ModelState.Remove("Descripcion1");
                ModelState.Remove("Cantidad");
                ModelState.Remove("IdCategory");
                ModelState.Remove("Descripcion2");
                ModelState.Remove("marca");

                return View("Add", product);
            }
            catch (Exception)
            {
                return View();
            }
               
            
        }

        
        public ActionResult SaveProduct(ProductModels product)
        {
            if (ModelState.IsValid)
            {
                ProductManagers.SaveProduct(product);
                return RedirectToAction("Index", "ListProduct");
            }
            product.Category = ProductManagers.GetCategory();
            product.SubCategory = ProductManagers.GetSubCategory();
           
            return View("Add", product);
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
        [HttpGet]
        public ActionResult Edit( Guid ids)
        {
           
           var productEdit = ProductManagers.GetProductById(ids);
            productEdit.Category = ProductManagers.GetCategory();
            productEdit.SubCategory = ProductManagers.GetSubCategory();
            return View(productEdit);
        }

        // POST: Product/Edit/5
        [HttpPost]
        public ActionResult Edit(Guid? id, ProductModels product)
        {
            try
            {
                ProductManagers.EditProduct(product);

                return RedirectToAction("Index", "ListProduct");
            }
            catch
            {
                return View();
            }
        }

        [HttpPost]
        private ActionResult ReadBarcodeFromFile(string _Filepath)
        {
            String[] barcodes = BarcodeScanner.Scan(_Filepath, BarcodeType.Code39);
            return View();
        }


        // GET: Product/Delete/5
        public ActionResult Delete(Guid id)
        {
            ProductManagers.Delete(id);
            return RedirectToAction("Index", "ListProduct");
        }

        //public ActionResult SearchProduct(ProductModels product)
        //{
        //    ViewData["Category"] = ProductManagers.GetCategory();
        //    ViewData["SubCategory"] = ProductManagers.GetSubCategory();
        //    product.Category= ViewData["Category"] as List<CategoryModel>;
        //    product.SubCategory= ViewData["SubCategory"] as List<SubCategoryModel>;
        //    var productList = ProductManagers.SearchProduct(product);

        //    int pageSize = 25;

            
        //    IPagedList<ProductModels> productModel = new StaticPagedList<ProductModels>(productList, pageSize + 1, 5, 25);
        //    if(productModel.Count == 0)
        //    {
        //        RedirectToAction("Index");
        //    }
           
        //        return View("Index", productModel);
            
            
        //}
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
