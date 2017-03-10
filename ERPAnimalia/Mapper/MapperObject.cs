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

        //public static List<ClienteModels> CreateClientesList(Cliente cli)
        //{
        //    var mapper = AutoMapperConfig.MapperConfiguration.CreateMapper();
        //    var cliente = mapper.Map<ClienteModels>(cli);
        //    return cliente;
        //}
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

        public static Cliente CreateClienteDb(ClienteModel cliente)
        {
            try
            {
                var mapper = AutoMapperConfig.MapperConfiguration.CreateMapper();

                var clienteMap = mapper.Map<Cliente>(cliente);

                return clienteMap;
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

        public static ClienteModel CreateClienteProductModel(Cliente cliente, List<Product> product)
        {
            try
            {
                var mapper = AutoMapperConfig.MapperConfiguration.CreateMapper();
                var list = Factory.Factory.Cliente();
                var productItem = new ProductModels();  
                var listaMap = mapper.Map<ClienteModel>(cliente);
                if(product != null && product.Count > 0)
                {
                    foreach (var item in product)
                    {
                        productItem = mapper.Map<ProductModels>(item);
                    }

                    listaMap.Productos.Add(productItem);
                }
                    

                return listaMap;
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
                NewProduct.Descripcion1 = product.Descripcion1;
                NewProduct.Descripcion2 = product.Descripcion2;
                NewProduct.Marca = product.Marca;
                NewProduct.Cantidad = product.Cantidad;
                NewProduct.Kg = product.kg;
                NewProduct.CodigoBarra = product.CodigoBarra;
                NewProduct.Codigo = product.Codigo;
                NewProduct.IdCategoria = product.IdCategory;
                
                return NewProduct;
        }

        public static Product EditProductDb(ProductModels product, Product productDb)
        {
            try
            {
                productDb.IdProducto = product.IdProducto;
                productDb.Descripcion1 = product.Descripcion1;
                productDb.Descripcion2 = product.Descripcion2;
                productDb.Marca = product.Marca;
                productDb.Cantidad = product.Cantidad;
                productDb.Kg = product.kg;
               
                productDb.CodigoBarra = product.CodigoBarra;
                productDb.Codigo = product.Codigo;
                productDb.IdCategoria = product.IdCategory;
               
                return productDb;
            }
            catch (Exception e)
            {

                throw new Exception(e.Message.ToString());
            }
        }
           
        }
    }
