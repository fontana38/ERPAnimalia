using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERPAnimalia.Models
{
    public class ListOfAmountModel
    {
        public int IdList { get; set; }
        public double Amount { get; set; }
        public DateTime DateInitial { get; set; }
        public string Name { get; set; }

        
    }
}