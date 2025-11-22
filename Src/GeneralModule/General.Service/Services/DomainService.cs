using AutoMapper;
using Base.Infrastructure.Logging;
using General.Service.DTOs;
using General.Service.Interfaces.Repositories;
using General.Service.Interfaces.Services;
using General.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace General.Service.Services
{
    public class DomainService : IDomainService
    {
        private readonly IGeneralRepositoryManager _repository;
        private readonly ILoggerService _logger;
        private readonly IMapper _mapper;
        public DomainService(IGeneralRepositoryManager repository, ILoggerService logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<DomainDto?> GetDomainAsync(int domainId, bool trackChanges)
        {
            var domain = await GetDomainAndCheckIfItExistsAsync(domainId, trackChanges);
            var domainDto = _mapper.Map<DomainDto>(domain);
            return domainDto;
        }

        private async Task<Domain.Entities.Domain> GetDomainAndCheckIfItExistsAsync(int domainId, bool trackChanges)
        {
            var domain = await _repository.Domain.GetDomainAsync(domainId, trackChanges);
            if (domain is null)
                throw new DomainNotFoundException(domainId);
            return domain;
        }
    }
}
