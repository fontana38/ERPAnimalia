using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERPAnimalia.Helper
{
    public class CalCulos
    {
        public static string CalcularRentabilidad(decimal? precioCosto, decimal? PrecioVenta)
        {
            var porcentage =  (PrecioVenta.Value - precioCosto.Value).ToString("F"); ;
            return porcentage;
        }

        public static string CalcularRentabilidadPorcentage(decimal precioCosto, decimal PrecioVenta)
        {
            var porcentage = ( precioCosto / PrecioVenta).ToString("F"); ;
            return porcentage;
        }
    }
}