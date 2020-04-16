using AdvertAPI.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAdvert.Web.Models.AdvertManagement;

namespace WebAdvert.Web.ServiceClients
{
    public class AdvertProfile : Profile
    {
        public AdvertProfile()
        {
            CreateMap<CreateAdvertViewModel, CreateAdvertModel>().ReverseMap();
            CreateMap<AdvertModel, CreateAdvertModel>().ReverseMap();
            CreateMap<CreateAdvertResponse, AdvertResponse>().ReverseMap();

        }
    }
}
