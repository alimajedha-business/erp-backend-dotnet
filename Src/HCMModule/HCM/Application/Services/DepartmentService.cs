// Ignore Spelling: HCM Dto

using AutoMapper;
using Common.Infrastructure.Logging;
using HCM.Application.DTOs;
using HCM.Application.Interfaces.Repositories;
using HCM.Application.Interfaces.Services;
using HCM.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCM.Application.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IHCMRepositoryManager _repository;
        private readonly ILoggerService _logger;
        private readonly IMapper _mapper;

        public DepartmentService(IHCMRepositoryManager repository, ILoggerService logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<DepartmentDto> CreateDepartmentForCompanyAsync(int companyId, DepartmentForCreationDto departmentDto, bool trackChanges)
        {
            var department = _mapper.Map<Department>(departmentDto);
            _repository.Department.CreateDepartment(companyId, department);
            await _repository.SaveAsync();
            var departmentToReturn = _mapper.Map<DepartmentDto>(department);
            return departmentToReturn;
        }

        public async Task DeleteDepartmentForCompanyAsync(int companyId, int departmentId, bool trackChanges)
        {
            var department = await _repository.Department.GetDepartmentAsync(companyId, departmentId, trackChanges);
            if (department != null)
                throw new Exception();
            _repository.Department.DeleteDepartment(department!);
            await _repository.SaveAsync();
        }

        public Task<IEnumerable<DepartmentDto>> GetAllDepartmentAsync(int companyId, bool trackChanges)
        {
            //var departments = await _repository.Department.GetAllDepartmentsAsync(companyId, trackChanges);
            //if (departments is null)
            //    throw new LedgerNotFoundException(companyId);
            //var accountSets = await _repository.AccountSet.GetAllAsync(ledgerId, trackChanges);
            //var accountSetDtos = _mapper.Map<IEnumerable<AccountSetDto>>(accountSets);
            //return accountSetDtos;
            throw new NotImplementedException();
        }

        public Task<DepartmentDto?> GetDepartmentAsync(int companyId, int departmentId, bool trackChanges)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(int companyId, int departmentId, DepartmentForUpdateDto department, bool comTrackChanges, bool depTrackChanges)
        {
            throw new NotImplementedException();
        }
    }
}
