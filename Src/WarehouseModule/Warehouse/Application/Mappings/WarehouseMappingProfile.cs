using Warehouse.Application.DTOs;
using Warehouse.Domain.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warehouse.Application.Mappings
{
    public class WarehouseMappingProfile : Profile
    {
        public WarehouseMappingProfile()
        {
            CreateMap<WarehouseType, WarehouseTypeDto>()
                .ForCtorParam("Id", opt => opt.MapFrom(src => src.Id))
                .ForCtorParam("TypeCode", opt => opt.MapFrom(src => src.TypeCode))
                .ForCtorParam("TypeName", opt => opt.MapFrom(src => src.TypeName));

            CreateMap<WarehouseTypeForCreationDto, WarehouseType>()
                .ForMember(dest => dest.TypeCode, opt => opt.MapFrom(src => src.TypeCode))
                .ForMember(dest => dest.TypeName, opt => opt.MapFrom(src => src.TypeName))
                .ForMember(dest=>dest.WarehouseStocks,opt=>opt.Ignore());

            CreateMap<WarehouseTypeForCreationDto, WarehouseType>()
                .ForMember(dest => dest.TypeCode, opt => opt.MapFrom(src => src.TypeCode))
                .ForMember(dest => dest.TypeName, opt => opt.MapFrom(src => src.TypeName));

///////////////////////////////////////////////////////////

            CreateMap<WarehouseStock, WarehouseStockDto>();
            CreateMap<WarehouseStockForCreationDto, WarehouseStock>();
            CreateMap<WarehouseStockForCreationDto, WarehouseStock>();

        }
    }
}
