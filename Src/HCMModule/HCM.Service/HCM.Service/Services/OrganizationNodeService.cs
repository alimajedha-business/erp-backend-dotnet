using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using Microsoft.Extensions.Localization;
using NGErp.Base.Service.Services;
using NGErp.General.Service.Services;
using NGErp.HCM.Domain.Entities;
using NGErp.HCM.Service.DTOs;
using NGErp.HCM.Service.Repository.Contracts;
using NGErp.HCM.Service.RequestFeatures;
using NGErp.HCM.Service.Resources;

namespace NGErp.HCM.Service.Services
{
    public class OrganizationNodeService(
    IOrganizationNodeRepository organizationNodeRepository,
    IMapper mapper,
    IStringLocalizer<HCMResource> localizer,
    IAdvancedFilterBuilder filterBuilder,

    ICompanyService companyService,
    IDepartmentService departmentService,
    IPositionService positiontService

        ) :
        IOrganizationNodeService
    {
        //private readonly string _key = "OrganizationNode";

        private readonly IMapper _mapper = mapper;
        private readonly IStringLocalizer _localizer = localizer;
        private readonly IAdvancedFilterBuilder _filterBuilder = filterBuilder;
        private readonly IOrganizationNodeRepository _organizationNodeRepository = organizationNodeRepository;
        private readonly ICompanyService _companyService = companyService;

        private readonly IDepartmentService _departmentService = departmentService;
        private readonly IPositionService _positionService = positiontService;

        public async Task<OrganizationNodeTreeDto?> GetByDepartmentIdAsync(
            Guid companyId,
            Guid departmentId,
            CancellationToken ct)
        {
            var x = await _organizationNodeRepository.GetByDepartmentIdAsync(companyId, departmentId, ct);
            return _mapper.Map<OrganizationNodeTreeDto>(x);
        }
        
        public async Task<OrganizationNode?> GetByPositionIdAsync(
            Guid companyId,
            Guid positionId,
            CancellationToken ct)
        {
            return await _organizationNodeRepository.GetByPositionIdAsync(companyId, positionId, ct);
        }

        public async Task<OrganizationNodeTreeDto> GetOrCreateAsync(
            Guid companyId,
            CreateOrganizationNodeDto item,
            CancellationToken ct = default
            )
        {
            Guid? departmentId = item.DepartmentId;
            Guid? positionId = item.PositionId;

            if (departmentId is not null && positionId is not null)
            {
                throw new Exception("It is not possible to send DepartmentId and PositionId simultaneously.");
            }

            if (departmentId is null && positionId is null)
            {
                throw new Exception("Either DepartmentId or PositionId must be provided.");
            }

            if (departmentId is not null)
            {
                try
                {
                    var existingDepartmentNode =
                        await GetByDepartmentIdAsync(
                            companyId,
                            departmentId.Value,
                            ct);


                    if (existingDepartmentNode is not null)
                    {
                        var existedDepartmentNode = _mapper.Map<OrganizationNodeTreeDto>(existingDepartmentNode);
                        return existedDepartmentNode;
                    }
                }
                catch (Exception ex)
                {
                    var message = ex.Message;
                    var innerMessage = ex.InnerException?.Message;
                    throw;
                }


                var department = await _departmentService.GetByIdAsync(companyId, departmentId.Value);

                var newDepartmentNode = new OrganizationNode
                {
                    Id = Guid.NewGuid(),
                    CompanyId = companyId,
                    DepartmentId = department.Id,
                    NodeType = NodeType.Department
                };
                var departmentNode = _mapper.Map<OrganizationNodeTreeDto>(newDepartmentNode);
                await _organizationNodeRepository.AddAsync(newDepartmentNode, ct);

                return departmentNode;
            }

            var existingPositionNode =
                await _organizationNodeRepository.GetByPositionIdAsync(companyId, positionId!.Value, ct);

            if (existingPositionNode is not null)
            {
                var existedPositionNode = _mapper.Map<OrganizationNodeTreeDto>(existingPositionNode);

                return existedPositionNode;
            }

            var position = await _positionService.GetByIdAsync(companyId, positionId.Value);

            var newPositionNode = new OrganizationNode
            {
                Id = Guid.NewGuid(),
                CompanyId = companyId,
                PositionId = position.Id,
                NodeType = NodeType.Position
            };
            var PositionNode = _mapper.Map<OrganizationNodeTreeDto>(newPositionNode);

            await _organizationNodeRepository.AddAsync(newPositionNode, ct);

            return PositionNode;
        }

    }
}
