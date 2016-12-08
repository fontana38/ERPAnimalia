using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERPAnimalia.Models
{
    public class ProductAndListModel
    {
        public int Idproduct { get; set; }
        public int IdList { get; set; }
        public int IdProductAndList { get; set; }
        public virtual ICollection<ProductModels> ProductModel { get; set; }
        public virtual ICollection<ListOfAmountModel> ListOfAmountModel { get; set; }

    }
}