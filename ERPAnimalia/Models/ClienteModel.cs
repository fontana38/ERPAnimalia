using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERPAnimalia.Models
{
    public class ClienteModel:PersonaModels
    {
        public Guid IdCliente { get; set; }
        public string Mascota { get; set; }
        public DateTime FechaCompra { get; set; }
        public DateTime FechaProximaCompra { get; set; }
        public Guid IdProducto { get; set; }
        public string NombreProducto { get; set; }
        public ICollection<ProductModels> Productos { get; set; }
    }
}
