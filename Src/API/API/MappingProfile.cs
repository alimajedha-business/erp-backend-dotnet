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
                .ForCtorParam("Description", opt => opt.MapFrom(x => string.Join(' ', x.Name, x.Name2)));
            CreateMap<AccountSet, AccountSetDto>();
            CreateMap<LedgerForCreationDto, Ledger>();
            CreateMap<AccountSetForCreationDto, AccountSet>();
            CreateMap<AccountSetForUpdateDto, AccountSet>();
            CreateMap<LedgerForUpdateDto, Ledger>();
        }

    }
}
