using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ERPAnimalia.EntityFramework;
using ERPAnimalia.Models;

namespace ERPAnimalia.Mapper
{
    public static class AutoMapperConfig
    {

        public static MapperConfiguration MapperConfiguration;

        public static void RegisterMappings()
        {
            MapperConfiguration = new MapperConfiguration(cfg => {
                cfg.CreateMap<Category, CategoryModel>().ForMember(t => t.SubCategory, opt => opt.Ignore());
                cfg.CreateMap<SubCategory, SubCategoryModel>();
                cfg.CreateMap<Product, ProductModels>().ForMember(t => t.Category, opt => opt.Ignore())
                .ForMember(t => t.SubCategory, opt => opt.Ignore())
                .ForMember(t => t.IsSelect, opt => opt.Ignore())
                .ForMember(t => t.ListaPrecio, opt => opt.Ignore());
                cfg.CreateMap<ListaPrecio, ListaPrecioModel>();
                cfg.CreateMap<ListaPrecioModel, ListaPrecio>();
                cfg.CreateMap<Cliente, ClienteModel>()
                .ForMember(t => t.FechaCompra1, opt => opt.Ignore())
                .ForMember(t => t.FechaCompra2, opt => opt.Ignore())
                .ForMember(t => t.CantidadDias, opt => opt.Ignore())
                .ForMember(t => t.IdsProduct, opt => opt.Ignore());
                cfg.CreateMap<ClienteModel, Cliente>();
                



            });

           
        }

       

    }
}