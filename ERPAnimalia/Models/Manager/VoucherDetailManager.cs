using ERPAnimalia.EntityFramework;
using ERPAnimalia.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERPAnimalia.Models.Manager
{
    public class VoucherDetailManager : IVoucherDetailManager
    {
        public AnimaliaPetShopEntities db { get; set; }

        public VoucherDetailManager()
        {
            db = Factory.Factory.CreateContextDataBase();
        }

        public List<ProductModels> GetProduct()
        {
            var productList = db.Product.ToList();
            var productListModel = MapperObject.CreateProductList(productList);

            return productListModel;
        }
    }
}