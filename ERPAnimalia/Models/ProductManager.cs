using ERPAnimalia.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;

namespace ERPAnimalia.Models
{
    public class ProductManager
    {
        public static AnimaliaPetShopEntities db { get; set; }

        public ProductManager()
        {
       
            
        }

        public List<ProductModels> GetAllProduct()
        {
            db = Factory.Factory.CreateContextDataBase();
            var listProduct=  db.Product.ToList();
            
            //Create the mapping between AutomobileDM to CarVM
          
            //As a next step define the mapping for the nested Invoice objects
            
          
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

        public ProductModels GetProductById (Guid id)
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

        public List<ProductModels> SortGrid(string currentSort,string sortOrder)
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




        public List<ProductModels> SearchProduct(ProductModels search)
        {
            try
            {
                db = Factory.Factory.CreateContextDataBase();

                var searchProduct = string.Empty;

                var product = db.Product.ToList();

                if (!String.IsNullOrWhiteSpace(search.Codigo)) product = product.Where(u => u.Codigo.Contains(search.Codigo)).ToList();
                if (!String.IsNullOrWhiteSpace(search.Name)) product = product.Where(u => u.Name.Contains(search.Name)).ToList();
                if ((search.IdCategory != null)) product = product.Where(u => u.Category.IdCategory == search.IdCategory).ToList();
                if ((search.IdSubCategory != null)) product = product.Where(u => u.SubCategory.IdSubCategory == search.IdSubCategory).ToList();

                //product = db.Product.Where(b => b.Codigo.Contains(codigo) || b.Name.Contains(name) || b.IdCategory ==category || b.IdSubCategory == subCategory).ToList();

                var productModel = MapperObject.CreateProductList(product);

                return productModel;
            }

            catch (Exception e)
            {

                throw new Exception(e.Message.ToString());
            }
        }
           
    }
}