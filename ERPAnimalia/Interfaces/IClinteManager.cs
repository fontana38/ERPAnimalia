using ERPAnimalia.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERPAnimalia.Interface
{
    public interface IClinteManager
    {
        void GuardarCliente(ClienteModel cliente);
        void BorrarCliente(Guid IdCliente);
        List<ClienteModel> ObtenerCliente();
        List<ClienteModel> ObtenerCliente(int? page, int? limit, string sortBy, string direction, string searchString, out int total);
    }
}