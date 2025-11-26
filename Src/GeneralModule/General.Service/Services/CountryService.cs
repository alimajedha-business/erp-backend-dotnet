using AutoMapper;
using NGErp.Base.Service.RequestParameters;
using NGErp.Base.Infrastructure.Logging;
using NGErp.General.Service.DTOs;
using NGErp.General.Service.Interfaces.Repositories;
using NGErp.General.Service.Interfaces.Services;
using NGErp.General.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NGErp.General.Service.Services
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
