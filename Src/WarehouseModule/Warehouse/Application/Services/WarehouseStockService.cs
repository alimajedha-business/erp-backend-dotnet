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
    internal sealed class WarehouseStockService : Warehouse.Interfaces.Services.IWarehouseStockService
    {
        private readonly IWarehouseRepositoryManager _repository;
        private readonly ILoggerService _logger;
        private readonly IMapper _mapper;

    

        public WarehouseStockService(IWarehouseRepositoryManager repository, ILoggerService logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }


        public WarehouseStockDto Create(WarehouseStockForCreationDto WarehouseStock)
        {
            var WarehouseStockEntity = _mapper.Map<WarehouseStock>(WarehouseStock);
            _repository.WarehouseStock.Create(WarehouseStockEntity);
            _repository.Save();
            var WarehouseStockToReturn = _mapper.Map<WarehouseStockDto>(WarehouseStockEntity);
            return WarehouseStockToReturn;
        }

        public (IEnumerable<WarehouseStockDto> WarehouseStocks, string ids) CreateCollection(IEnumerable<WarehouseStockForCreationDto> WarehouseStockCollection)
        {
            if (WarehouseStockCollection is null)
                throw new WarehouseStockCollectionBadRequestException();
            var WarehouseStockEntities = _mapper.Map<IEnumerable<WarehouseStock>>(WarehouseStockCollection);
            foreach (var WarehouseStock in WarehouseStockEntities)
            {
                _repository.WarehouseStock.Create(WarehouseStock);
            }
            _repository.Save();
            var WarehouseStockCollectionToReturn =
            _mapper.Map<IEnumerable<WarehouseStockDto>>(WarehouseStockEntities);
            var ids = string.Join(",", WarehouseStockCollectionToReturn.Select(c => c.Id));
            return (WarehouseStocks: WarehouseStockCollectionToReturn, ids);
        }

        public void Delete(int Id, bool trackChanges)
        {
            var WarehouseStock = _repository.WarehouseStock.Get(Id, trackChanges);
            if (WarehouseStock is null)
                throw new WarehouseStockNotFoundException(Id);
            _repository.WarehouseStock.Delete(WarehouseStock);
            _repository.Save();
        }

        public IEnumerable<WarehouseStockDto> GetAll(bool trackChanges)
        {
            var WarehouseStocks = _repository.WarehouseStock.GetAll(trackChanges);
            var WarehouseStocksDto = _mapper.Map<IEnumerable<WarehouseStockDto>>(WarehouseStocks);
            return WarehouseStocksDto;
        }

        public IEnumerable<WarehouseStockDto> GetByIds(IEnumerable<int> ids, bool trackChanges)
        {
            if (ids is null)
                throw new IdParametersBadRequestException();
            var WarehouseStockEntities = _repository.WarehouseStock.GetByIds(ids, trackChanges);
            if (ids.Count() != WarehouseStockEntities.Count())
                throw new CollectionByIdsBadRequestException();
            var WarehouseStocksToReturn = _mapper.Map<IEnumerable<WarehouseStockDto>>(WarehouseStockEntities);
            return WarehouseStocksToReturn;
        }

        public WarehouseStockDto? Get(int Id, bool trackChanges)
        {
            var WarehouseStock = _repository.WarehouseStock.Get(Id, trackChanges);
            if (WarehouseStock is null)
                throw new WarehouseStockNotFoundException(Id);
            var WarehouseStockDto = _mapper.Map<WarehouseStockDto>(WarehouseStock);
            return WarehouseStockDto;
        }

        public void Update(int Id, WarehouseStockForUpdateDto WarehouseStock, bool trackChanges)
        {
            var WarehouseStockEntity = _repository.WarehouseStock.Get(Id, trackChanges);
            if (WarehouseStockEntity is null)
                throw new WarehouseStockNotFoundException(Id);
            _mapper.Map(WarehouseStock, WarehouseStockEntity);
            _repository.Save();
        }

        public (WarehouseStockForUpdateDto WarehouseStockForUpdate, WarehouseStock WarehouseStockEntity) GetWarehouseStockForPatch(int Id, bool trackChanges)
        {
            var WarehouseStockEntity = _repository.WarehouseStock.Get(Id, trackChanges);
            if (WarehouseStockEntity is null)
                throw new WarehouseStockNotFoundException(Id);
            var WarehouseStockToPatch = _mapper.Map<WarehouseStockForUpdateDto>(WarehouseStockEntity);
            return (WarehouseStockToPatch, WarehouseStockEntity);
        }

        public void SaveChangesForPatch(WarehouseStockForUpdateDto WarehouseStockToPatch, WarehouseStock WarehouseStockEntity)
        {
            _mapper.Map(WarehouseStockToPatch, WarehouseStockEntity);
            _repository.Save();
        }
    }
}
