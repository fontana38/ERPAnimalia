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
    
    public partial class AmountProductAsoc
    {
        public Nullable<System.Guid> IdProduct { get; set; }
        public Nullable<System.Guid> IdAmountList { get; set; }
        public System.Guid IdAmountProduct { get; set; }
    
        public virtual AmountListProduct AmountListProduct { get; set; }
        public virtual Product Product { get; set; }
    }
}
