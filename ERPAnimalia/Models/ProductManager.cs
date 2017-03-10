using ERPAnimalia.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using System.Web.Http.ModelBinding;

namespace ERPAnimalia.Models
{
    public  class ProductManager
    {
        public static AnimaliaPetShopEntities db { get; set; }

        public TransferToFreeProductManager ProductOpenManagers { get; set; }

        public ProductManager()
        {
       
            
        }

        public List<ProductModels> GetAllProduct()
        {
            db = Factory.Factory.CreateContextDataBase();
            var listProduct=  db.Product.ToList();
 
            return MapperObject.CreateProductList(listProduct).ToList();
        }

        public void AddProduct(Product product)
        {
            try
            {
                db = Factory.Factory.CreateContextDataBase();
                db.Product.Add(product);
                
                db.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message.ToString());
            }
            
        }

        public void CreateProduct(ProductModels product)
        {
            try
            {
                db = Factory.Factory.CreateContextDataBase();
                var productDb = MapperObject.CreateProductDb(product);
                db.Product.Add(productDb);

                db.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        public void SaveProduct(ProductModels product)
        {
            using (var context = new AnimaliaPetShopEntities())
            {
                using (var dbContextTransaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        var productDb = MapperObject.CreateProductDb(product);
                        
                        var managerList = Factory.Factory.CreateManagerListOfAmount();
                        product.ListaPrecioItem.FechaInicio = DateTime.Now.Date;
                        product.ListaPrecioItem.FechaFinal = DateTime.Now.Date.AddYears(1);
                        product.ListaPrecioItem = managerList.AddList(product, productDb);
                        
                        //AddNewProduct
                        AddProduct(productDb);

                        context.SaveChanges();

                        dbContextTransaction.Commit();
                    }
                    catch (Exception)
                    {
                        dbContextTransaction.Rollback();
                        throw;
                    }
                }
            }
        }
            
     
        public void DeleteProduct(ProductModels product)
        {
            db = Factory.Factory.CreateContextDataBase();
            var deleteProduct =db.Product.Find(product);
            db.Product.Remove(deleteProduct);
            db.SaveChanges();

        }

        public List<CategoryModel> GetCategoryList()
        {
            db = Factory.Factory.CreateContextDataBase();
            var CategoryList = db.Category.ToList();
            var map = MapperObject.CreateCategoryList(CategoryList);
            return map;

        }

        public List<SubCategoryModel> GetSubCategoryList()
        {
            db = Factory.Factory.CreateContextDataBase();
            var CategoryList = db.SubCategory.ToList();
            var map = MapperObject.CreateSubCategoryList(CategoryList);
            return map;

        }

        public virtual ProductModels GetProductById (Guid id)
        {
            db = Factory.Factory.CreateContextDataBase();
            var  productById = db.Product.Find(id);
            var  productMap =MapperObject.CreateProductModel(productById);
            productMap.Category = GetCategoryList();
            productMap.SubCategory = GetSubCategoryList();
            return productMap;
        }

        public void EditProduct(ProductModels product)
        {
            db = Factory.Factory.CreateContextDataBase();
            var id = product.IdProducto;
            var productById = db.Product.Find(id);
            var productDb = MapperObject.EditProductDb(product, productById);

            db.SaveChanges();

        }

        public List<CategoryModel> GetCategory()
        {
            db = Factory.Factory.CreateContextDataBase();
            var category = db.Category.ToList();
           
            return MapperObject.CreateCategoryList(category);
        }

        public List<SubCategoryModel> GetSubCategory()
        {
            db = Factory.Factory.CreateContextDataBase();
            var subCategory = db.SubCategory.ToList();

            return MapperObject.CreateSubCategoryList(subCategory);
        }

        public ProductModels NewProductModel()
        {
           return Factory.Factory.NewProductModel();
        }

        public virtual List<ProductModels> SortGrid(string currentSort,string sortOrder)
        {
            var model = GetAllProduct();

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
                case "descripcion1":
                    if (sortOrder.Equals(currentSort))
                        model = model.OrderByDescending
                                (p => p.Descripcion1).ToList();
                    else
                        model = model.OrderBy
                                (p => p.Descripcion1).ToList();
                    break;
                   
                case "Categoria":
                    if (sortOrder.Equals(currentSort))
                        model = model.OrderByDescending
                                (p => p.CategoryItem.Name).ToList();
                    else
                        model = model.OrderBy
                                (p => p.CategoryItem.Name).ToList();
                    break;
                case "Descripcion2":
                    if (sortOrder.Equals(currentSort))
                        model = model.OrderByDescending
                                (p => p.Descripcion2).ToList();
                    else
                        model = model.OrderBy
                                (p => p.Descripcion2).ToList();
                    break;
                case "Cantidad":
                    if (sortOrder.Equals(currentSort))
                        model = model.OrderByDescending
                                (p => p.Cantidad).ToList();
                    else
                        model = model.OrderBy
                                (p => p.Cantidad).ToList();
                    break;
                case "Presentacion":
                    if (sortOrder.Equals(currentSort))
                        model = model.OrderByDescending
                                (p => p.Presentacion).ToList();
                    else
                        model = model.OrderBy
                                (p => p.Presentacion).ToList();
                    break;
                case "RentabilidadPesos":
                    if (sortOrder.Equals(currentSort))
                        model = model.OrderByDescending
                                (p => p.RentabilidadPesos).ToList();
                    else
                        model = model.OrderBy
                                (p => p.Presentacion).ToList();
                    break;


                default:
                    model = model.OrderBy(s => s.ListaPrecioItem.FechaInicio).ToList(); 
                    break;
                
            }
            return model;
           
        }

        public List<ProductModels> SearchProduct(ProductModels search)
        {
            try
            {
                db = Factory.Factory.CreateContextDataBase();

                var searchProduct = string.Empty;

                var product = db.Product.ToList();

                if (!String.IsNullOrWhiteSpace(search.Codigo)) product = product.Where(u => u.Codigo.Contains(search.Codigo)).ToList();
                if (!String.IsNullOrWhiteSpace(search.Descripcion1)) product = product.Where(u => u.Descripcion1.Contains(search.Descripcion1)).ToList();
                if ((search.IdCategory != 0)) product = product.Where(u => u.Category.IdCategory == search.IdCategory).ToList();
                if ((search.Descripcion2 != null)) product = product.Where(u => u.Descripcion2 == search.Descripcion2).ToList();

                //product = db.Product.Where(b => b.Codigo.Contains(codigo) || b.Name.Contains(name) || b.IdCategory ==category || b.IdSubCategory == subCategory).ToList();

                var productModel = MapperObject.CreateProductList(product);
                return productModel;
            }

            catch (Exception e)
            {

                throw new Exception(e.Message.ToString());
            }
        }

        public List<ProductModels> GetProductList(int? page, int? limit, string sortBy, string direction, string searchString, out int total)
        {
            db = Factory.Factory.CreateContextDataBase();
            var productList = db.Product.ToList();

          

            var map = MapperObject.CreateProductList(productList);

            if (!string.IsNullOrWhiteSpace(searchString))
            {
                map = map.Where(p => p.Codigo.Contains(searchString) || p.Descripcion1.Contains(searchString)).ToList();
            }
            total = productList.Count();
            var productQueryable = map.AsQueryable();

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

        public void RemoveModelView(ModelStateDictionary modelState)
        {
            modelState.Remove("Codigo");
            modelState.Remove("Description");
            modelState.Remove("quantity");
            modelState.Remove("IdCategory");
            modelState.Remove("IdSubCategory");
        }
           
    }
}