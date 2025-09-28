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
    internal sealed class WarehouseTypeService : Warehouse.Interfaces.Services.IWarehouseTypeService
    {
        private readonly IWarehouseRepositoryManager _repository;
        private readonly ILoggerService _logger;
        private readonly IMapper _mapper;

    

        public WarehouseTypeService(IWarehouseRepositoryManager repository, ILoggerService logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }


        public WarehouseTypeDto Create(WarehouseTypeForCreationDto WarehouseType)
        {
            var WarehouseTypeEntity = _mapper.Map<WarehouseType>(WarehouseType);
            _repository.WarehouseType.Create(WarehouseTypeEntity);
            _repository.Save();
            var WarehouseTypeToReturn = _mapper.Map<WarehouseTypeDto>(WarehouseTypeEntity);
            return WarehouseTypeToReturn;
        }

        public (IEnumerable<WarehouseTypeDto> WarehouseTypes, string ids) CreateCollection(IEnumerable<WarehouseTypeForCreationDto> WarehouseTypeCollection)
        {
            if (WarehouseTypeCollection is null)
                throw new WarehouseTypeCollectionBadRequestException();
            var WarehouseTypeEntities = _mapper.Map<IEnumerable<WarehouseType>>(WarehouseTypeCollection);
            foreach (var WarehouseType in WarehouseTypeEntities)
            {
                _repository.WarehouseType.Create(WarehouseType);
            }
            _repository.Save();
            var WarehouseTypeCollectionToReturn =
            _mapper.Map<IEnumerable<WarehouseTypeDto>>(WarehouseTypeEntities);
            var ids = string.Join(",", WarehouseTypeCollectionToReturn.Select(c => c.Id));
            return (WarehouseTypes: WarehouseTypeCollectionToReturn, ids);
        }

        public void Delete(int Id, bool trackChanges)
        {
            var WarehouseType = _repository.WarehouseType.Get(Id, trackChanges);
            if (WarehouseType is null)
                throw new WarehouseTypeNotFoundException(Id);
            _repository.WarehouseType.Delete(WarehouseType);
            _repository.Save();
        }

        public IEnumerable<WarehouseTypeDto> GetAll(bool trackChanges)
        {
            var WarehouseTypes = _repository.WarehouseType.GetAll(trackChanges);
            var WarehouseTypesDto = _mapper.Map<IEnumerable<WarehouseTypeDto>>(WarehouseTypes);
            return WarehouseTypesDto;
        }

        public IEnumerable<WarehouseTypeDto> GetByIds(IEnumerable<int> ids, bool trackChanges)
        {
            if (ids is null)
                throw new IdParametersBadRequestException();
            var WarehouseTypeEntities = _repository.WarehouseType.GetByIds(ids, trackChanges);
            if (ids.Count() != WarehouseTypeEntities.Count())
                throw new CollectionByIdsBadRequestException();
            var WarehouseTypesToReturn = _mapper.Map<IEnumerable<WarehouseTypeDto>>(WarehouseTypeEntities);
            return WarehouseTypesToReturn;
        }

        public WarehouseTypeDto? Get(int Id, bool trackChanges)
        {
            var WarehouseType = _repository.WarehouseType.Get(Id, trackChanges);
            if (WarehouseType is null)
                throw new WarehouseTypeNotFoundException(Id);
            var WarehouseTypeDto = _mapper.Map<WarehouseTypeDto>(WarehouseType);
            return WarehouseTypeDto;
        }

        public void Update(int Id, WarehouseTypeForUpdateDto WarehouseType, bool trackChanges)
        {
            var WarehouseTypeEntity = _repository.WarehouseType.Get(Id, trackChanges);
            if (WarehouseTypeEntity is null)
                throw new WarehouseTypeNotFoundException(Id);
            _mapper.Map(WarehouseType, WarehouseTypeEntity);
            _repository.Save();
        }

        public (WarehouseTypeForUpdateDto WarehouseTypeForUpdate, WarehouseType WarehouseTypeEntity) GetWarehouseTypeForPatch(int Id, bool trackChanges)
        {
            var WarehouseTypeEntity = _repository.WarehouseType.Get(Id, trackChanges);
            if (WarehouseTypeEntity is null)
                throw new WarehouseTypeNotFoundException(Id);
            var WarehouseTypeToPatch = _mapper.Map<WarehouseTypeForUpdateDto>(WarehouseTypeEntity);
            return (WarehouseTypeToPatch, WarehouseTypeEntity);
        }

        public void SaveChangesForPatch(WarehouseTypeForUpdateDto WarehouseTypeToPatch, WarehouseType WarehouseTypeEntity)
        {
            _mapper.Map(WarehouseTypeToPatch, WarehouseTypeEntity);
            _repository.Save();
        }
    }
}
