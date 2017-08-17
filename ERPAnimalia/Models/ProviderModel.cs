using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERPAnimalia.Models
{
    public class ProviderModel :PersonModels
    {
        public Guid IdProveedor { get; set; }
        public DateTime Fecha { get; set; }
        public string FechaString { get; set; }
        public List<ProductModels> Productos { get; set; }
        public Guid[] IdsProduct { get; set; }

    }
}