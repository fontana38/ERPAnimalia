using ERPAnimalia.EntityFramework;
using ERPAnimalia.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERPAnimalia
{
    public static class MapperObject
    {
        public static List<ProductModels> CreateProductList(List<Product> product)
        {
            var list = new List<ProductModels>();
            foreach (var item in product)
            {
                var NewProduct = new ProductModels();
                NewProduct.IdProduct = item.IdProducto;
                NewProduct.Description = item.Desciption;
                NewProduct.Name = item.Name;
                NewProduct.quantity = item.quantity;
                NewProduct.kg = item.Kg;
                NewProduct.barCode = item.BarCode;
                NewProduct.Codigo = item.Codigo;
                list.Add(NewProduct);
            }



            return list;
        }
        public static ProductModels CreateProductModel(Product product)
        {
            
                var NewProduct = new ProductModels();
                NewProduct.IdProduct = product.IdProducto;
                NewProduct.Description = product.Desciption;
                NewProduct.Name = product.Name;
                NewProduct.quantity = product.quantity;
                NewProduct.kg = product.Kg;
                NewProduct.barCode = product.BarCode;
                NewProduct.Codigo = product.Codigo;

            return NewProduct;
        }
        public static Product CreateProductDb(ProductModels product)
        {
             
            var NewProduct = Factory.Factory.CreateProductdb();
           
                
                NewProduct.IdProducto = Guid.NewGuid();
                NewProduct.Desciption = product.Description;
                NewProduct.Name = product.Name;
                NewProduct.quantity = product.quantity;
                NewProduct.Kg = product.kg;
                NewProduct.BarCode = product.barCode;
                NewProduct.Codigo = product.Codigo;

                return NewProduct;
        }

        public static Product EditProductDb(ProductModels product, Product productDb)
        {




            productDb.IdProducto = product.IdProduct;
            productDb.Desciption = product.Description;
            productDb.Name = product.Name;
            productDb.quantity = product.quantity;
            productDb.Kg = product.kg;
            productDb.BarCode = product.barCode;
            productDb.Codigo = product.Codigo;

            return productDb;
        }
    }
}