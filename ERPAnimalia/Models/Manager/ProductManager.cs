using ERPAnimalia.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using System.Web.Http.ModelBinding;
using OnBarcode.Barcode.BarcodeScanner;

namespace ERPAnimalia.Models
{
    public  class ProductManager
    {
        public static AnimaliaPetShopEntities db { get; set; }

        public TransferToFreeProductManager ProductOpenManagers { get; set; }

        public List<ProductListModel> map;
        private static readonly Object obj = new Object();



        public ProductManager()
        {
            
                db = Factory.Factory.CreateContextDataBase();
            
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
  
                        //AddNewProduct
                        AddProduct(productDb);

                        context.SaveChanges();

                        dbContextTransaction.Commit();
                    }
                    catch (Exception exepction)
                    {
                        dbContextTransaction.Rollback();
                        throw exepction;
                    }
                }
            }
        }
            
     
        public void DeleteProduct(ProductModels product)
        {
           
            var deleteProduct =db.Product.Find(product);
            db.Product.Remove(deleteProduct);
            db.SaveChanges();

        }

        public List<CategoryModel> GetCategoryList()
        {
           
            var CategoryList = db.Category.ToList();
            var map = MapperObject.CreateCategoryList(CategoryList);
            return map;

        }

        public List<SubCategoryModel> GetSubCategoryList()
        {
           
            var CategoryList = db.SubCategory.ToList();
            var map = MapperObject.CreateSubCategoryList(CategoryList);
            return map;

        }

        public virtual ProductModels GetProductById (Guid id)
        {
            
            var  productById = db.Product.Find(id);
            var  productMap =MapperObject.CreateProductModel(productById);
            productMap.Category = GetCategoryList();
            productMap.SubCategory = GetSubCategoryList();
            return productMap;
        }

        public void EditProduct(ProductModels product)
        {
            
            var id = product.IdProducto;
            var productById = db.Product.Find(id);
            var productDb = MapperObject.EditProductDb(product, productById);

            db.SaveChanges();

        }

        public List<CategoryModel> GetCategory()
        {
           
            var category = db.Category.ToList();
           
            return MapperObject.CreateCategoryList(category);
        }

        public List<SubCategoryModel> GetSubCategory()
        {
           
            var subCategory = db.SubCategory.ToList();

            return MapperObject.CreateSubCategoryList(subCategory);
        }

        public void Delete(Guid id)
        {
            
           
            var productById = db.Product.Find(id);

            if(productById != null)
            {
                db.Product.Remove(productById);
                db.SaveChanges();
            }
           
        }

        public ProductModels NewProductModel()
        {
           return Factory.Factory.NewProductModel();
        }

       
        public List<ProductModels> GetProductList(int? page, int? limit, string sortBy, string direction, string searchString, out int total)
        {
            var map = new List<ProductModels>();
            
                map = MapProduct();

           

            if (!string.IsNullOrWhiteSpace(searchString))
            {
                
                    map = map.Where(p => (p.CodigoBarra != null) ?(((p.CodigoBarra.ToUpper().StartsWith(searchString.ToUpper()) || p.CodigoBarra.ToUpper().EndsWith(searchString.ToUpper())) || (p.Codigo.ToUpper().StartsWith(searchString.ToUpper()) || p.Codigo.ToUpper().EndsWith(searchString.ToUpper())) ||
                    (p.Descripcion1.ToUpper().StartsWith(searchString.ToUpper()) || p.Descripcion1.ToUpper().EndsWith(searchString.ToUpper())) ||
                    (p.Marca.ToUpper().StartsWith(searchString.ToUpper()) || p.Marca.ToUpper().EndsWith(searchString.ToUpper())) ||
                    (p.Descripcion2.ToUpper().StartsWith(searchString.ToUpper()) || p.Descripcion2.ToUpper().EndsWith(searchString.ToUpper())))): ((p.Codigo.ToUpper().StartsWith(searchString.ToUpper()) || p.Codigo.ToUpper().EndsWith(searchString.ToUpper())) ||
                    (p.Descripcion1.ToUpper().StartsWith(searchString.ToUpper()) || p.Descripcion1.ToUpper().EndsWith(searchString.ToUpper())) ||
                    (p.Marca.ToUpper().StartsWith(searchString.ToUpper()) || p.Marca.ToUpper().EndsWith(searchString.ToUpper())) ||
                    (p.Descripcion2.ToUpper().StartsWith(searchString.ToUpper()) || p.Descripcion2.ToUpper().EndsWith(searchString.ToUpper())))).ToList();
                
               
            }

            total = map.Count();

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


        public List<ProductModels> GetProductNotSelected(Guid? idClient, int? page, int? limit, string sortBy, string direction, string searchString, out int total)
        {
            total = 0;
            var map = new List<ProductModels>();
            map = MapProduct();

            if (idClient != null)
            {



                var productList = db.IdClienteIdProducto.Where(x => x.IdCliente == idClient).ToList();



                foreach (var item in productList)
                {
                    var deleteProduct = map.Where(x => x.IdProducto == item.IdProducto).First();
                    map.Remove(deleteProduct);
                }

            }


                if (!string.IsNullOrWhiteSpace(searchString))
            {

                map = map.Where(p => (p.CodigoBarra != null) ? (((p.CodigoBarra.ToUpper().StartsWith(searchString.ToUpper()) || p.CodigoBarra.ToUpper().EndsWith(searchString.ToUpper())) || (p.Codigo.ToUpper().StartsWith(searchString.ToUpper()) || p.Codigo.ToUpper().EndsWith(searchString.ToUpper())) ||
                (p.Descripcion1.ToUpper().StartsWith(searchString.ToUpper()) || p.Descripcion1.ToUpper().EndsWith(searchString.ToUpper())) ||
                (p.Marca.ToUpper().StartsWith(searchString.ToUpper()) || p.Marca.ToUpper().EndsWith(searchString.ToUpper())) ||
                (p.Descripcion2.ToUpper().StartsWith(searchString.ToUpper()) || p.Descripcion2.ToUpper().EndsWith(searchString.ToUpper())))) : ((p.Codigo.ToUpper().StartsWith(searchString.ToUpper()) || p.Codigo.ToUpper().EndsWith(searchString.ToUpper())) ||
                (p.Descripcion1.ToUpper().StartsWith(searchString.ToUpper()) || p.Descripcion1.ToUpper().EndsWith(searchString.ToUpper())) ||
                (p.Marca.ToUpper().StartsWith(searchString.ToUpper()) || p.Marca.ToUpper().EndsWith(searchString.ToUpper())) ||
                (p.Descripcion2.ToUpper().StartsWith(searchString.ToUpper()) || p.Descripcion2.ToUpper().EndsWith(searchString.ToUpper())))).ToList();


            }

            total = map.Count();

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



        public List<ProductModels> GetProductListByIdClient(Guid? idClient,int? page, int? limit, string sortBy, string direction, string searchString, out int total)
        {
            total = 0;
            var productList = new List<ProductModels>();

            if(idClient != null)
            {
                var pc = db.IdClienteIdProducto.Where(x => x.IdCliente == idClient).ToList();

                foreach (var itemproduct in pc)
                {
                    var product = GetProductById(itemproduct.IdProducto.Value);
                    productList.Add(product);
                }


                total = productList.Count();

                var productQueryable = productList.AsQueryable();

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

            return new List<ProductModels>();
        }

        public List<ProductModels> GetProductListByIdProvider(Guid? idProvider, int? page, int? limit, string sortBy, string direction, string searchString, out int total)
        {
            total = 0;
            var productList = new List<ProductModels>();

            if (idProvider != null)
            {
                var pc = db.IdProveedorProducto.Where(x => x.IdProveedor == idProvider).ToList();

                foreach (var itemproduct in pc)
                {
                    var product = GetProductById(itemproduct.IdProducto.Value);
                    productList.Add(product);
                }


                total = productList.Count();

                var productQueryable = productList.AsQueryable();

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

            return new List<ProductModels>();
        }
        public List<ProductModels> GetProducBugtList(List<ProductModels> list)
        {

            var listProduct = list.Where(x => x.IdSubCategory == (int)Enumeration.Subcategory.Bolsa || x.IdCategory == (int)Enumeration.Category.Accesorios).ToList();
            return listProduct;

        }



        public List<ProductModels> GetProductNotSelectedProvider(Guid? idProvider, int? page, int? limit, string sortBy, string direction, string searchString, out int total)
        {
            total = 0;
            var map = new List<ProductModels>();
            map = MapProduct();

            if (idProvider != null)
            {



                var productList = db.IdProveedorProducto.Where(x => x.IdProveedor == idProvider).ToList();



                foreach (var item in productList)
                {
                    var deleteProduct = map.Where(x => x.IdProducto == item.IdProducto).First();
                    map.Remove(deleteProduct);
                }

            }


            if (!string.IsNullOrWhiteSpace(searchString))
            {

                map = map.Where(p => (p.CodigoBarra != null) ? (((p.CodigoBarra.ToUpper().StartsWith(searchString.ToUpper()) || p.CodigoBarra.ToUpper().EndsWith(searchString.ToUpper())) || (p.Codigo.ToUpper().StartsWith(searchString.ToUpper()) || p.Codigo.ToUpper().EndsWith(searchString.ToUpper())) ||
                (p.Descripcion1.ToUpper().StartsWith(searchString.ToUpper()) || p.Descripcion1.ToUpper().EndsWith(searchString.ToUpper())) ||
                (p.Marca.ToUpper().StartsWith(searchString.ToUpper()) || p.Marca.ToUpper().EndsWith(searchString.ToUpper())) ||
                (p.Descripcion2.ToUpper().StartsWith(searchString.ToUpper()) || p.Descripcion2.ToUpper().EndsWith(searchString.ToUpper())))) : ((p.Codigo.ToUpper().StartsWith(searchString.ToUpper()) || p.Codigo.ToUpper().EndsWith(searchString.ToUpper())) ||
                (p.Descripcion1.ToUpper().StartsWith(searchString.ToUpper()) || p.Descripcion1.ToUpper().EndsWith(searchString.ToUpper())) ||
                (p.Marca.ToUpper().StartsWith(searchString.ToUpper()) || p.Marca.ToUpper().EndsWith(searchString.ToUpper())) ||
                (p.Descripcion2.ToUpper().StartsWith(searchString.ToUpper()) || p.Descripcion2.ToUpper().EndsWith(searchString.ToUpper())))).ToList();


            }

            total = map.Count();

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


        public List<ProductModels> GetProducBug(List<ProductModels> list)
        {

            var listProduct = list.Where(x => x.IdSubCategory == (int)Enumeration.Subcategory.Bolsa && x.IdCategory == (int)Enumeration.Category.Alimento).ToList();
            return listProduct;

        }

        public List<ProductModels> GetProducLooseList(List<ProductModels> list)
        {

            var listProduct = list.Where(x => x.IdSubCategory == (int)Enumeration.Subcategory.Suelto && x.IdCategory == (int)Enumeration.Category.Alimento).ToList();
            return listProduct;

        }

      
        public List<ProductModels> MapProduct()
        {
            lock (obj)
            {
                var productList = db.Product.ToList();
                var category = db.Category.ToList();
                var subcategoria = db.SubCategory.ToList();

                return MapperObject.CreateProductList(productList, category, subcategoria);
            }
        }

        public void RemoveModelView(ModelStateDictionary modelState)
        {
            modelState.Remove("Codigo");
            modelState.Remove("Description");
            modelState.Remove("quantity");
            modelState.Remove("IdCategory");
            modelState.Remove("IdSubCategory");
        }


        public void SaveProductToLoose(Guid? idBug,Guid? idLoose, string precioCosto, string precioVenta)
        {
            using (var context = new AnimaliaPetShopEntities())
            {
                using (var dbContextTransaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        Product productBug = db.Product.Find(idBug);
                        Product productLoose = db.Product.Find(idLoose);
                        var totalQuantity = productLoose.TotalKg + productBug.Kg;


                        var precioCostoNew = ((Math.Round(productLoose.TotalKg.Value,2) * Math.Round(productLoose.PrecioCosto.Value,2)) + ((Math.Round(productBug.Kg.Value,2)) * (Convert.ToDecimal(precioCosto) ))) / Math.Round(totalQuantity.Value,2);
                        productLoose.PrecioCosto = precioCostoNew;
                        productLoose.PrecioVenta = Convert.ToDecimal(precioVenta);
                        productLoose.TotalKg = productLoose.TotalKg + productBug.Kg;

                        productBug.TotalKg = productBug.TotalKg - productBug.Kg;
                        productBug.Cantidad = productBug.Cantidad - 1;

                       
                        db.SaveChanges();

                        dbContextTransaction.Commit();


                    }
                    catch (Exception exepction)
                    {
                        dbContextTransaction.Rollback();
                        throw exepction;
                    }


                    // producto.First().PrecioCosto = Convert.ToDecimal((Convert.ToDecimal(producto.First().Cantidad) * producto.First().PrecioCosto) + (Convert.ToDecimal(cantidad[i]) * Convert.ToDecimal(precioCosto[i]))) / cantidadTotal;

                }
            }
         }


           
    }
}