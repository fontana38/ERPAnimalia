using ERPAnimalia.Models;
using System.Collections.Generic;

namespace ERPAnimalia
{
    public interface IListProduct
    {
        List<ProductModels> GetProduct(int? page, int? limit, string sortBy, string direction, string searchString, out int total);
    }
}
