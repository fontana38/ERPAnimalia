using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERPAnimalia.Models
{
    public class ProductDetailModel
    {
        public Guid IdProductDetail { get; set; }
        public Guid IdProducto { get; set; }
        public DateTime Dia { get; set; }
        public int CantidadBolsa { get; set; }
        public int? CantidadKilos { get; set; }

    }
}