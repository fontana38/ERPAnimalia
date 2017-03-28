using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERPAnimalia.Models
{
    public class CabeceraComprobanteModel
    {
        public Guid IdCabeceraComprobante;
        public Guid IdDetalleComprobante;
        public Guid IdCliente;
        public Guid IdTipoComprobante;
        public DateTime Fecha;
        public string Numero;
    }
}