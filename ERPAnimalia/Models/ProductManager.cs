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
            
           return MapperObject.CreateProductList(list).ToList();
        }

        public void AddProduct(Product product)
        {
            
            db.Product.Add(product);
            db.SaveChanges();

        }

        public void CreateProduct(ProductModels product)
        {
            try
            {
                var productDb = MapperObject.CreateProductDb(product);
                db.Product.Add(productDb);
                db.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        public Product SaveProduct(ProductModels product)
        {
            var productDb = MapperObject.CreateProductDb(product);
            Product productById = null;

            if (productDb != null && productDb.IdProducto != null)
            {
                //AddNewProduct
                AddProduct(productDb);
            }
            else
            {
                //Edit
                
            }

            return productById;
        }

        public void DeleteProduct(ProductModels product)
        {
            var deleteProduct =db.Product.Find(product);
            db.Product.Remove(deleteProduct);
            db.SaveChanges();

        }

        public ProductModels GetProductById (Guid id)
        {
             var  productById = db.Product.Find(id);
           return MapperObject.CreateProductModel(productById);
        }

        public void EditProduct(ProductModels product)
        {
            var id =product.IdProduct;
            var productById = db.Product.Find(id);
            var productDb = MapperObject.EditProductDb(product, productById);
            
            db.SaveChanges();
        }
    }
}