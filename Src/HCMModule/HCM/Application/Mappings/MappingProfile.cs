using AutoMapper;
using HCM.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace HCM.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //CreateMap<Country, CountryDto>()
            //    .ForMember("Currency", opt => opt.MapFrom(src => src.Currency != null ? src.Currency.Name : null));
        }
    }
}
