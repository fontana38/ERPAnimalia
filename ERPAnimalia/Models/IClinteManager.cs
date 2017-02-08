using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERPAnimalia.Models
{
    public interface IClinteManager
    {
        void GuardarCliente(ClienteModel cliente);
        List<ClienteModel> ObtenerCliente();
    }
}