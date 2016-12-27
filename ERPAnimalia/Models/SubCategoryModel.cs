using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ERPAnimalia.Models
{
    public class SubCategoryModel
    {
        public int IdSubCategory { get; set; }

        [Display(Name = "SubCategoria")]
        public string Name { get; set; }
    }
}