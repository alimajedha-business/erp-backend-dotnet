using Warehouse.Application.DTOs;
using Warehouse.Domain.Exceptions;
using Warehouse.Domain.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using Common.Domain.Exceptions;
using Warehouse.Application.Interfaces.Services;
using Warehouse.Application.Interfaces.Repositories;
using Common.Infrastructure.Logging;


namespace Warehouse.Application.Services
{
    internal sealed class ProductCodeService : Warehouse.Interfaces.Services.IProductCodeService
    {
        private readonly IWarehouseRepositoryManager _repository;
        private readonly ILoggerService _logger;
        private readonly IMapper _mapper;

    

        public ProductCodeService(IWarehouseRepositoryManager repository, ILoggerService logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }


        public ProductCodeDto Create(ProductCodeForCreationDto ProductCode)
        {
            var ProductCodeEntity = _mapper.Map<ProductCode>(ProductCode);
            _repository.ProductCode.Create(ProductCodeEntity);
            _repository.Save();
            var ProductCodeToReturn = _mapper.Map<ProductCodeDto>(ProductCodeEntity);
            return ProductCodeToReturn;
        }

        public (IEnumerable<ProductCodeDto> ProductCodes, string CompanyIds) CreateCollection(IEnumerable<ProductCodeForCreationDto> ProductCodeCollection)
        {
            if (ProductCodeCollection is null)
                throw new ProductCodeCollectionBadRequestException();
            var ProductCodeEntities = _mapper.Map<IEnumerable<ProductCode>>(ProductCodeCollection);
            foreach (var ProductCode in ProductCodeEntities)
            {
                _repository.ProductCode.Create(ProductCode);
            }
            _repository.Save();
            var ProductCodeCollectionToReturn =
            _mapper.Map<IEnumerable<ProductCodeDto>>(ProductCodeEntities);
            var ids = string.Join(",", ProductCodeCollectionToReturn.Select(c => c.Id));
            return (ProductCodes: ProductCodeCollectionToReturn, ids);
        }

        public void Delete(int CompanyId, bool trackChanges)
        {
            var ProductCode = _repository.ProductCode.Get(CompanyId, trackChanges);
            if (ProductCode is null)
                throw new ProductCodeNotFoundException(CompanyId);
            _repository.ProductCode.Delete(ProductCode);
            _repository.Save();
        }

        public IEnumerable<ProductCodeDto> GetAll(bool trackChanges)
        {
            var ProductCodes = _repository.ProductCode.GetAll(trackChanges);
            var ProductCodesDto = _mapper.Map<IEnumerable<ProductCodeDto>>(ProductCodes);
            return ProductCodesDto;
        }

        public IEnumerable<ProductCodeDto> GetByIds(IEnumerable<int> CompanyIds, bool trackChanges)
        {
            if (CompanyIds is null)
                throw new IdParametersBadRequestException();
            var ProductCodeEntities = _repository.ProductCode.GetByIds(CompanyIds, trackChanges);
            if (CompanyIds.Count() != ProductCodeEntities.Count())
                throw new CollectionByIdsBadRequestException();
            var ProductCodesToReturn = _mapper.Map<IEnumerable<ProductCodeDto>>(ProductCodeEntities);
            return ProductCodesToReturn;
        }

        public ProductCodeDto? Get(int CompanyId, bool trackChanges)
        {
            var ProductCode = _repository.ProductCode.Get(CompanyId, trackChanges);
            if (ProductCode is null)
                throw new ProductCodeNotFoundException(CompanyId);
            var ProductCodeDto = _mapper.Map<ProductCodeDto>(ProductCode);
            return ProductCodeDto;
        }

        public void Update(int CompanyId, ProductCodeForUpdateDto ProductCode, bool trackChanges)
        {
            var ProductCodeEntity = _repository.ProductCode.Get(CompanyId, trackChanges);
            if (ProductCodeEntity is null)
                throw new ProductCodeNotFoundException(CompanyId);
            _mapper.Map(ProductCode, ProductCodeEntity);
            _repository.Save();
        }

        public (ProductCodeForUpdateDto ProductCodeForUpdate, ProductCode ProductCodeEntity) GetProductCodeForPatch(int CompanyId, bool trackChanges)
        {
            var ProductCodeEntity = _repository.ProductCode.Get(CompanyId, trackChanges);
            if (ProductCodeEntity is null)
                throw new ProductCodeNotFoundException(CompanyId);
            var ProductCodeToPatch = _mapper.Map<ProductCodeForUpdateDto>(ProductCodeEntity);
            return (ProductCodeToPatch, ProductCodeEntity);
        }

        public void SaveChangesForPatch(ProductCodeForUpdateDto ProductCodeToPatch, ProductCode ProductCodeEntity)
        {
            _mapper.Map(ProductCodeToPatch, ProductCodeEntity);
            _repository.Save();
        }
    }
}
