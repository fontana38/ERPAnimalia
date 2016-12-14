using ERPAnimalia.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ERPAnimalia.EntityFramework;

namespace ERPAnimalia.Factory
{
    public static class Factory
    {




       

        public static ProductModels NewProduct()
        {
            var NewProduct = new ProductModels();
           
            return NewProduct;
        }

        public static AnimaliaPetShopEntities CreateContextDataBase()
        {
            var db = new AnimaliaPetShopEntities();
            return db;
        }

        public static Product CreateProductdb()
        {
          
            return new Product();
        }


        public static ProductManager CreateProducManager()
        {
            var productManager = new ProductManager();
            return productManager;
        }



    }

}
