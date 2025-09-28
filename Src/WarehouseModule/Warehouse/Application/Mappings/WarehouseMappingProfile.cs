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
                .ForCtorParam("Code", opt => opt.MapFrom(src => src.Code))
                .ForCtorParam("Name", opt => opt.MapFrom(src => src.Name));

            //CreateMap<WarehouseTypeForCreationDto, WarehouseType>()
            //    .ForMember(d => d.Code, opt => opt.MapFrom(src => src.Code))
            //    .ForMember(d => d.Name, opt => opt.MapFrom(src => src.Name))
            //    .ForMember(d => d.WarehouseStocks, opt => opt.Ignore());

            CreateMap<WarehouseTypeForCreationDto, WarehouseType>()
                .ForMember(d => d.Code, opt => opt.MapFrom(src => src.Code))
                .ForMember(d => d.Name, opt => opt.MapFrom(src => src.Name));

///////////////////////////////////////////////////////////

            CreateMap<WarehouseStock, WarehouseStockDto>();
            CreateMap<WarehouseStockForCreationDto, WarehouseStock>();

            ///////////////////////////////////////////////////////////  
            CreateMap<ProductHierarchy, ProductHierarchyDto>();
            CreateMap<ProductHierarchyForCreationDto, ProductHierarchy>();

            ///////////////////////////////////////////////////////////  
            CreateMap<ProductCode, ProductCodeDto>();
            CreateMap<ProductCodeForCreationDto, ProductCode>();
        }
    }
}
