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
     public double PrecioCosto { get; set; }
        public decimal Cantidad { get; set; }
     public decimal kg { get; set; }
     public double Descuento { get; set; }
     public string Subtotal { get; set; }
     public decimal Total { get; set; }
     public double Porcentage { get; set; }
     public int CategoryItem { get; set; }
     public int SubCategoryItem { get; set; }
    }
}