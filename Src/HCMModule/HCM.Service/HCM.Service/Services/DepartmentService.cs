using AutoMapper;
using Microsoft.Extensions.Logging;
using NGErp.Base.Infrastructure.Services;
using NGErp.HCM.Infrastructure.DataAccess.Repositories;
using NGErp.HCM.Service.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core.Tokenizer;
using System.Text;
using System.Threading.Tasks;

namespace NGErp.HCM.Service.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentRepository _departmentRepository;
        private readonly ILogger<DepartmentService> _logger;
        private readonly IMapper _mapper;

        public DepartmentService(IDepartmentRepository departmentRepository, ILogger<DepartmentService> logger, IMapper mapper)
        {
            _departmentRepository = departmentRepository;
            _logger = logger;
            _mapper = mapper;
        }

        public Task<DepartmentDto> CreateDepartmentAsync(CreateDepartmentDto dto)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteDepartmentAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<DepartmentDto?> GetDepartmentByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<DepartmentDto>> GetDepartmentsAsync()
        {
            try
            {
                _logger.LogInformation("Fetching departments");
                var result = await _departmentRepository.GetAllAsync();
                var departmentDto = _mapper.Map<List<DepartmentDto>>(result);
                return departmentDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching departments");
                throw;
            }
        }

        public Task<DepartmentDto> UpdateDepartmentAsync(int id, UpdateDepartmentDto dto)
        {
            throw new NotImplementedException();
        }
    }
}
