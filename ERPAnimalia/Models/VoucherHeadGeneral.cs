using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERPAnimalia.Models
{
    public abstract class VoucherHeadGeneral
    {
        public  Guid IdComprobante { get; set; }       
        public DateTime Fecha { get; set; }
        public string Numero { get; set; }
        public int IdtipoComprobante { get; set; }
        public int IdformaDePago { get; set; }
        public decimal Total { get; set; }
        bool Cobrada { get; set; }
        public string comentario { get; set; }
    }
}