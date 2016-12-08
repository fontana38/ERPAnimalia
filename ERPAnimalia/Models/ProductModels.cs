using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ERPAnimalia.Models
{
    public class ProductModels
    {
        public Guid IdProduct { get; set; }
        public string Codigo { get; set; }
        public string Name { get; set; }

        [MaxLength(50)]
        [Display(Name = "Descripción")]

        public string Description { get; set; }
        [MaxLength(50)]
        public string barCode { get; set; }

        public int? quantity { get; set; }
        public string kg { get; set; }
    }


    
}