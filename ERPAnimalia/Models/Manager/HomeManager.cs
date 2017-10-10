using ERPAnimalia.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Web;

namespace ERPAnimalia.Models.Manager
{
    public class HomeManager
    {
        public static AnimaliaPetShopEntities db { get; set; }

        public HomeManager()
        {
            db = Factory.Factory.CreateContextDataBase();
        }

        public List<VentaDiariaMesModel> GetVentasDiaria()
        {
            // using Object Context (EF4.0)  
           
           var dia = db.VentaDiariaMes().ToList();
           var mes = db.VentaMes().ToList();
           var vtaModel = Factory.ReportFactory.CreateListventaDiariaModel();
            var venta = Factory.ReportFactory.CreateventaDiariaModel();

            venta.Dia = (dia != null) ? dia.First().Dia : 0;
            venta.Mes = (mes.First().Mes != null) ? mes.First().Mes : 0;

            vtaModel.Add(venta);
            return vtaModel;
        }
    }
}