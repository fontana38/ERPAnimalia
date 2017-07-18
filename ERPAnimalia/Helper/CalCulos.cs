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
            var pesos = (PrecioVenta.Value - precioCosto.Value).ToString("f2");            
            return pesos;
        }

        public static string CalcularRentabilidadPorcentage(decimal precioCosto, decimal PrecioVenta)
        {
            var porcentage = (1 - precioCosto / PrecioVenta).ToString("f2");
            return porcentage;
        }

        public static decimal CalculateTotalKg(decimal kg, decimal cantidad)
        {
            var total = kg * cantidad;
            return total;
        }
    }
}