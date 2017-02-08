using ERPAnimalia.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERPAnimalia.Models
{
    public class ClienteManager : IClinteManager
    {
        public AnimaliaPetShopEntities db { get; set; }

        public ClienteManager()
        {
            db = Factory.Factory.CreateContextDataBase();
        }

        public void GuardarCliente(ClienteModel clienteModel)
        {
            try
            {

                db = Factory.Factory.CreateContextDataBase();
                var clienteDb = MapperObject.CreateClienteDb(clienteModel);
                clienteDb.IdCliente = Guid.NewGuid();
                db.Cliente.Add(clienteDb);

                db.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message.ToString());
            }
        }

        public List<ClienteModel> ObtenerCliente()
        {  
            var clienteList = db.Cliente.ToList();
            var map = MapperObject.CreateClienteList(clienteList);
            return map;

        }

        
    }
}