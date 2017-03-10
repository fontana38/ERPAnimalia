﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ERPAnimalia.Models;
using PagedList;


namespace ERPAnimalia.Controllers
{
    [RoutePrefix("Cliente")]
    public class ClienteController : Controller
    {
        public IClinteManager ClienteManagers { get; set; }
        public ProductManager ProductManagers { get; set; }
        public IListProduct ListProductManagers { get; set; }

       
        public ManagerListOfAmount ManagerList { get; set; }

        public ClienteController()
        {
            ClienteManagers = new ClienteManager();
            ProductManagers = Factory.Factory.CreateProducManager();
            ListProductManagers = Factory.Factory.CreateListProducManager();
        }
        // GET: Cliente
        public ActionResult Index()
        {
            //ViewData["producto"] = ProductManagers.GetAllProduct();
            //var listCliente=ClienteManagers.ObtenerCliente();
            return View("Cliente");
        }

        public ActionResult GuardarCliente(ClienteModel cliente)
        {
            ClienteManagers.GuardarCliente(cliente);
            return View("Cliente", new ClienteModel());
        }

        [HttpGet]
        public JsonResult GetCliente(int? page, int? limit, string sortBy, string direction, string searchString = null)
        {
            int total;
           
           
            var records = ClienteManagers.ObtenerCliente(page, limit, sortBy, direction, searchString, out total);
            

            return Json(new { records, total }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Save(ClienteModel cliente)
        {
            ClienteManagers.GuardarCliente(cliente);
            return Json(true);
        }

        [HttpPost]
        public JsonResult Remove(Guid idCliente)
        {
             ClienteManagers.BorrarCliente(idCliente);
            return Json(true);
        }

        // GET: ListProduct


        [HttpGet]
        public JsonResult GetProduct(int? page, int? limit, string sortBy, string direction, string searchString = null)
        {
            int total;
           

           var records = ProductManagers.GetProductList(page, limit, sortBy, direction, searchString, out total);
            

            return Json(new { records, total }, JsonRequestBehavior.AllowGet);
        }
    }
}