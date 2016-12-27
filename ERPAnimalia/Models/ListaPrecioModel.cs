using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERPAnimalia.Models
{
    public class ListaPrecioModel
    {
        public Guid IdLitaPrecio { get; set; }
        public string Nombre { get; set; }
        public double PrecioCosto { get; set; }
        public double PrecioVenta { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFinal { get; set; }

    }
}