using AutoMapper;
using Common.Application.RequestParameters;
using Common.Infrastructure.Logging;
using General.Application.DTOs;
using General.Application.Interfaces.Repositories;
using General.Application.Interfaces.Services;
using General.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace General.Application.Services
{
    public sealed class CountryService : ICountryService
    {
        private readonly IGeneralRepositoryManager _repository;
        private readonly ILoggerService _logger;
        private readonly IMapper _mapper;

        public CountryService(IGeneralRepositoryManager repository, ILoggerService logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }
        public (IEnumerable<CountryDto> countries, MetaData metaData) GetAll(CountryParameters countryParameters, bool trackChanges)
        {
            var countriesWithMetaData = _repository.Country.GetAll(countryParameters, trackChanges);
            var countryDtos = _mapper.Map<IEnumerable<CountryDto>>(countriesWithMetaData);
            return (countryDtos,countriesWithMetaData.MetaData);
        }
    }
}
