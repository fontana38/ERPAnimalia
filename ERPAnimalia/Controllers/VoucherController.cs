﻿using ERPAnimalia.Interfaces;
using ERPAnimalia.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ERPAnimalia.Controllers
{

    [RoutePrefix("Factura")]
    public class VoucherController : Controller
    {
        public IVoucherHeadManager HeadVoucherManager { get; set; }
        public IVoucherDetailManager VoucherDetailManager { get; set; }
        public VoucherDetailModel voucherDetailModel;
        public List<DetailGrid> Listproduct;
        public VoucherController()
        {
            HeadVoucherManager = Factory.VoucherFactory.CreateVoucherHeadManager();
            VoucherDetailManager = Factory.VoucherFactory.CreateVoucherDetailManager();
        }
    // GET: Voucher
    public ActionResult Index()
        {
            return View();
        }

        // GET: Voucher
        [Route("CrearFactura")]
        public ActionResult HeadVoucher()
        {
         
            var voucherModel =Factory.VoucherFactory.CreateVoucherHeadModel();
             voucherDetailModel = Factory.VoucherFactory.CreateVoucherDetailModel();
           
            return View("~/Views/Voucher/Voucher.cshtml");
        }

        // GET: Voucher
        [HttpPost]
        public JsonResult GetClient(string term)
        {
            
            var voucherModel = Factory.VoucherFactory.CreateVoucherHeadModel();
            var listClient = HeadVoucherManager.GetClient();
            voucherModel.clientModel = listClient;

            var clientName = (from N in listClient
                              where N.Nombre.StartsWith(term) || N.Apellido.StartsWith(term)
                              select new {N.NombreCompleto }).ToList();

            return Json(clientName, JsonRequestBehavior.AllowGet);

           
        }

        [HttpPost]
        public JsonResult GetProduct(string term)
        {

            voucherDetailModel = Factory.VoucherFactory.CreateVoucherDetailModel();
            var listProduct = VoucherDetailManager.GetProduct();
            voucherDetailModel.ProductModel = listProduct;

          var Descripcion1 = (from N in listProduct
                              where N.Descripcion1.StartsWith(term) || N.Codigo.StartsWith(term)
                              select new { N.Descripcion1 }).ToList();
          


            return Json(Descripcion1, JsonRequestBehavior.AllowGet);


        }

        [HttpGet]
        public JsonResult GetProductDetail(int? page, int? limit, string term, int cantidad = 0, double descuento= 0)
        {
            var detailGridTemp = TempData["DetailGrid"] as List<DetailGrid>;
            var detailGridList = new List<DetailGrid>();
            if (String.IsNullOrEmpty(term))
            {
                term = string.Empty;
            }

            int total;

            voucherDetailModel = Factory.VoucherFactory.CreateVoucherDetailModel();
            var listProduct = VoucherDetailManager.GetProduct();
            voucherDetailModel.ProductModel = listProduct;

            var Descripcion1 = (from N in listProduct
                                where N.Descripcion1.StartsWith(term) 
                                select new { N }).ToList();

          

            var detailGrid = new DetailGrid();

            if (Descripcion1.Count != 0 && term != string.Empty)
            {
                detailGrid.IdProduct = Descripcion1[0].N.IdProducto;
                detailGrid.Codigo = Descripcion1[0].N.Codigo;
                detailGrid.Descripcion1 = Descripcion1[0].N.Descripcion1;
                detailGrid.PrecioVenta = (double)Descripcion1[0].N.ListaPrecioItem.PrecioVenta;
                detailGrid.Cantidad = cantidad;
                detailGrid.Descuento = descuento;

                detailGrid.Subtotal = (detailGrid.PrecioVenta * cantidad) - descuento; 
               
            }

            if (detailGridTemp != null )
            {
                var newRowExist = detailGridTemp.Find(x => x.IdProduct == detailGrid.IdProduct);
 
                if (newRowExist == null)
            {
                    detailGridTemp.Add(detailGrid);
                    foreach (var temp in detailGridTemp)
                    {
                        var detailRow = new DetailGrid();
                        detailRow.Codigo = temp.Codigo;
                        detailRow.PrecioVenta = temp.PrecioVenta;

                        detailGridList.Add(detailRow);
                    }

                }
            }
            else if(!String.IsNullOrEmpty(term))
            {
                detailGridTemp = new List<DetailGrid>();
                detailGridTemp.Add(detailGrid);
                foreach (var temp in detailGridTemp)
                {
                    var detailRow = new DetailGrid();
                    detailRow.Codigo = temp.Codigo;
                    detailRow.PrecioVenta = temp.PrecioVenta;

                    detailGridList.Add(detailRow);
                }
            }


            if (detailGridList != null)
            {
                TempData["DetailGrid"] = detailGridTemp;
            }

             var records= (detailGridTemp!=null) ? detailGridTemp: detailGridList;

             total = (detailGridTemp != null) ? detailGridTemp.Count() : detailGridList.Count();
            return Json(new { records, total }, JsonRequestBehavior.AllowGet);


        }


        [HttpGet]
        public JsonResult GetSubtotal( Guid? idProduct,int cantidad = 0, double descuento = 0)
        {
            var detailGridTemp = TempData["DetailGrid"] as List<DetailGrid>;
            double total = 0;
            foreach (var item in detailGridTemp)
            {
                total += item.Subtotal;
            } 

            DetailGrid product = detailGridTemp.Find(x => x.IdProduct == idProduct);

            product.Cantidad = cantidad;
            product.Descuento = descuento;
            product.Total = total;

            product.Subtotal = (product.PrecioVenta * cantidad) - descuento;
            TempData["DetailGrid"] = detailGridTemp;
            var records = detailGridTemp;
            var total = detailGridTemp.Count;
            return Json(new { records, total }, JsonRequestBehavior.AllowGet);
        }



    }
}