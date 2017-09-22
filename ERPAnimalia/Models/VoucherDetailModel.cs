using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ERPAnimalia.Models
{
    public class VoucherDetailModel
    {
        public Guid IdDetalleComprobante { get; set; }
        public Guid IdProducto { get; set; }
        public decimal Cantidad { get; set; }
        public decimal Subtotal { get; set; }
        public decimal PrecioVenta { get; set; }
        public decimal PrecioCosto { get; set; }
        public decimal Descuento { get; set; }
        public List<ProductModels> ProductModel { get; set; }
        public List<String> ProductName { get; set; }

        [MaxLength(50)]
        public string Comentario { get; set; }

        public Guid IdCabeceraComprobante { get; set; }

        public Guid IdCliente { get; set; }
        public Guid IdTipoComprobante { get; set; }
        public DateTime Fecha { get; set; }
        public string Numero { get; set; }

        public List<ClienteModel> clientModel { get; set; }
    }
}