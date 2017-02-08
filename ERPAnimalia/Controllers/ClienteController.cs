using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ERPAnimalia.Models;

namespace ERPAnimalia.Controllers
{
    [RoutePrefix("Cliente")]
    public class ClienteController : Controller
    {
        public IClinteManager ClienteManagers { get; set; }

        public ClienteController()
        {
            ClienteManagers = new ClienteManager();
        }
        // GET: Cliente
        public ActionResult Index()
        {
            var listCliente=ClienteManagers.ObtenerCliente();
            return View("Cliente", listCliente);
        }

        public ActionResult GuardarCliente(ClienteModel cliente)
        {
            ClienteManagers.GuardarCliente(cliente);
            return View("Cliente", new ClienteModel());
        }
    }
}