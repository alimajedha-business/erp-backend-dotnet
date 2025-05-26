using Accounting.Application.DTOs;
using Accounting.Domain.Entities;
using AutoMapper;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace API
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Ledger, LedgerDto>()
            .ForMember(l => l.Description, opt => opt.MapFrom(x => string.Join(' ', x.Name, x.Name2)));
        }
 
    }
}
