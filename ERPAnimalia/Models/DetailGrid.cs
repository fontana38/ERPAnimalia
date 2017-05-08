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
     public double PrecioVenta { get; set; }
     public decimal Cantidad { get; set; }
     public double Descuento { get; set; }
     public double Subtotal { get; set; }
        public double Total { get; set; }



    }
}