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
        public string errorStock { get; set; }

        public VoucherDetailManager()
        {
            db = Factory.Factory.CreateContextDataBase();
            _ProducManager = Factory.Factory.CreateProducManager();
        }
        public List<TipoComprobante> GetTipoComprobante()
        {

            var tipoComprobante = db.TipoComprobante.Where(x=>x.IdTipoComprobante != 4).ToList();
            return tipoComprobante;
        }

        public List<FormaDePago> GetFormaDePago()
        {
            var formaDePago = db.FormaDePago.ToList();
            return formaDePago;
        }

        public List<ProductModels> GetProduct(string term)
        {
            int total = 0;   
            return _ProducManager.GetProductList(null,null,null, null,term, out total);
        }

        public List<ProductModels> GetProduct()
        {
            return _ProducManager.MapProduct();
        }

        public bool SaveVoucher(List<DetailGrid> detailGridTemp, VoucherHeadModel head)
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
                                if(product.IdCategory != (int)Enumeration.Category.Accesorios)
                                {
                                    if (product.IdSubCategory != (int)Enumeration.Subcategory.Suelto)
                                    {
                                        product.Cantidad = product.Cantidad - item.Cantidad;
                                        product.TotalKg = product.TotalKg - (item.Cantidad * product.Kg);
                                    }
                                    else
                                    {
                                        product.TotalKg = product.TotalKg - item.Cantidad;
                                    }
                                }
                                else
                                {
                                    product.Cantidad = product.Cantidad - item.Cantidad;
                                }
                                
                               
                            }
                            else
                            {
                                return false;
                            }

                            db.DetalleComprobante.Add(item);
                        }
                       
                        db.SaveChanges();
                        dbContextTransaction.Commit();
                        return true;
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
           
            var voucherDetailModelList = Factory.VoucherFactory.CreateVoucherDetailModelList();
            var listProduct = GetProduct();

            if (detailGridTemp != null)
            {
                foreach (var item in detailGridTemp)
                {
                    var voucherDetailModel = Factory.VoucherFactory.CreateVoucherDetailModel();
                    voucherDetailModel.Cantidad = item.Cantidad;
                    voucherDetailModel.Subtotal = Convert.ToDecimal(item.Subtotal);
                    voucherDetailModel.PrecioCosto = item.PrecioCosto;
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
           
            if(product.IdCategory == (int)Enumeration.Category.Alimento)
            {
                if (product.IdSubCategory != (int)Enumeration.Subcategory.Suelto)
                {
                    if (comprobante.Cantidad <= product.Cantidad)
                    {
                        return true;
                    }
                    else
                    {
                        errorStock = String.Concat(product.Descripcion1);
                        return false;
                    }
                }
                else
                {
                    if (comprobante.Cantidad <= product.TotalKg)
                    {
                        return true;
                    }
                    else
                    {
                        errorStock = String.Concat(product.Descripcion1);
                        return false;
                    }
                }
            }
            else
            {
                if (comprobante.Cantidad <= product.Cantidad)
                {
                    return true;
                }
                else
                {
                    errorStock = String.Concat(product.Descripcion1);
                    return false;
                }
            }
        }

        public decimal CalculateDiscountPorcentage(DetailGrid row,decimal discount)
        {
            if(row.Subtotal != null)
            {
                row.Descuento = discount;
                //row.Subtotal = ((row.PrecioVenta * Convert.ToInt16(row.Cantidad)) - discount).ToString("F");
                row.Porcentage = (discount / Convert.ToDecimal(row.Subtotal));
                return row.Porcentage;
            }
            
            return row.Porcentage;
        }

        public DetailGrid SetValuesNewRowTable(DetailGrid detailGrid, decimal cantidad,  decimal descuento)
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

