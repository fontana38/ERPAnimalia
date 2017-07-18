using ERPAnimalia.Interfaces;
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


            ViewData["listTipoComprobante"] = VoucherDetailManager.GetTipoComprobante();
            ViewData["listFormaPAgo"] = VoucherDetailManager.GetFormaDePago();


            return View("~/Views/Voucher/Voucher.cshtml");
        }

        // GET: Voucher
        [HttpPost]
        public JsonResult GetClient(string term)
        {
            
            var voucherModel = Factory.VoucherFactory.CreateVoucherHeadModel();
            var listClient = HeadVoucherManager.GetClient();
            voucherModel.ClientModel = listClient;

            var clientName = (from N in listClient
                              where N.Nombre.StartsWith(term) || N.Apellido.StartsWith(term)
                              select new {N.NombreCompleto,N.Direccion,N.Telefono }).ToList();

            return Json(clientName, JsonRequestBehavior.AllowGet);

           
        }

        // GET: Voucher
        [HttpPost]
        public JsonResult GetClientSelect(string term)
        {

            var voucherModel = Factory.VoucherFactory.CreateVoucherHeadModel();
            var listClient = HeadVoucherManager.GetClient();
            voucherModel.ClientModel = listClient;

            var clientName = (from N in listClient
                              where N.NombreCompleto.Equals(term)
                              select new { N.NombreCompleto, N.Direccion, N.Telefono }).ToList();

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
        public JsonResult GetProductDetail(int? page, int? limit, string term, int cantidad = 0, decimal descuento = 0)
        {      
            var detailGridTemp = TempData["DetailGrid"] as List<DetailGrid>;
            var detailGridList = new List<DetailGrid>();
            int total;
            var isDelete = TempData["isDelete"] as string;
            if (String.IsNullOrEmpty(term) || isDelete=="true")
            {
                term = string.Empty;
                TempData["isDelete"] = "false";
            }

            if (!String.IsNullOrEmpty(term))
            {

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
                    detailGrid.PrecioVenta =  Descripcion1[0].N.PrecioVenta.Value;
                    detailGrid.PrecioCosto = Descripcion1[0].N.PrecioCosto.Value;
                    detailGrid.CategoryItem = Descripcion1[0].N.CategoryItem.IdCategory;
                    detailGrid.SubCategoryItem = Descripcion1[0].N.SubCategoryItem.IdSubCategory;

                    detailGrid = VoucherDetailManager.SetValuesNewRowTable(detailGrid, cantidad, descuento);
                }

                if (detailGridTemp != null)
                {
                    var newRowExist = detailGridTemp.Find(x => x.IdProduct == detailGrid.IdProduct);

                    if (newRowExist == null)
                    {
                        detailGridTemp.Add(detailGrid);
                        
                    }
                }
                else if (!String.IsNullOrEmpty(term))
                {
                    detailGridTemp = new List<DetailGrid>();
                    detailGridTemp.Add(detailGrid);
                }

                if (detailGridTemp != null)
                {
                    TempData["DetailGrid"] = detailGridTemp;
                }

                if (detailGridTemp != null)
                {
                    detailGridTemp.Last().Total = 0;
                    var totalRow = new decimal();
                    foreach (var item in detailGridTemp)
                    {
                        totalRow += Convert.ToDecimal(item.Subtotal);
                    }

                    detailGridTemp.Last().Total = Decimal.Round( totalRow,2);
                }

            }
            

             var records= (detailGridTemp!=null) ? detailGridTemp: detailGridList;
           
            total = (detailGridTemp != null) ? detailGridTemp.Count() : detailGridList.Count();
            return Json(new { records, total }, JsonRequestBehavior.AllowGet);


        }


        [HttpGet]
        public JsonResult GetSubtotal(Guid? idProduct, int cantidad = 0, decimal descuento = 0)
        {
            var detailGridTemp = TempData["DetailGrid"] as List<DetailGrid>;
            var records = new List<DetailGrid>();
            decimal total = 0;
            if (detailGridTemp != null)
            {
                DetailGrid product = detailGridTemp.Find(x => x.IdProduct == idProduct);
                product.Cantidad = cantidad;
                product.Descuento = descuento;
                product.Subtotal = ((product.PrecioVenta * cantidad) - descuento).ToString("F"); 
                product.Total = CalculateTotal(detailGridTemp);
                product.Porcentage = VoucherDetailManager.CalculateDiscountPorcentage(product, descuento);

                TempData["DetailGrid"] = detailGridTemp;
                records = detailGridTemp;
                total = detailGridTemp.Count;
            }

            return Json(new { records, total }, JsonRequestBehavior.AllowGet);
        }

        private decimal CalculateTotal(List<DetailGrid> detailGridTemp)
        {
            foreach (var item in detailGridTemp)
            {
                item.Total += Convert.ToDecimal(item.Subtotal);
               
            }
            return detailGridTemp.Last().Total;

        }
            
          
           

        [HttpPost]
        public JsonResult Save(string cliente,string date, int comprobante,int formaDePago)
        {
            var detailGridTemp = TempData["DetailGrid"] as List<DetailGrid>;

            var listClient = HeadVoucherManager.GetClient();

            var idClient = listClient.Find(x => x.NombreCompleto == cliente).IdCliente;
            var voucherHeadModel =Factory.VoucherFactory.CreateVoucherHeadModel();
            voucherHeadModel.IdformaDePago = formaDePago;
            voucherHeadModel.IdtipoComprobante = comprobante;
            voucherHeadModel.Fecha = DateTime.Parse(date).Date;
            voucherHeadModel.IdCliente = idClient;
            var IsSave =VoucherDetailManager.SaveVoucher(detailGridTemp,voucherHeadModel);
            var message = string.Empty;
            if (IsSave)
            {
                TempData["DetailGrid"] = null;
                message = "El comprobante fue guardado";
            }
            else
            {
                message = "El stock es insuficiente";
            }
            
            return Json(message);
        }

        [HttpPost]
        public JsonResult Delete(Guid idProduct)
        {
            TempData["isDelete"] = "true";
            var detailGridTemp = TempData["DetailGrid"] as List<DetailGrid>;

            if(detailGridTemp !=null)
            {
                var product = detailGridTemp.Find(x => x.IdProduct == idProduct);
                detailGridTemp.Remove(product);
                TempData["DetailGrid"] = detailGridTemp;
            }
           
            return Json(true);
        }




    }
}