using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERPAnimalia.Models
{
    public class VoucherHeadModel
    {
        public Guid IdComprobante;
        public Guid IdCliente;      
        public DateTime Fecha;
        public string Numero;
        public int IdtipoComprobante;
        public int IdformaDePago;
        public decimal Total;
        bool Cobrada;
        public List<ClienteModel> ClientModel;
    }
}