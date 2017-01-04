using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERPAnimalia.Models
{
    public class TransferToFreeProduct
    {
        public Guid IdTranferToFreeProduct { get; set; }
        public Guid IdProductoBolsa { get; set; }
        public Guid IdProductFree { get; set; }
        public DateTime DateTransfer { get; set; }
        public int? CantidadBolasa { get; set; }
        public int? CantidadKilos { get; set; }
        public ICollection<ProductModels> ProductModel { get; set; }

    }
}