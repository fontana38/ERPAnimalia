using ERPAnimalia.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERPAnimalia.Helper
{
    public static class Common
    {
        public static AnimaliaPetShopEntities db { get; set; }

        public static List<FormaDePago> GetFormaPagoLoadOrder()
        {
            db = Factory.Factory.CreateContextDataBase();
            var formaDePago = db.FormaDePago.Where(x => x.IdFormaDePago > 2).ToList();
            return formaDePago;
        }
    }
}