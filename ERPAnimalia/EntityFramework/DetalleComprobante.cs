//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ERPAnimalia.EntityFramework
{
    using System;
    using System.Collections.Generic;
    
    public partial class DetalleComprobante
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DetalleComprobante()
        {
            this.CabeceraComprobante = new HashSet<CabeceraComprobante>();
        }
    
        public System.Guid IdDetalleComprobante { get; set; }
        public System.Guid IdProducto { get; set; }
        public Nullable<decimal> Cantidad { get; set; }
        public Nullable<decimal> Total { get; set; }
        public Nullable<System.Guid> IdFormaDePago { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CabeceraComprobante> CabeceraComprobante { get; set; }
        public virtual FormaDePago FormaDePago { get; set; }
    }
}
