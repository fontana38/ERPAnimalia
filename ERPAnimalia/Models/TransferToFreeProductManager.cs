using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERPAnimalia.Models
{
    public class TransferToFreeProductManager : ProductManager
    {
        public TransferToFreeProductManager()
        {

        }


        public  List<ProductModels> SortGrid(string currentSort, string sortOrder)
        {
            var model = GetAllProductByFree();

            switch (sortOrder)
            {
                case "codigo":
                    if (sortOrder.Equals(currentSort))
                        model = model.OrderByDescending
                                (p => p.Codigo).ToList();
                    else
                        model = model.OrderBy
                                (p => p.Codigo).ToList();
                    break;
                case "descripcion":
                    if (sortOrder.Equals(currentSort))
                        model = model.OrderByDescending
                                (p => p.Description).ToList();
                    else
                        model = model.OrderBy
                                (p => p.Description).ToList();
                    break;

                case "categoria":
                    if (sortOrder.Equals(currentSort))
                        model = model.OrderByDescending
                                (p => p.CategoryItem.Name).ToList();
                    else
                        model = model.OrderBy
                                (p => p.CategoryItem.Name).ToList();
                    break;
                case "subCategoria":
                    if (sortOrder.Equals(currentSort))
                        model = model.OrderByDescending
                                (p => p.SubCategoryItem.Name).ToList();
                    else
                        model = model.OrderBy
                                (p => p.SubCategoryItem.Name).ToList();
                    break;


                default:
                    model = model.OrderBy(s => s.ListaPrecioItem.FechaInicio).ToList();
                    break;

            }
            return model;

        }


        public List<ProductModels> GetAllProductByFree()
        {
            db = Factory.Factory.CreateContextDataBase();
           
            var listProduct = db.Product.Where(p => p.IdCategory == Convert.ToInt16(Enumeration.Category.Suelto)).ToList();

            //Create the mapping between AutomobileDM to CarVM

            //As a next step define the mapping for the nested Invoice objects


            return MapperObject.CreateProductList(listProduct).ToList();
        }
    }
}