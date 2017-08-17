using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERPAnimalia.Models
{
    public class ProductListModel
    {
        public Guid IdProductoLista { get; set; }
        public Guid? IdProducto { get; set; }
        public Guid? IdListaPrecio { get; set; }
    }
}