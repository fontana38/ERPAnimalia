﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERPAnimalia.Models
{
    public class VoucherDetailModel
    {
        public Guid IdDetalleComprobante;
        public Guid IdProducto;
        public decimal Cantidad;
        public decimal Subtotal;
        public decimal PrecioVenta;
        public decimal PrecioCosto;
        public decimal Descuento;
        public List<ProductModels> ProductModel;
        public List<String> ProductName;

        public Guid IdCabeceraComprobante;
       
        public Guid IdCliente;
        public Guid IdTipoComprobante;
        public DateTime Fecha;
        public string Numero;
       
        public List<ClienteModel> clientModel;
    }
}