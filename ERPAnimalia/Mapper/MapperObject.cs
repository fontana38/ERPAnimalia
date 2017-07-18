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
        public static List<ProductModels> CreateProductList(List<Product> product, List<Category> category, List<SubCategory> subCategory)
        {
            try
            {
                var mapper = AutoMapperConfig.MapperConfiguration.CreateMapper();
                var list = Factory.Factory.CreateListProduct();

                foreach (var item in product)
                {
                    var cat= category.Find(x => x.IdCategory == item.IdCategory);
                    var subCat = subCategory.Find(x => x.IdSubCategory == item.IdSubCategory);
                    var productMap = mapper.Map<ProductModels>(item);
                    var subCategoryMap = CreateSubCategory(subCat);
                    var categoryMap = CreateCategory(cat);

                    productMap.CategoryItem = categoryMap;
                    productMap.SubCategoryItem = subCategoryMap;
                    productMap.CategoryName = categoryMap.Name;
                    productMap.SubCategoryName = subCategoryMap.Name;
                   
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
                

                var listaMap = mapper.Map<ClienteModel>(cliente);

                listaMap.Productos = Factory.Factory.CreateListProduct();

                if (product != null && product.Count > 0)
                {
                    foreach (var item in product)
                    {
                        var category = CreateCategory(item.Category);
                        var productMap = mapper.Map<ProductModels>(item);
                        var subCategoryMap = CreateSubCategory(item.SubCategory);
                        productMap.CategoryItem = category;
                        productMap.SubCategoryItem = subCategoryMap;
                        listaMap.Productos.Add(productMap);
                    }

                    
                }
                    

                return listaMap;
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
            newSubCategory.IdSubCategory = p.SubCategory.IdSubCategory;
            newSubCategory.Name = p.SubCategory.Name;
            return newSubCategory;
        }

        public static ProductModels CreateProductModel(Product product)
        {
            var mapper = AutoMapperConfig.MapperConfiguration.CreateMapper();
            
                var category = CreateCategory(product.Category);
                
                var productMap = mapper.Map<ProductModels>(product);
                
                var subCategoryMap = CreateSubCategory(product.SubCategory);
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
                if(product.CategoryItem.IdCategory != (int)Enumeration.Category.Accesorios)
                {
                     NewProduct.Kg = product.kg;
                    NewProduct.TotalKg = product.kg * product.Cantidad;
                 }
                NewProduct.CodigoBarra = product.CodigoBarra;
                NewProduct.Codigo = product.Codigo;
                NewProduct.IdCategory = product.IdCategory;
                NewProduct.IdSubCategory = product.IdSubCategory;
                NewProduct.PrecioVenta = product.PrecioVenta;
                NewProduct.PrecioCosto = product.PrecioCosto;
                NewProduct.Presentacion = product.Presentacion;
                NewProduct.RentabilidadPesos = Convert.ToDecimal(Helper.CalCulos.CalcularRentabilidad(product.PrecioCosto, product.PrecioVenta));
                NewProduct.Rentabilidad =Convert.ToDecimal( Helper.CalCulos.CalcularRentabilidadPorcentage(product.PrecioCosto.Value, product.PrecioVenta.Value));
                NewProduct.TotalKg = Helper.CalCulos.CalculateTotalKg(NewProduct.Kg.Value, NewProduct.Cantidad.Value);

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
                productDb.PrecioVenta = product.PrecioVenta;
                productDb.PrecioCosto = product.PrecioCosto;
                productDb.CodigoBarra = product.CodigoBarra;
                productDb.Codigo = product.Codigo;
                productDb.IdCategory = product.CategoryItem.IdCategory;
                productDb.IdSubCategory = product.SubCategoryItem.IdSubCategory;
                productDb.RentabilidadPesos = Convert.ToDecimal(Helper.CalCulos.CalcularRentabilidad(product.PrecioCosto, product.PrecioVenta));
                productDb.Rentabilidad = Convert.ToDecimal(Helper.CalCulos.CalcularRentabilidadPorcentage(product.PrecioCosto.Value, product.PrecioVenta.Value));


                return productDb;
            }
            catch (Exception e)
            {

                throw new Exception(e.Message.ToString());
            }
        }

        public static List<ClienteModel> CreateListClientModel(List<Cliente> listCliente)
        {
            try
            {
                
                var mapper = AutoMapperConfig.MapperConfiguration.CreateMapper();
                var client = mapper.Map<List<ClienteModel>>(listCliente);
                foreach (var item in client)
                {
                    item.NombreCompleto = item.Apellido + " " + item.Nombre;
                }
                return client;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message.ToString());
            }
        }


        public static List<ProveedorModel> CreateListProveedorModel(List<Proveedor> listProveedor)
        {
            try
            {

                var mapper = AutoMapperConfig.MapperConfiguration.CreateMapper();
                var proveedor = mapper.Map<List<ProveedorModel>>(listProveedor);
                foreach (var item in proveedor)
                {
                    item.NombreCompleto = item.Apellido + " " + item.Nombre;
                }
                return proveedor;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message.ToString());
            }
        }
        public static List<DetalleComprobante> CreateVoucherDetailDb(List<VoucherDetailModel> voucherDetail)
        {
            try
            {
                var mapper = AutoMapperConfig.MapperConfiguration.CreateMapper();
                var vaoucherDetailMapper = new List<DetalleComprobante>();
                foreach (var item in voucherDetail)
                {
                   
                  var  vouccherDetailMap = mapper.Map<DetalleComprobante>(item);
                    vaoucherDetailMapper.Add(vouccherDetailMap);

                }
                return vaoucherDetailMapper;
            }
            catch (Exception e)
            {

                throw new Exception(e.Message.ToString());
            }

        }
        public static Comprobante CreateVoucherHeadDb(VoucherHeadModel voucherHead)
        {
            try
            {
                var mapper = AutoMapperConfig.MapperConfiguration.CreateMapper();

                var voucherHeadMap = mapper.Map<Comprobante>(voucherHead);

                return voucherHeadMap;
            }
            catch (Exception e)
            {

                throw new Exception(e.Message.ToString());
            }

        }

    }

    }
