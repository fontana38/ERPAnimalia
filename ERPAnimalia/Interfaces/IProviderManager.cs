using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ERPAnimalia.Models;

namespace ERPAnimalia.Interfaces
{
    public interface IProviderManager
    {
        void SaveProvider(ProviderModel providerModel);
        void DeleteProvider(Guid idProvider);
        
        List<ProviderModel> GetProvider(int? page, int? limit, string sortBy, string direction, string searchString, out int total);
    }
}
