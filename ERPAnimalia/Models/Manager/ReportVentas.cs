using ERPAnimalia.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERPAnimalia.Models.Manager
{
    public class ReportVentas
    {
        public AnimaliaPetShopEntities db { get; set; }

        public ReportVentas()
        {
            db = Factory.Factory.CreateContextDataBase();
        }


        public void GetreportSellDaily()
        {
           //var sellsdaily= db.VentaDiariaMes;
        }
    }
}