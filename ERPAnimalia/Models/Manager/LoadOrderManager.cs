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

        public void Save(string cliente, string date, string fechaPago, int formaDePago, string[] precioCosto, int[] cantidad, Guid[] idProducto, string[] precioVenta)
        {
            //using (var context = new AnimaliaPetShopEntities())
            //{
            //    using (var dbContextTransaction = context.Database.BeginTransaction())
            //    {
            //        try
            //        {
            //            db = Factory.Factory.CreateContextDataBase();

            //            SaveHead( cliente, date, formaDePago, fechaPago,db);

            //            SaveDetail(precioCosto, cantidad, idProducto, precioVenta);


                       

            //            foreach (var item in voucherDetailDb)
            //            {
            //                item.IdDetalleComprobante = Guid.NewGuid();
            //                item.IdComprobante = headDb.IdComprobante;
            //                var product = db.Product.Find(item.IdProducto);

            //                if (verifyQuantyty(item, product))
            //                {
            //                    if (product.IdSubCategoria != (int)Enumeration.Subcategory.Suelto)
            //                    {
            //                        product.Cantidad = product.Cantidad - item.Cantidad;
            //                    }
            //                    else
            //                    {
            //                        product.Kg = product.Kg - item.Cantidad;
            //                    }

            //                }
            //                else
            //                {
            //                    return message = "La cantidad del producto es menor que la cantidad vendida ";
            //                }

            //                db.DetalleComprobante.Add(item);
            //            }
            //            db.SaveChanges();
            //            dbContextTransaction.Commit();
            //            return message = "EL comprobante fue guardado exitosamente";
            //        }
            //        catch (Exception e)
            //        {

            //            throw new Exception(e.Message.ToString());
            //        }
            //    }
            //}
        }


        private List<ProveedorModel> GetProveedorList()
        {
            var ProveedorList = db.Proveedor.ToList();
            var listClient = MapperObject.CreateListProveedorModel(ProveedorList);

            return listClient;
        }

        public void SaveHead(string cliente, string date,int formaDePago,string fechaPago, AnimaliaPetShopEntities db)
        {

            var listProveedor = GetProveedor();

            var proveedorName = (from N in listProveedor
                                 where N.Nombre.StartsWith(cliente) || N.Apellido.StartsWith(cliente)
                                 select new { N.IdProveedor }).ToList();

            var head = new Comprobante();

            head.IdComprobante = new Guid();
            head.IdProveedor = proveedorName[0].IdProveedor;
            head.IdTipoComprobante = (int)Enumeration.TipoComprobante.NotaPedido;
            head.IdFormaDePago = formaDePago;
            head.FechaPago = Convert.ToDateTime(fechaPago);
            head.Fecha = Convert.ToDateTime(date);

            db.Comprobante.Add(head);

        }
        
        public List<ProductModels> GetRecordsNewQuantity( List<ProductModels> product)
        {
            foreach (var item in collection)
            {

            }
            return product;
        }

        public void SaveDetail( string[] precioCosto, int[] cantidad, Guid[] idProducto, string[] precioVenta)
        {

        }

    }
}