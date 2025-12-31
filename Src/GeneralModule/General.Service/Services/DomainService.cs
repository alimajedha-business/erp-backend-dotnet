using AutoMapper;
using NGErp.Base.Infrastructure.Logging;
using NGErp.General.Service.DTOs;
using NGErp.General.Service.Interfaces.Repositories;
using NGErp.General.Service.Interfaces.Services;
using NGErp.General.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NGErp.General.Service.Services
{
    public class DomainService : IDomainService
    {
        private readonly IDomainRepository _domainRepository;
        private readonly ILoggerService _logger;
        private readonly IMapper _mapper;
        public DomainService(IDomainRepository repository, ILoggerService logger, IMapper mapper)
        {
            _domainRepository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<DomainDto?> GetDomainAsync(Guid domainId)
        {
            var domain = await GetDomainAndCheckIfItExistsAsync(domainId);
            var domainDto = _mapper.Map<DomainDto>(domain);
            return domainDto;
        }

        private async Task<Domain.Entities.Domain> GetDomainAndCheckIfItExistsAsync(Guid domainId)
        {
            var domain = await _domainRepository.GetByIdAsync(domainId);
            if (domain is null)
                throw new DomainNotFoundException(domainId);
            return domain;
        }
    }
}
