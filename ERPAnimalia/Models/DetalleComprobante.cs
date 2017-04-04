using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERPAnimalia.Models
{
    public class DetalleComprobante
    {
        public Guid IdDetalleComprobante;
        public Guid IdProducto;
        public decimal Cantidad;
        public decimal Total;
    }
}