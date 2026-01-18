using AutoMapper;
using NGErp.General.Domain.Entities;
using NGErp.HCM.Domain.Entities;
using NGErp.HCM.Service.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NGErp.HCM.Service.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Department, DepartmentDto>();
        }
    }
}
