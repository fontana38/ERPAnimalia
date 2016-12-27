using ERPAnimalia.EntityFramework;
using ERPAnimalia.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ERPAnimalia.Factory;
using AutoMapper;
using ERPAnimalia.Mapper;

namespace ERPAnimalia
{
    public  class MapperObject
    {
        

        public static List<ProductModels> CreateProductList(List<Product> product)
        {
            try
            {
                var mapper = AutoMapperConfig.MapperConfiguration.CreateMapper();
                var list = Factory.Factory.CreateListProduct();

                foreach (var item in product)
                {
                    var category = CreateCategory(item.Category);
                    var productMap = mapper.Map<ProductModels>(item);
                    var subCategoryMap = CreateSubCategory(item.Category.SubCategory);
                    var listPriceMap = CreateListMap(item.ListaPrecio);
                    productMap.CategoryItem = category;
                    productMap.SubCategoryItem = subCategoryMap;
                    productMap.ListaPrecioItem = listPriceMap;



                    list.Add(productMap);
                }


                return list;
            }
            catch (Exception e)
            {

                throw new Exception(e.Message.ToString());
            }
       
        }

        public static CategoryModel CreateCategory(Category category)
        {
            try
            {
                var mapper = AutoMapperConfig.MapperConfiguration.CreateMapper();
                var categoryMap = mapper.Map<CategoryModel>(category);
                return categoryMap;
            }
            catch (Exception e)
            {

                  throw new Exception(e.Message.ToString()); 
            }
             
        }

        public static SubCategoryModel CreateSubCategory(SubCategory subCategory)
        {
            try
            {
                var mapper = AutoMapperConfig.MapperConfiguration.CreateMapper();

                var subCategoryMap = mapper.Map<SubCategoryModel>(subCategory);

                return subCategoryMap;
            }
            catch (Exception e)
            {

                throw new Exception(e.Message.ToString());
            }
           
        }

        public static List<CategoryModel> CreateCategoryList(List<Category> category)
        {
            try
            {
                var mapper = AutoMapperConfig.MapperConfiguration.CreateMapper();
                var list = Factory.Factory.CreateListCategoryModel();
                list.Add(new CategoryModel { IdCategory = 0, Name = "Select" });

                foreach (var item in category)
                {
                    var categoryMap = mapper.Map<CategoryModel>(item);

                    list.Add(categoryMap);
                }

                return list;

            }
            catch (Exception e)
            {

                throw new Exception(e.Message.ToString());
            }
        }    

        public static List<ListaPrecioModel> CreateListAmountMap(List<ListaPrecio> lista)
        {
            try
            {
                var mapper = AutoMapperConfig.MapperConfiguration.CreateMapper();
                var List = Factory.Factory.NewListaPrecioModelCollection();
                foreach (var item in List)
                {
                    var listaMap = mapper.Map<ListaPrecioModel>(item);

                    List.Add(listaMap);
                }

                return List;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message.ToString());
            }
            
        }

        public static ListaPrecioModel CreateListMap(ListaPrecio lista)
        {
            try
            {
                var mapper = AutoMapperConfig.MapperConfiguration.CreateMapper();
                return mapper.Map<ListaPrecioModel>(lista);
            }
            catch (Exception e)
            {

                throw new Exception(e.Message.ToString());
            }
            
        }
        public static ListaPrecio CreateListMapDb(ListaPrecioModel lista)
        {
            try
            {
                var mapper = AutoMapperConfig.MapperConfiguration.CreateMapper();
                return mapper.Map<ListaPrecio>(lista);
            }
            catch (Exception e)
            {

                throw new Exception(e.Message.ToString());
            }
           
        }

        public static List<SubCategoryModel> CreateSubCategoryList(List<SubCategory> subCategory)
        {
            try
            {
                var mapper = AutoMapperConfig.MapperConfiguration.CreateMapper();
                var list = Factory.Factory.CreateListSubCategoryModel();
                list.Add(new SubCategoryModel { IdSubCategory = 0, Name = "Select" });
                foreach (var item in subCategory)
                {
                    var subCategoryMap = mapper.Map<SubCategoryModel>(item);
                    list.Add(subCategoryMap);
                }



                return list;
            }
            catch (Exception e)
            {

                throw new Exception(e.Message.ToString());
            }
            
        }

        public static SubCategoryModel CreateSubCategoryModel(Product p)
        {
            var newSubCategory = new SubCategoryModel();
            newSubCategory.IdSubCategory = p.Category.SubCategory.IdSubCategory;
            newSubCategory.Name = p.Category.SubCategory.Name;
            return newSubCategory;
        }

        public static ProductModels CreateProductModel(Product product)
        {
            var mapper = AutoMapperConfig.MapperConfiguration.CreateMapper();
            
                var category = CreateCategory(product.Category);
                
                var productMap = mapper.Map<ProductModels>(product);
                
                var subCategoryMap = CreateSubCategory(product.Category.SubCategory);
                productMap.CategoryItem = category;
                productMap.SubCategoryItem = subCategoryMap;
                
            return productMap;
        }
        
        public static Product CreateProductDb(ProductModels product)
        {
             
            var NewProduct = Factory.Factory.CreateProductdb();
           
                
                NewProduct.IdProducto = Guid.NewGuid();
                NewProduct.Description = product.Description;
                NewProduct.Name = "Animalias";
                NewProduct.quantity = product.quantity;
                NewProduct.Kg = product.kg;
                NewProduct.BarCode = product.barCode;
                NewProduct.Codigo = product.Codigo;
                NewProduct.IdCategory = product.IdCategory;
            NewProduct.IdSubCategory = product.IdSubCategory;
                return NewProduct;
        }

        public static Product EditProductDb(ProductModels product, Product productDb)
        {
            try
            {
                productDb.IdProducto = product.IdProducto;
                productDb.Description = product.Description;
                productDb.Name = "Animalia";
                productDb.quantity = product.quantity;
                productDb.Kg = product.kg;
                productDb.BarCode = product.barCode;
                productDb.Codigo = product.Codigo;
                productDb.IdCategory = product.IdCategory;
                productDb.IdSubCategory = product.IdSubCategory;
                return productDb;
            }
            catch (Exception e)
            {

                throw new Exception(e.Message.ToString());
            }
        }
           
        }
    }
