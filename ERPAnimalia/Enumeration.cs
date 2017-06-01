using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERPAnimalia
{
    public class Enumeration
    {
        public enum Category : int
        {
            Alimento =1,
            Accesorios=2

        }

        public enum Subcategory : int
        {
            Suelto = 1,
            Bolsa = 2

        }

        public enum TipoComprobante : int
        {
            FacturaA = 1,
            FacturaB = 2,
            NoteCredito=3
        }

        public enum FormaDePago : int
        {
            Efectivo = 1,
            CtaCte = 2,
            TarjetaCredito = 3,
            TarjetaDebito=4
        }

        public enum TipoVenta : int
        {
           Cantidad=1,
           Kg=2
        }
    }
}