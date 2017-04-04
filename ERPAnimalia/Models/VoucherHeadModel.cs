using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERPAnimalia.Models
{
    public class VoucherHeadModel
    {
        public Guid IdCabeceraComprobante;
        public Guid IdDetalleComprobante;
        public Guid IdCliente;
        public Guid IdTipoComprobante;
        public DateTime Fecha;
        public string Numero;
        List<TipoComprobante> Tipo;
        List<ClienteModel> clientModel;
    }
}