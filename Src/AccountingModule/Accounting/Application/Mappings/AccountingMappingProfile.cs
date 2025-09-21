using Accounting.Application.DTOs;
using Accounting.Domain.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounting.Application.Mappings
{
    public class AccountingMappingProfile : Profile
    {
        public AccountingMappingProfile()
        {
            CreateMap<Ledger, LedgerDto>()
                .ForCtorParam("Description", opt => opt.MapFrom(x => string.Join(' ', x.Name, x.Name2)));
            CreateMap<AccountSet, AccountSetDto>();
            CreateMap<LedgerForCreationDto, Ledger>();
            CreateMap<AccountSetForCreationDto, AccountSet>();
            CreateMap<AccountSetForUpdateDto, AccountSet>();
            CreateMap<LedgerForUpdateDto, Ledger>();
            CreateMap<Ledger, LedgerForUpdateDto>();
        }
    }
}
