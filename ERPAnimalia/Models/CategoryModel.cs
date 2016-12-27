using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ERPAnimalia.Models
{
    public class CategoryModel
    {
        public int IdCategory { get; set; }

        [Display(Name = "Categoria")]
        public string Name { get; set; }

        public SubCategoryModel SubCategoryItem { get; set; }
        public List<SubCategoryModel> SubCategory { get; set; }
    }
}