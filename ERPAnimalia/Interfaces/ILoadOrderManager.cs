﻿using ERPAnimalia.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPAnimalia.Interfaces
{
    interface ILoadOrderManager
    {
        List<ProveedorModel> GetProveedor();
       

    }
}