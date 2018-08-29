using AutoMapper;
using ProjectApi.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectApi.Mappings
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public DomainToViewModelMappingProfile()
        {
            CreateMap<Item, ViewModels.Item>().ReverseMap();
            CreateMap<Item, ViewModels.Tag>().ReverseMap();
            CreateMap<Item, Events.Commands.SetItemAsync.Command>().ReverseMap();
            CreateMap<Item, Events.Commands.UpdateItemAsync.Command>().ReverseMap();
        }
    }
}
