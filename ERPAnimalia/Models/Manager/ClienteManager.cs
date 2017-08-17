using ERPAnimalia.EntityFramework;
using ERPAnimalia.Interface;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
            using (var context = new AnimaliaPetShopEntities())
            {
                using (var dbContextTransaction = context.Database.BeginTransaction())
                {

                    try
                    {
                        db = Factory.Factory.CreateContextDataBase();
                        
                            


                        clienteModel.FechaCompra = DateTime.Parse(clienteModel.FechaCompra1).Date;
                        clienteModel.FechaProximaCompra = DateTime.Parse(clienteModel.FechaCompra2).Date;
                        clienteModel.FechaProximaCompra = clienteModel.FechaProximaCompra.AddDays(clienteModel.Dias);
                        var clienteDb = MapperObject.CreateClienteDb(clienteModel);
                        if (clienteDb.IdCliente == null || clienteDb.IdCliente == Guid.Empty)
                        {
                            
                            clienteDb.IdCliente = Guid.NewGuid();
                          
                            db.Cliente.Add(clienteDb);
                            SaveProductsId(clienteModel, clienteDb.IdCliente);

                            db.SaveChanges();
                            dbContextTransaction.Commit();
                        }
                        else
                        {
                            SaveProductsId(clienteModel, clienteDb.IdCliente);
                            db.Cliente.Attach(clienteDb);
                            db.Entry(clienteDb).State = EntityState.Modified;
                            db.SaveChanges();
                            dbContextTransaction.Commit();
                        }

                    }
                    catch (Exception e)
                    {
                        throw new Exception(e.Message.ToString());
                    }
                }
            }
        }

        private void SaveProductsId(ClienteModel clienteModel, Guid idClient)
        {
            if (clienteModel.IdsProduct != null)
            {
                foreach (var item in clienteModel.IdsProduct)
                {
                    var IdClienteIdProducto = new IdClienteIdProducto();
                    IdClienteIdProducto.IdClienteProducto = Guid.NewGuid();
                    IdClienteIdProducto.IdCliente = idClient;
                    IdClienteIdProducto.IdProducto = item;
                    db.IdClienteIdProducto.Add(IdClienteIdProducto);

                }
            }
        }
        public List<ClienteModel> ObtenerCliente(int? page, int? limit, string sortBy, string direction, string searchString, out int total)
        {
            
            var clienteList = db.Cliente.ToList();
             
            var productList = Factory.Factory.CreateListProductdb();
            var listClient = Factory.Factory.ListClienteModels();

            //Edit Product Selected
            foreach (var item in clienteList)
            {
                var pc = db.IdClienteIdProducto.Where(x => x.IdCliente == item.IdCliente).ToList();

                foreach (var itemproduct in pc)
                {
                   var product = db.Product.Where(x => x.IdProducto == itemproduct.IdProducto).First();
                   productList.Add(product);
                }

                var map = MapperObject.CreateClienteProductModel(item, productList);
                listClient.Add(map);
            }
           
            if (!string.IsNullOrWhiteSpace(searchString))
            {
                listClient = listClient.Where(p => (p.Nombre.ToUpper().StartsWith(searchString.ToUpper()) || p.Nombre.ToUpper().EndsWith(searchString.ToUpper())) ||
                (p.Codigo.ToUpper().StartsWith(searchString.ToUpper()) || p.Codigo.ToUpper().EndsWith(searchString.ToUpper())) ||
                (p.Apellido.ToUpper().StartsWith(searchString.ToUpper()) || p.Apellido.ToUpper().EndsWith(searchString.ToUpper()))).ToList();
            }

            total = clienteList.Count();
            var clienteQueryable = listClient.AsQueryable();

            if (!string.IsNullOrEmpty(sortBy) && !string.IsNullOrEmpty(direction))
            {
               
                if (direction.Trim().ToLower() == "asc")
                {
                    clienteQueryable = SortHelper.OrderBy(clienteQueryable, sortBy);
                }
                else
                {
                    clienteQueryable = SortHelper.OrderByDescending(clienteQueryable, sortBy);
                }
            }

            if (page.HasValue && limit.HasValue)
            {
                int start = (page.Value - 1) * limit.Value;
                clienteQueryable = clienteQueryable.Skip(start).Take(limit.Value);
            }

            return clienteQueryable.ToList();
        }

        public void BorrarCliente(Guid idCliente)
        {
            var cliente=db.Cliente.Where(x => x.IdCliente == idCliente).ToList();
            var product = db.IdClienteIdProducto.Where(x => x.IdCliente == idCliente).ToList();

            if (product.Count > 0)
            {
                foreach (var item in product)
                {
                    db.IdClienteIdProducto.Remove(item);
                }
            }
            db.Cliente.Remove(cliente[0]);
            db.SaveChanges();
        }

        public List<ClienteModel> ObtenerCliente()
        {
            throw new NotImplementedException();
        }
    }
}