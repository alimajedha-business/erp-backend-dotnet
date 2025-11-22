using Base.Infrastructure.DataAccess;
using General.Service.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace General.Infrastructure.DataAccess.Repositories
{
    public sealed class GeneralRepositoryManager : IGeneralRepositoryManager
    {
        private readonly MainDbContext _repositoryContext;
        private readonly Lazy<ICountryRepository> _countryRepository;
        private readonly Lazy<ICurrencyRepository> _currencyRepository;
        private readonly Lazy<IProvinceRepository> _provinceRepository;
        private readonly Lazy<IDomainRepository> _domainRepository;
        private readonly Lazy<ICompanyRepository> _companyRepository;
        public GeneralRepositoryManager(MainDbContext repositoryContext)
        {
            _repositoryContext = repositoryContext;

            _countryRepository = new Lazy<ICountryRepository>(() =>
            new CountryRepository(repositoryContext));

            _currencyRepository = new Lazy<ICurrencyRepository>(() =>
            new CurrencyRepository(repositoryContext));

            _provinceRepository = new Lazy<IProvinceRepository>(() =>
            new ProvinceRepository(repositoryContext));

            _domainRepository = new Lazy<IDomainRepository>(() =>
            new DomainRepository(repositoryContext));

            _companyRepository = new Lazy<ICompanyRepository>(() =>
            new CompanyRepository(repositoryContext));

        }

        public ICountryRepository Country => _countryRepository.Value;

        public ICurrencyRepository Currency => _currencyRepository.Value;

        public IProvinceRepository Province => _provinceRepository.Value;

        public IDomainRepository Domain => _domainRepository.Value;

        public ICompanyRepository Company => _companyRepository.Value;

        public void Save() => _repositoryContext.SaveChangesAsync();

    }
}
