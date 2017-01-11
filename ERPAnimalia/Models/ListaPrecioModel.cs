using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ERPAnimalia.Models
{
    public class ListaPrecioModel
    {
        public Guid IdLitaPrecio { get; set; }
        [MaxLength(50)]
        public string Nombre { get; set; }
        [ DataType(DataType.Currency)]
        public decimal PrecioCosto { get; set; }
        [ DataType(DataType.Currency)]
        public decimal PrecioVenta { get; set; }
        [Display(Name = "FechaInicio"), DataType(DataType.Date)]
        public DateTime FechaInicio { get; set; }
        [Display(Name = "FechaFinal"), DataType(DataType.Date)]
        public DateTime FechaFinal { get; set; }

    }
}