using AutoMapper;
using Common.Infrastructure.Logging;
using General.Application.DTOs;
using General.Application.Interfaces.Repositories;
using General.Application.Interfaces.Services;
using General.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace General.Application.Services
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
            var domain = await _repository.Domain.GetDomainAsync(domainId, trackChanges);
            if (domain == null)
                throw new DomainNotFoundException(domainId);

            var domainDto = _mapper.Map<DomainDto>(domain);
            return domainDto;
        }
    }
}
