using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ERPAnimalia.Interfaces;
using ERPAnimalia.EntityFramework;
using ERPAnimalia.Helper;


namespace ERPAnimalia.Models.Manager
{
    public class LoadOrderManager:ILoadOrderManager 
    {
        public AnimaliaPetShopEntities db { get; set; }
        public ProductManager _productManager { get; set; }

        public LoadOrderManager(ProductManager productManager)
        {
            db = Factory.Factory.CreateContextDataBase();
            _productManager = productManager;
        }

        public List<ProviderModel> GetProveedor()
        {
            var proveedor = GetProveedorList();

            var LoadOrderModel = Factory.LoadOrderFacory.CreateVoucherHeadLoadOrder();
            LoadOrderModel.ProveedorModel = proveedor;

            return proveedor; 
        }

        public string Save(string proveedor, string date, string fechaPago, int formaDePago,string[] precioCosto, int[] cantidad, Guid[] idProducto, string[] precioVenta)
        {
            var listProveedor = GetProveedor();
            var idProveedor = listProveedor.Where(x => x.RazonSocial == proveedor);
            
            using (var context = new AnimaliaPetShopEntities())
            {
                using (var dbContextTransaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        var message = string.Empty; 
                        db = Factory.Factory.CreateContextDataBase();

                        var head = SaveHead(idProveedor.First().IdProveedor, date, formaDePago, fechaPago, db);

                        SaveProductChange(precioCosto, cantidad, idProducto, precioVenta);

                        for (int i = 0; i < idProducto.Length; i++)
                        {
                           
                                var detalleComprobante = new DetalleComprobante();
                                detalleComprobante.IdComprobante = head.IdComprobante;
                                detalleComprobante.IdDetalleComprobante = Guid.NewGuid();
                                detalleComprobante.IdProducto = idProducto[i];
                                detalleComprobante.PrecioCosto = Convert.ToDecimal(precioCosto[i]);
                                detalleComprobante.PrecioVenta = Convert.ToDecimal(precioVenta[i]);
                                detalleComprobante.Cantidad = Convert.ToDecimal(cantidad[i]);

                                db.DetalleComprobante.Add(detalleComprobante);
                            
                        }
                       
                        db.SaveChanges();
                        dbContextTransaction.Commit();
                        return message = "EL comprobante fue guardado exitosamente";
                    }
                    catch (Exception e)
                    {

                        throw new Exception(e.Message.ToString());
                    }
                }
            }
        }


        private List<ProviderModel> GetProveedorList()
        {
            var ProveedorList = db.Proveedor.ToList();
            var listClient = MapperObject.CreateListProveedorModel(ProveedorList);

            return listClient;
        }

        public Comprobante SaveHead(Guid idProveedor, string date,int formaDePago,string fechaPago, AnimaliaPetShopEntities db)
        {
            var head = new Comprobante();

            head.IdComprobante = Guid.NewGuid();
            head.IdProveedor = idProveedor;
            head.IdTipoComprobante = (int)Enumeration.TipoComprobante.NotaPedido;
            head.IdFormaDePago = formaDePago;
            head.FechaPago = Convert.ToDateTime(fechaPago);
            head.Fecha = Convert.ToDateTime(date);

            db.Comprobante.Add(head);
            return head;

        }
        
      

        public void SaveProductChange( string[] precioCosto, int[] cantidad, Guid[] idProducto, string[] precioVenta)
        {
            var listproduct = new List<ProductModels>();
            for (int i = 0; i < idProducto.Length; i++)
            {
                     var prod = idProducto[i];
                    var producto = db.Product.Where(x => x.IdProducto == prod);


                    var cantidadTotal = Convert.ToDecimal(producto.First().Cantidad) + Convert.ToDecimal(cantidad[i]);
                    producto.First().PrecioCosto = Convert.ToDecimal((Convert.ToDecimal(producto.First().Cantidad) * producto.First().PrecioCosto) + (Convert.ToDecimal(cantidad[i]) * Convert.ToDecimal(precioCosto[i]))) / cantidadTotal;
                    producto.First().Cantidad = Convert.ToDecimal(cantidadTotal);
                    producto.First().PrecioVenta = Convert.ToDecimal(precioVenta[i]);
                    producto.First().Rentabilidad = Convert.ToDecimal(CalCulos.CalcularRentabilidadPorcentage(producto.First().PrecioCosto.Value, producto.First().PrecioVenta.Value));
                    producto.First().RentabilidadPesos = Convert.ToDecimal(CalCulos.CalcularRentabilidad(producto.First().PrecioCosto, producto.First().PrecioVenta));
                    db.SaveChanges();
            }
            
        }

    }
}