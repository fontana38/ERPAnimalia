﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ERPAnimalia.Models;
using ERPAnimalia.Interfaces;


namespace ERPAnimalia.Factory
{
    using ERPAnimalia.Interface;
    using ERPAnimalia.Models.Manager;
    public static class VoucherFactory
    {

        public static VoucherHeadModel CreateVoucherHeadModel()
        {
            return new VoucherHeadModel();
        }
        public static IVoucherHeadManager CreateVoucherHeadManager()
        {
            return new VoucheHeadManager();
        }

    }
}