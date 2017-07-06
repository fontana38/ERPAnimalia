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
        public ProductManager _ProducManager { get; set; }

        public VoucherDetailManager()
        {
            db = Factory.Factory.CreateContextDataBase();
            _ProducManager = Factory.Factory.CreateProducManager();
        }
        public List<TipoComprobante> GetTipoComprobante()
        {

            var tipoComprobante = db.TipoComprobante.ToList();
            return tipoComprobante;
        }

        public List<FormaDePago> GetFormaDePago()
        {
            var formaDePago = db.FormaDePago.ToList();
            return formaDePago;
        }

        public List<ProductModels> GetProduct()
        {
            return _ProducManager.MapProduct();
        }

        public string SaveVoucher(List<DetailGrid> detailGridTemp, VoucherHeadModel head)
        {
            var detail = MappModels(detailGridTemp);
            var message = string.Empty;
            using (var context = new AnimaliaPetShopEntities())
            {
                using (var dbContextTransaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        db = Factory.Factory.CreateContextDataBase();
                        decimal total =0;

                       

                        foreach (var item in detail)
                        {
                            total =+ item.Subtotal;
                        }
                        
                        head.Total = total;
                         var headDb = MapperObject.CreateVoucherHeadDb(head);
                        var voucherDetailDb = MapperObject.CreateVoucherDetailDb(detail);

                        headDb.IdComprobante= Guid.NewGuid();
                        db.Comprobante.Add(headDb);

                        foreach (var item in voucherDetailDb)
                        {
                            item.IdDetalleComprobante= Guid.NewGuid();
                            item.IdComprobante = headDb.IdComprobante;
                            var product = db.Product.Find(item.IdProducto);
                            
                            if(verifyQuantyty(item, product))
                            {
                                if(product.IdSubCategory != (int)Enumeration.Subcategory.Suelto)
                                {
                                    product.Cantidad = product.Cantidad - item.Cantidad;
                                }
                                else
                                {
                                    product.Kg = product.Kg - item.Cantidad;
                                }
                               
                            }
                            else
                            {
                                return message = "La cantidad del producto es menor que la cantidad vendida ";
                            }

                            db.DetalleComprobante.Add(item);
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

               public List<VoucherDetailModel> MappModels(List<DetailGrid> detailGridTemp)
        {
            var voucherDetailModel = Factory.VoucherFactory.CreateVoucherDetailModel();
            var voucherDetailModelList = Factory.VoucherFactory.CreateVoucherDetailModelList();
            var listProduct = GetProduct();

            if (detailGridTemp != null)
            {
                foreach (var item in detailGridTemp)
                {
                    voucherDetailModel.Cantidad = item.Cantidad;
                    voucherDetailModel.Subtotal = Convert.ToDecimal(item.Subtotal);
                 
                    voucherDetailModel.PrecioVenta = item.PrecioVenta;
                    voucherDetailModel.Descuento = item.Descuento;
                    voucherDetailModel.IdProducto = item.IdProduct;

                    voucherDetailModelList.Add(voucherDetailModel);
                }


            }
            return voucherDetailModelList;
        }

        private bool verifyQuantyty(DetalleComprobante comprobante,Product product)
        {
           if(product.IdSubCategory != (int)Enumeration.Subcategory.Suelto)
            {
                if (comprobante.Cantidad < product.Cantidad)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
           else
            {
                if (comprobante.Cantidad < product.Kg)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            
        }

        public double CalculateDiscountPorcentage(DetailGrid row,double discount)
        {
            row.Descuento = discount;
            row.Subtotal = ((row.PrecioVenta * Convert.ToInt16(row.Cantidad)) - discount).ToString("F");
            row.Porcentage = (discount / row.PrecioVenta) ;
            return row.Porcentage;
        }

        public DetailGrid SetValuesNewRowTable(DetailGrid detailGrid, int cantidad,  double descuento)
        {
            if (detailGrid.SubCategoryItem != (int)Enumeration.Subcategory.Suelto)
            {
                detailGrid.Cantidad = (cantidad == 0) ? 1 : cantidad;
            }
            else
            {
                detailGrid.Cantidad = cantidad;
            }

            detailGrid.Descuento = descuento;

            detailGrid.Porcentage = CalculateDiscountPorcentage(detailGrid, descuento);

            detailGrid.Subtotal = ((detailGrid.PrecioVenta * Convert.ToInt16( detailGrid.Cantidad)) - descuento).ToString("F");

            return detailGrid;
        }

    }
}

