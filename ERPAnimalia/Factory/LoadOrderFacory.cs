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
            var productManager=Factory.CreateProducManager();
            return new LoadOrderManager(productManager);
        }
        public static VoucherHeadLoadOrder CreateVoucherHeadLoadOrder()
        {
            return new VoucherHeadLoadOrder();
        }
    }
}