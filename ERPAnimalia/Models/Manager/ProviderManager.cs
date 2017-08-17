using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ERPAnimalia.Interfaces;
using ERPAnimalia.EntityFramework;
using System.Data.Entity;

namespace ERPAnimalia.Models.Manager
{

    public class ProviderManager : IProviderManager
    {
        public AnimaliaPetShopEntities db { get; set; }

        public ProviderManager()
        {
            db = Factory.Factory.CreateContextDataBase();
        }
        public void BorrarCliente(Guid idProvider)
        {
            throw new NotImplementedException();
        }

        public List<ProviderModel> GetProvider(int? page, int? limit, string sortBy, string direction, string searchString, out int total)
        {

            var providerList = db.Proveedor.ToList();

            var productList = Factory.Factory.CreateListProductdb();
            var listProvider = Factory.Factory.ListProviderModels();

            //Edit Product Selected
            foreach (var item in providerList)
            {
                var pc = db.IdProveedorProducto.Where(x => x.IdProveedor == item.IdProveedor).ToList();

                foreach (var itemproduct in pc)
                {
                    var product = db.Product.Where(x => x.IdProducto == itemproduct.IdProducto).First();
                    productList.Add(product);
                }

                var map = MapperObject.CreateProveedorProductModel(item, productList);
                listProvider.Add(map);
            }

            if (!string.IsNullOrWhiteSpace(searchString))
            {
                listProvider = listProvider.Where(p => (p.Nombre.ToUpper().StartsWith(searchString.ToUpper()) || p.Nombre.ToUpper().EndsWith(searchString.ToUpper())) ||
                    (p.Codigo.ToUpper().StartsWith(searchString.ToUpper()) || p.Codigo.ToUpper().EndsWith(searchString.ToUpper())) ||
                    (p.Apellido.ToUpper().StartsWith(searchString.ToUpper()) || p.Apellido.ToUpper().EndsWith(searchString.ToUpper()))).ToList();
                
            }

            total = listProvider.Count();
            var clienteQueryable = listProvider.AsQueryable();

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

            return listProvider;
        }

        public void SaveProvider(ProviderModel providerModel)
        {
            using (var context = new AnimaliaPetShopEntities())
            {
                using (var dbContextTransaction = context.Database.BeginTransaction())
                {

                    try
                    {
                        db = Factory.Factory.CreateContextDataBase();

                        providerModel.Fecha = DateTime.Parse(providerModel.FechaString).Date;

                        var providerDb = MapperObject.CreateProviderDb(providerModel);

                        if (providerDb.IdProveedor == null || providerDb.IdProveedor == Guid.Empty)
                        {

                            providerDb.IdProveedor = Guid.NewGuid();

                            db.Proveedor.Add(providerDb);
                            SaveProductsId(providerModel, providerDb.IdProveedor);

                            db.SaveChanges();
                            dbContextTransaction.Commit();
                        }
                        else
                        {
                            SaveProductsId(providerModel, providerDb.IdProveedor);
                            db.Proveedor.Attach(providerDb);
                            db.Entry(providerDb).State = EntityState.Modified;
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

        private void SaveProductsId(ProviderModel providerModel, Guid idProvider)
        {
            if (providerModel.IdsProduct != null)
            {
                foreach (var item in providerModel.IdsProduct)
                {
                    var idProviderIdProducto = new IdProveedorProducto();
                    idProviderIdProducto.IdProveedorIdproducto = Guid.NewGuid();
                    idProviderIdProducto.IdProveedor = idProvider;
                    idProviderIdProducto.IdProducto = item;
                    db.IdProveedorProducto.Add(idProviderIdProducto);

                }
            }
        }

        public void DeleteProvider(Guid idProveedor)
        {
            var proveedor = db.Proveedor.Where(x => x.IdProveedor == idProveedor).ToList();
            var product =db.IdProveedorProducto.Where(x => x.IdProveedor == idProveedor).ToList();

            if(product.Count > 0 )
            {
                foreach (var item in product)
                {
                    db.IdProveedorProducto.Remove(item);
                }
            }
            
            db.Proveedor.Remove(proveedor[0]);
            db.SaveChanges();
        }
    }
}
