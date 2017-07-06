using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERPAnimalia.Helper
{
    public class CalCulos
    {
        public static string CalcularRentabilidad(double? precioCosto, double? PrecioVenta)
        {
            var pesos = (PrecioVenta.Value - precioCosto.Value).ToString("f2");            
            return pesos;
        }

        public static string CalcularRentabilidadPorcentage(double precioCosto, double PrecioVenta)
        {
            var porcentage = (precioCosto / PrecioVenta).ToString("f2");
            return porcentage;
        }
    }
}