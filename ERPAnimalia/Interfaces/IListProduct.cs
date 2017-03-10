using ERPAnimalia.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Results;
using System.Web.Mvc;

namespace ERPAnimalia
{
    public interface IListProduct
    {
        List<ProductModels> GetProduct(int? page, int? limit, string sortBy, string direction, string searchString, out int total);
    }
}
