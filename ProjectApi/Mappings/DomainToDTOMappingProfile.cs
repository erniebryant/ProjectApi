using AutoMapper;
using ProjectApi.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectApi.Mappings
{
    public class DomainToDTOMappingProfile : Profile
    {
        public DomainToDTOMappingProfile()
        {
            CreateMap<Item, Data.Models.Item>().ReverseMap();
            CreateMap<Item, Data.Models.Tag>().ReverseMap();
        }
    }
}
