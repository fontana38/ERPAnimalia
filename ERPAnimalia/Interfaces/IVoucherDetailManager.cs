using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ERPAnimalia.Models;

namespace ERPAnimalia.Interfaces
{
    public interface IVoucherDetailManager
    {
        List<ProductModels> GetProduct();
        void SaveVoucher(List<DetailGrid> list);
    }
}
