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
        public System.Guid IdDetalleComprobante { get; set; }
        public System.Guid IdProducto { get; set; }
        public Nullable<decimal> Cantidad { get; set; }
        public Nullable<decimal> Subtotal { get; set; }
        public Nullable<decimal> PrecioVenta { get; set; }
        public Nullable<decimal> PrecioCosto { get; set; }
        public Nullable<System.Guid> IdComprobante { get; set; }
    
        public virtual Comprobante Comprobante { get; set; }
    }
}
