using ERPAnimalia.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERPAnimalia.Factory
{
    public static class ReportFactory
    {
        public static List<VentaDiariaMesModel>  CreateListventaDiariaModel()
        {
            return new List<VentaDiariaMesModel>();
        }

        public static VentaDiariaMesModel CreateventaDiariaModel()
        {
            return new VentaDiariaMesModel();
        }
    }
}