using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ERPAnimalia.Models;
using ERPAnimalia.EntityFramework;

namespace ERPAnimalia.Interfaces
{
    public interface IVoucherDetailManager
    {
        List<ProductModels> GetProduct();
        string SaveVoucher(List<DetailGrid> detailGridTemp,VoucherHeadModel head);
        List<FormaDePago> GetFormaDePago();
        List<TipoComprobante> GetTipoComprobante();
        List<VoucherDetailModel> MappModels(List<DetailGrid> detailGridTemp);
        decimal CalculateDiscountPorcentage(DetailGrid row, decimal discount);

        DetailGrid SetValuesNewRowTable(DetailGrid detailGrid, int cantidad, decimal descuento);
    }
}
