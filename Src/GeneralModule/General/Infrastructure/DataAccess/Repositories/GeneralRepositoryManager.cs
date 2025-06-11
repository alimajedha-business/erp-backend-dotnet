using General.Application.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace General.Infrastructure.DataAccess.Repositories
{
    public sealed class GeneralRepositoryManager : IGeneralRepositoryManager
    {
        private readonly GeneralDbContext _repositoryContext;
        private readonly Lazy<ICountryRepository> _countryRepository;
        private readonly Lazy<ICurrencyRepository> _currencyRepository;
        private readonly Lazy<IProvinceRepository> _provinceRepository;
        public GeneralRepositoryManager(GeneralDbContext repositoryContext)
        {
            _repositoryContext = repositoryContext;
            _countryRepository = new Lazy<ICountryRepository>(() =>
            new CountryRepository(repositoryContext));
            _currencyRepository = new Lazy<ICurrencyRepository>(() =>
            new CurrencyRepository(repositoryContext));
            _provinceRepository = new Lazy<IProvinceRepository>(() =>
            new ProvinceRepository(repositoryContext));

        }
        public ICountryRepository Country => _countryRepository.Value;
        public ICurrencyRepository Currency => _currencyRepository.Value;
        public IProvinceRepository Province => _provinceRepository.Value;
        public void Save() => _repositoryContext.SaveChanges();

    }
}
}
