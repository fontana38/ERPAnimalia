using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERPAnimalia.Models
{
    public class VoucherHeadModel:VoucherHeadGeneral
    {       
        public Guid IdCliente;
        public List<ClienteModel> ClientModel;
    }
}