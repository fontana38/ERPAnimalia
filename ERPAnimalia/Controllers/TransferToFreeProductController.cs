using ERPAnimalia.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ERPAnimalia.Controllers
{
    [RoutePrefix("Product")]
    public class TransferToFreeProductController : Controller
    {
        public ProductManager TransferToFreeProductManagers { get; set; }
        public ManagerListOfAmount ManagerList { get; set; }

        public TransferToFreeProductController()
        {
            TransferToFreeProductManagers = Factory.Factory.CreateTransferToFreeProductManager();
            ManagerList = Factory.Factory.CreateManagerListOfAmount();
        }

      
        // GET: TransferToFreeProduct
        public ActionResult TransferToFreeProduct( Guid? IdProducto, string sortOrder, string CurrentSort, int? page, int pageSize = 25)
        {
            var productID = IdProducto;
            ViewData["Category"] = TransferToFreeProductManagers.GetCategory();

            ViewData["SubCategory"] = TransferToFreeProductManagers.GetSubCategory();

            page = page > 0 ? page : 1;
            pageSize = pageSize > 0 ? pageSize : 25;

            sortOrder = String.IsNullOrEmpty(sortOrder) ? "date" : sortOrder;


            ViewBag.CurrentSort = sortOrder;

            var model = TransferToFreeProductManagers.SortGrid(sortOrder, CurrentSort);

            IPagedList<ProductModels> productModel = new StaticPagedList<ProductModels>(model, pageSize + 1, 5, 25);
            return View("TransferToFreeProduct", productModel);
        }
    }
}