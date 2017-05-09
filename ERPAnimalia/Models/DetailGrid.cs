using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERPAnimalia.Models
{
    public class DetailGrid
    { 
     public Guid IdProduct { get; set; }
        public string Codigo { get; set; }
     public string Descripcion1 { get; set; }
     public decimal PrecioVenta { get; set; }
     public decimal Cantidad { get; set; }
     public decimal Descuento { get; set; }
     public decimal Subtotal { get; set; }
     public decimal Total { get; set; }
    }
}