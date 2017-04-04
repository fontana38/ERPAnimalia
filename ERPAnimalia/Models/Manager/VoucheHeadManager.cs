using ERPAnimalia.EntityFramework;
using ERPAnimalia.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERPAnimalia.Models.Manager
{
    public class VoucheHeadManager : IVoucherHeadManager
    {
        
        public AnimaliaPetShopEntities db { get; set; }

        public VoucheHeadManager()
        {
            db = Factory.Factory.CreateContextDataBase();
        }

        public List<ClienteModel> GetClient()
        {
           
            var clienteList = db.Cliente.ToList();
            var listClient = MapperObject.CreateListClientModel(clienteList);
            return listClient;

        }

        
    }
}