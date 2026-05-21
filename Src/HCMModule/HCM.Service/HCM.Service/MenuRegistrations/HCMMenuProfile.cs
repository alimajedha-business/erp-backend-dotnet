using NGErp.Base.Domain.Constants;
using NGErp.Base.Domain.EntityTypeRegistration;
using NGErp.HCM.Domain.Constants;

namespace NGErp.HCM.Service.MenuRegistrations;

public class HCMMenuProfile : IMenuModuleProfile
{
    public long ModuleId => ModuleIds.HCM;
    public bool DeleteStale => true;

    public IEnumerable<MenuDefinition> GetMenus()
    {
        yield return new MenuDefinition
        {
            NameFa = "اطلاعات پایه",
            NameEn = "Base Data",
            Order = 1,
            Children = new List<MenuDefinition>
            {
                new() { NameFa = "گروه استخدامی", NameEn = "Employment Group", Order = 1, EntityTypeKey = EntityTypes.EmploymentGroup, Link = "/base-data/employment-groups" },
                new() { NameFa = "شغل", NameEn = "Job", Order = 2, EntityTypeKey = EntityTypes.Job, Link = "/base-data/jobs" },
            }
        };

        yield return new MenuDefinition
        {
            NameFa = "اطلاعات عمومی",
            NameEn = "General Information",
            Order = 2,
            Children = new List<MenuDefinition>
            {
                new() { NameFa = "کارمندان", NameEn = "Employees", Order = 1, EntityTypeKey = EntityTypes.Employee, Link = "/general-information/employees" },
            }
        };

        yield return new MenuDefinition
        {
            NameFa = "عملیات حقوقی",
            NameEn = "Payroll Operations",
            Order = 3,
            Children = new List<MenuDefinition>(),
            Link = "/individual-operations/employees" 
        };
    }
}
