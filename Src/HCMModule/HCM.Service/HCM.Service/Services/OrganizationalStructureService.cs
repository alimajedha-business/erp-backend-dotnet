using System.Diagnostics.CodeAnalysis;

using AutoMapper;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

using NGErp.HCM.Domain.Entities;
using NGErp.HCM.Service.DTOs;
using NGErp.HCM.Service.Repository.Contracts;
using NGErp.HCM.Service.Resources;

using static System.Runtime.InteropServices.JavaScript.JSType;

namespace NGErp.HCM.Service.Services;

public class OrganizationalStructureService(
    IOrganizationalStructureRepository organizationalStructureServiceRepository,
    IMapper mapper,
    IStringLocalizer<HCMResource> localizer
    ) : IOrganizationalStructureService
{
    private readonly IOrganizationalStructureRepository _organizationalStructureRepository = organizationalStructureServiceRepository;
    private readonly IMapper _mapper = mapper;
    private readonly IStringLocalizer<HCMResource> _localizer = localizer;

    //public Task<Guid> SaveStructureVersionAsync(SaveOrganizationalStructureDto dto)
    //{
    //    throw new NotImplementedException();
    //}

    public Task<OrganizationalStructureTreeDto> GetCurrentTreeAsync(Guid companyId)
    {
        //var structure = await _organizationalStructureRepository
        //    .Find(companyId)
        //    .Include(s => s.Items!)
        //        .ThenInclude(i => i.Node)
        //            .ThenInclude(n => n.Department)
        //    .Include(s => s.Items!)
        //        .ThenInclude(i => i.Node)
        //            .ThenInclude(n => n.Position)
        //    .OrderByDescending(s => s.EffectiveFrom)
        //    .FirstOrDefaultAsync();

        //if (structure == null)
        //{
        //    return new OrganizationalStructureTreeDto
        //    {
        //        Id = Guid.Empty,
        //        EffectiveFrom = DateOnly.FromDateTime(DateTime.Now),
        //        Items = []
        //    };
        //}

        //var items = structure.Items!.Select(MapToTreeNodeDto).ToList();

        //// Build tree hierarchy
        //var rootItems = items.Where(x => x.ParentItemId == null).ToList();
        //var childItems = items.Where(x => x.ParentItemId.HasValue).ToList();

        //// Add children to parents
        //foreach (var item in rootItems.Concat(childItems))
        //{
        //    item.Children = childItems.Where(c => c.ParentItemId == item.Id).ToList();
        //}

        //return new OrganizationalStructureTreeDto
        //{
        //    Id = structure.Id,
        //    EffectiveFrom = structure.EffectiveFrom,
        //    Description = structure.Description,
        //    Items = rootItems
        //};
        throw new NotImplementedException();
    }

    public async Task<OrganizationalStructureTreeDto> GetTreeAtDateAsync(Guid companyId, DateOnly date)
    {
        var structure = await _organizationalStructureRepository
            .Find(companyId, s => s.EffectiveFrom == date)
            .Include(s => s.Items!)
                .ThenInclude(i => i.Node)
                    .ThenInclude(n => n.Department)
            .Include(s => s.Items!)
                .ThenInclude(i => i.Node)
                    .ThenInclude(n => n.Position)
            .FirstOrDefaultAsync();

        if (structure == null)
        {
            return new OrganizationalStructureTreeDto
            {
                Id = Guid.Empty,
                EffectiveFrom = date,
                Items = []
            };
        }

        var items = structure.Items!.Select(MapToTreeNodeDto).ToList();

        // Build tree hierarchy
        var rootItems = items.Where(x => x.ParentItemId == null).ToList();
        var childItems = items.Where(x => x.ParentItemId.HasValue).ToList();

        // Add children to parents
        foreach (var item in rootItems.Concat(childItems))
        {
            item.Children = childItems.Where(c => c.ParentItemId == item.Id).ToList();
        }

        return new OrganizationalStructureTreeDto
        {
            Id = structure.Id,
            EffectiveFrom = structure.EffectiveFrom,
            Description = structure.Description,
            Items = rootItems
        };
    }

    private OrganizationalStructureTreeNodeDto MapToTreeNodeDto(OrganizationalStructureItem item)
    {
        return new OrganizationalStructureTreeNodeDto
        {
            Id = item.Id,
            //NodeId = item.NodeId,
            ParentItemId = item.ParentItemId,
            Node = new OrganizationNodeDto
            {
                Id = item.Node.Id,
                NodeType = item.Node.NodeType,
                //DepartmentId = item.Node.DepartmentId,
                //PositionId = item.Node.PositionId,
                Department = item.Node.Department != null ? new DepartmentDto
                {
                    Id = item.Node.Department.Id,
                    Name = item.Node.Department.Name,
                    Code = item.Node.Department.Code,
                } : null,
                Position = item.Node.Position != null ? new PositionDto
                {
                    Id = item.Node.Position.Id,
                    Name = item.Node.Position.Name,
                    Code = item.Node.Position.Code,
                } : null
            },
            Children = []
        };
    }
}