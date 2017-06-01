using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERPAnimalia.Models
{
    public abstract class VoucherHeadGeneral
    {
        public  Guid IdComprobante;        
        public DateTime Fecha;
        public string Numero;
        public int IdtipoComprobante;
        public int IdformaDePago;
        public decimal Total;
        bool Cobrada;
    }
}