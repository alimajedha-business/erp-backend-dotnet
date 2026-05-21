using NGErp.Base.Domain.Constants;
using NGErp.Base.Domain.EntityTypeRegistration;
using NGErp.HCM.Domain.Constants;

namespace NGErp.Shared.Service.MenuRegistrations;

public class SharedMenuProfile : IMenuModuleProfile
{
    public long ModuleId => ModuleIds.Shared;
    public bool DeleteStale => false;

    public IEnumerable<MenuDefinition> GetMenus()
    {
        yield return new MenuDefinition
        {
            NameFa = "ساختار سازمانی",
            NameEn = "Organizational Structure",
            Order = 6,
            Children = new List<MenuDefinition>
            {
                new() { NameFa = "واحد سازمانی", NameEn = "Department", Order = 1, EntityTypeKey = EntityTypes.Department, Link = "/organizational-structure/departments" },
                new() { NameFa = "پست سازمانی", NameEn = "Position", Order = 2, EntityTypeKey = EntityTypes.Position, Link = "/organizational-structure/positions" },
                new() { NameFa = "چارت سازمانی", NameEn = "Organizational Chart", Order = 3, EntityTypeKey = EntityTypes.OrganizationalStructure, Link = "/organizational-structure/chart" },
            }
        };
    }
}
