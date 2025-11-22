using AutoMapper;
using Base.Service.RequestParameters;
using Base.Infrastructure.Logging;
using General.Service.DTOs;
using General.Service.Interfaces.Repositories;
using General.Service.Interfaces.Services;
using General.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace General.Service.Services
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
