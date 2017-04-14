using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERPAnimalia.Models
{
    public class VoucherDetailModel
    {
        public Guid IdDetalleComprobante;
        public Guid IdProducto;
        public decimal Cantidad;
        public decimal Total;
        public decimal Subtotal;
        public List<ProductModels> ProductModel;
    }
}