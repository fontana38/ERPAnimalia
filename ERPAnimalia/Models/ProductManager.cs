using ERPAnimalia.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERPAnimalia.Models
{
    public class ProductManager
    {
        public static AnimaliaPetShopEntities db { get; set; }

        public ProductManager()
        {
            db = Factory.Factory.CreateContextDataBase();
        }

        public List<ProductModels> GetAllProduct()
        {
           var list=  db.Product.ToList();
            
                return Factory.Factory.CreateProductList(list).ToList();
            
        }

        public void AddProduct(Product product)
        {
            db.Product.Add(product);
            db.SaveChanges();

        }

        public void DeleteProduct(ProductModels product)
        {
            var deleteProduct =db.Product.Find(product);
            db.Product.Remove(deleteProduct);
            db.SaveChanges();

        }
    }
}