using NGErp.Base.Domain.Constants;
using NGErp.Base.Domain.EntityTypeRegistration;
using NGErp.HCM.Domain.Constants;

namespace NGErp.HCM.Service.MenuRegistrations;

public class HCMMenuProfile : IMenuModuleProfile
{
    public long ModuleId => ModuleIds.HCM;
    public bool DeleteStale => false;

    public IEnumerable<MenuDefinition> GetMenus()
    {
        yield return new MenuDefinition
        {
            NameFa = "اطلاعات پایه",
            NameEn = "Basic Information",
            Order = 1,
            Children = new List<MenuDefinition>
            {
                new() { NameFa = "گروه استخدامی", NameEn = "Employment Group", Order = 1, EntityTypeKey = EntityTypes.EmploymentGroup, Link = "/hcm/basic-information/employment-groups" },
                new() { NameFa = "شغل", NameEn = "Job", Order = 2, EntityTypeKey = EntityTypes.Job, Link = "/hcm/basic-information/jobs" },
            }
        };

        yield return new MenuDefinition
        {
            NameFa = "اطلاعات عمومی",
            NameEn = "General Information",
            Order = 2,
            Children = new List<MenuDefinition>
            {
                new() { NameFa = "کارمندان", NameEn = "Employees", Order = 1, EntityTypeKey = EntityTypes.Employee, Link = "/hcm/general-information/employees" },
            }
        };

        yield return new MenuDefinition
        {
            NameFa = "عملیات حقوقی",
            NameEn = "Payroll Operations",
            Order = 3,
            Children = new List<MenuDefinition>()
        };
    }
}
