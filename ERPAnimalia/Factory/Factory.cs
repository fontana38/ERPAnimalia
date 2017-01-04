using ERPAnimalia.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ERPAnimalia.EntityFramework;

namespace ERPAnimalia.Factory
{
    public static class Factory
    {

       public static ListaPrecio NewListaPrecio()
        {
            return new ListaPrecio();
        }

        public static ProductoLista NewProductoLista()
        {
            return new ProductoLista();
        }

        public static ListaPrecioModel NewListaPrecioModel()
        {
            return new ListaPrecioModel();
        }

        public static List<ListaPrecioModel>  NewListaPrecioModelCollection()
        {
            return new List<ListaPrecioModel>();
        }

        public static ProductoListaModel NewProductoListaModel()
        {
            return new ProductoListaModel();
        }

        public static ProductModels NewProduct()
        {
            var newProduct = new ProductModels();
            newProduct.Category = new List<CategoryModel>();
            newProduct.SubCategory = new List<SubCategoryModel>();
            return newProduct;
        }

        public static AnimaliaPetShopEntities CreateContextDataBase()
        {
            return new AnimaliaPetShopEntities();
            
        }

        public static Product CreateProductdb()
        {
          
            return new Product();
        }

        public static TransferToFreeProductManager CreateTransferToFreeProductManager()
        {

            return new TransferToFreeProductManager();
        }


        public static ManagerListOfAmount CreateManagerListOfAmount()
        {
            return new  ManagerListOfAmount();
            
        }

        public static ProductManager CreateProducManager()
        {
            var productManager = new ProductManager();
            return productManager;
        }

        public static ProductModels NewProductModel()
        {
          
            return new ProductModels();
            
        }

        public static List<ProductModels> CreateListProduct()
        {

            return new List<ProductModels>();

        }

        public static List<CategoryModel> CreateListCategoryModel()
        {
            return new List<CategoryModel>();

        }

        public static List<SubCategoryModel> CreateListSubCategoryModel()
        {
            return new List<SubCategoryModel>();

        }

        public static SubCategoryModel CreateSubCategoryModel()
        {
            return new SubCategoryModel();

        }

        public static ProductoLista NewProductoListaDB()
        {
            return new ProductoLista();

        }

        public static CategoryModel CreateCategoryModel()
        {
            var categoryModel = new CategoryModel();
            categoryModel.SubCategory = new List<SubCategoryModel>();
            return  categoryModel;

        }


    }

}
