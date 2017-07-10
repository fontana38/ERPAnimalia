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
        public string FechaCompra1 { get; set; }
        public string FechaCompra2 { get; set; }
        public DateTime FechaCompra { get; set; }
        public DateTime FechaProximaCompra { get; set; }
        public Guid[] IdsProduct { get; set; }

        public string NombreProducto { get; set; }
        public int Dias { get; set; }
        public List<ProductModels> Productos { get; set; }
    }
}
