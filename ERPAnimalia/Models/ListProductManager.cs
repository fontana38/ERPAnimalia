using ERPAnimalia.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ERPAnimalia.Models
{
    
    public class ListProductManager : IListProduct
    {
        public AnimaliaPetShopEntities db { get; set; }

        public ListProductManager()
        {
            db = Factory.Factory.CreateContextDataBase();

        }
        public List<ProductModels> GetProduct(int? page, int? limit, string sortBy, string direction, string searchString, out int total)
        {
            var productList = db.Product.ToList();

           

            var product = MapperObject.CreateProductList(productList);

            if (!string.IsNullOrWhiteSpace(searchString))
            {
                product = product.Where(p => p.Descripcion1.Contains(searchString) || p.Codigo.Contains(searchString)|| p.Descripcion2.Contains(searchString)).ToList();
            }

            total = product.Count();

            var productQueryable = product.AsQueryable();

            if (!string.IsNullOrEmpty(sortBy) && !string.IsNullOrEmpty(direction))
            {

                if (direction.Trim().ToLower() == "asc")
                {
                    productQueryable = SortHelper.OrderBy(productQueryable, sortBy);
                }
                else
                {
                    productQueryable = SortHelper.OrderByDescending(productQueryable, sortBy);
                }
            }

            if (page.HasValue && limit.HasValue)
            {
                int start = (page.Value - 1) * limit.Value;
                productQueryable = productQueryable.Skip(start).Take(limit.Value);
            }

            return productQueryable.ToList();
        }

    }
}
