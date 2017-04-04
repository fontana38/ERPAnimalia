using ERPAnimalia.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERPAnimalia.Models
{
    public class ManagerListOfAmount
    {
        public  AnimaliaPetShopEntities db { get; set; }
        public ManagerListOfAmount()
        {

        }

        public List<ListaPrecioModel> GetListOfAmount()
        {
            db  = Factory.Factory.CreateContextDataBase();
           var listOfAmount = db.ListaPrecio.ToList();
           return MapperObject.CreateListAmountMap(listOfAmount).ToList();
        }

        public ListaPrecioModel GetListOfAmountByID(Guid id)
        {
             db = Factory.Factory.CreateContextDataBase();
            var listOfAmount = db.ListaPrecio.Find(id);
            return MapperObject.CreateListMap(listOfAmount);
        }

        public ListaPrecioModel AddList(ProductModels productModel, Product productDb)
        {
             db = Factory.Factory.CreateContextDataBase();

            var listDB= MapperObject.CreateListMapDb(productModel.ListaPrecioItem);
            productDb.ListaPrecio = listDB;
            listDB.Name = "Animalias";
            listDB.IdListaPrecio = Guid.NewGuid();
            var listOfAmount = db.ListaPrecio.Add(listDB);
            productDb.IdListaPrecio = listOfAmount.IdListaPrecio;

            var prodLista=Factory.Factory.NewProductoListaDB();
            prodLista.IdProductoLista = Guid.NewGuid();
            prodLista.IdListaPrecio = listOfAmount.IdListaPrecio;
            prodLista.IdProducto = productDb.IdProducto;
            db.ProductoLista.Add(prodLista);
           
            return MapperObject.CreateListMap(listOfAmount);
        }

        
    }
}