using ERPAnimalia.Models;
using ERPAnimalia.Models.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERPAnimalia.Factory
{
    public static class LoadOrderFacory
    {
        public static LoadOrderManager CreateOrderManager()
        {
            return new LoadOrderManager();
        }
        public static VoucherHeadLoadOrder CreateVoucherHeadLoadOrder()
        {
            return new VoucherHeadLoadOrder();
        }
    }
}