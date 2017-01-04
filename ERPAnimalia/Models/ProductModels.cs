using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ERPAnimalia.Models
{
    public class ProductModels
    {
        public Guid IdProducto { get; set; }
        [Required(ErrorMessage = "Por favor Ingrese el Código")]
        [MaxLength(50)]
        [Display(Name = "Código")]
        public string Codigo { get; set; }

        [MaxLength(50)]
        [Display(Name = "Marca")]
        public string Marca { get; set; }

        [Required(ErrorMessage = "Por favor Ingrese la Descripción1")]
        [MaxLength(50)]
        [Display(Name = "Descripción1")]
        public string Descripcion1 { get; set; }

        [Required(ErrorMessage = "Por favor Ingrese la Descripción2")]
        [MaxLength(50)]
        [Display(Name = "Descripción2")]
        public string Descripcion2 { get; set; }

        [MaxLength(50)]
        [Display(Name = "Código de Barra")]
        public string CodigoBarra { get; set; }

        [Display(Name = "Cantidad")]
        [Required(ErrorMessage = "Por favor Ingrese la Cantidad")]
        public int? Cantidad { get; set; }

        public int? kg { get; set; }

        [Required(ErrorMessage = "Por favor Ingrese la Categoria")]

        public int? IdCategory { get; set; }
        [Required(ErrorMessage = "Por favor Ingrese la SubCategoria")]
        public int? IdSubCategory { get; set; }

        public Guid? IdListaPrecio { get; set; }

        [Display(Name = "Categoria")]
        public List<CategoryModel> Category { get; set; }
   
        [Display(Name = "Sub Categoria")]
        public List<SubCategoryModel> SubCategory { get; set; }

        [Display(Name = "Categoria")]
        public CategoryModel CategoryItem { get; set; }

        [Display(Name = "Sub Categoria")]
        public SubCategoryModel SubCategoryItem { get; set; }

        public List<ListaPrecioModel> ListaPrecio { get; set; }

        public ListaPrecioModel ListaPrecioItem { get; set; }
    }


}


    
