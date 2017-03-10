using ERPAnimalia.EntityFramework;
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

                        foreach (var item in clienteModel.IdsProduct)
                        {
                            var IdClienteIdProducto = new IdClienteIdProducto();
                            IdClienteIdProducto.IdClienteProducto= Guid.NewGuid();
                            IdClienteIdProducto.IdCliente = clienteModel.IdCliente;
                            IdClienteIdProducto.IdProducto = new Guid("1B04AE93-44F5-4511-869E-27A36C809D59");
                            db.IdClienteIdProducto.Add(IdClienteIdProducto);


                        }


                        clienteModel.FechaCompra = DateTime.Parse(clienteModel.FechaCompra1).Date;
                        clienteModel.FechaProximaCompra = DateTime.Parse(clienteModel.FechaCompra2).Date;
                        clienteModel.FechaProximaCompra = clienteModel.FechaProximaCompra.AddDays(clienteModel.CantidadDias);
                        var clienteDb = MapperObject.CreateClienteDb(clienteModel);
                        if (clienteDb.IdCliente == null || clienteDb.IdCliente == Guid.Empty)
                        {
                            
                            clienteDb.IdCliente = Guid.NewGuid();

                            db.Cliente.Add(clienteDb);
                            db.SaveChanges();
                            dbContextTransaction.Commit();
                        }
                        else
                        {
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
                listClient = listClient.Where(p => p.Nombre.Contains(searchString) || p.Codigo.Contains(searchString)).ToList();
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
            db.Cliente.Remove(cliente[0]);
            db.SaveChanges();
        }

        public List<ClienteModel> ObtenerCliente()
        {
            throw new NotImplementedException();
        }
    }
}