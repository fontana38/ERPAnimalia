using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ERPAnimalia.Interfaces;
using ERPAnimalia.EntityFramework;

namespace ERPAnimalia.Models.Manager
{
    public class LoadOrderManager:ILoadOrderManager 
    {
        public AnimaliaPetShopEntities db { get; set; }

        public LoadOrderManager()
        {
            db = Factory.Factory.CreateContextDataBase();
        }

        public List<ProveedorModel> GetProveedor()
        {
            var proveedor = GetProveedorList();
            
            return proveedor; 
        }


        private List<ProveedorModel> GetProveedorList()
        {
            var ProveedorList = db.Proveedor.ToList();
            var listClient = MapperObject.CreateListProveedorModel(ProveedorList);

            return listClient;
        }
    }
}