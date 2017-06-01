using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERPAnimalia.Models
{
    public class VoucherHeadLoadOrder:VoucherHeadGeneral
    {
        public Guid IdProveedor;
        public List<ProveedorModel> ProveedorModel;
    }
}