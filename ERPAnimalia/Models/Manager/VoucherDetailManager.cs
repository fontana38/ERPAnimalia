using ERPAnimalia.EntityFramework;
using ERPAnimalia.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERPAnimalia.Models.Manager
{
    public class VoucherDetailManager : IVoucherDetailManager
    {
        public AnimaliaPetShopEntities db { get; set; }

        public VoucherDetailManager()
        {
            db = Factory.Factory.CreateContextDataBase();
        }

        public List<ProductModels> GetProduct()
        {
            var productList = db.Product.ToList();
            var productListModel = MapperObject.CreateProductList(productList);

            return productListModel;
        }

        public void SaveVoucher(List<DetailGrid> list)
        {
            using (var context = new AnimaliaPetShopEntities())
            {
                using (var dbContextTransaction = context.Database.BeginTransaction())
                {

                    try
                    {
                        db = Factory.Factory.CreateContextDataBase();


                        var clienteDb = MapperObject.CreateClienteDb(clienteModel);
                        if (clienteDb.IdCliente == null || clienteDb.IdCliente == Guid.Empty)
                        {

                            clienteDb.IdCliente = Guid.NewGuid();

                            db.Cliente.Add(clienteDb);

                            if (clienteModel.IdsProduct != null)
                            {
                                foreach (var item in clienteModel.IdsProduct)
                                {
                                    var IdClienteIdProducto = new IdClienteIdProducto();
                                    IdClienteIdProducto.IdClienteProducto = Guid.NewGuid();
                                    IdClienteIdProducto.IdCliente = clienteDb.IdCliente;
                                    IdClienteIdProducto.IdProducto = item;
                                    db.IdClienteIdProducto.Add(IdClienteIdProducto);

                                }
                            }
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

    }
}