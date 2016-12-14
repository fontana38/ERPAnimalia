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

        [Display(Name = "Código")]
        public string Codigo { get; set; }

        [Display(Name = "Nombre")]
        public string Name { get; set; }

        [MaxLength(50)]
        [Display(Name = "Descripción")]

        public string Description { get; set; }
        [MaxLength(50)]
        [Display(Name = "Código de Barra")]
        public string barCode { get; set; }

        [Display(Name = "Cantidad")]
        public int? quantity { get; set; }

        public int? kg { get; set; }
    }


    
}