using NGErp.Base.Domain.Constants;
using NGErp.Base.Domain.EntityTypeRegistration;
using NGErp.HCM.Domain.Constants;

namespace NGErp.HCM.Service.EntityTypeRegistrations;

public class HCMModuleProfile : IEntityTypeModuleProfile
{
    public long ModuleId => ModuleIds.HCM;
    public string Prefix => "hcm";
    public bool DeleteStale => false;

    public IEnumerable<EntityTypeDefinition> GetDefinitions()
    {
        yield return new EntityTypeDefinition 
        { 
            Key = EntityTypes.ModuleEntity, 
            NameFa = "موجودیت ماژول", 
            NameEn = "Module Entity", 
            Code = "0001", 
            Attributes = new EntityTypeAttributes
            {
                Readable = false, Creatable = false, Editable = false, Deletable = false, 
                Loggable = false, Printable = false, Importable = false, Exportable = false, 
                IfNotCreator = false, HasRestriction = false, Permissible = true
            },
            Commands = new List<EntityTypeCommandDefinition>
            {
                new() { Key = EntityTypeCommands.ModuleEntityView, NameFa = "مشاهده", NameEn = "View" }
            }
        };

        yield return new EntityTypeDefinition 
        { 
            Key = EntityTypes.EmploymentGroup, 
            NameFa = "گروه استخدامی", 
            NameEn = "Employment Group", 
            Code = "0007", 
            Ordering = 40,
            Attributes = new EntityTypeAttributes
            {
                Readable = true, Creatable = true, Editable = true, Deletable = true, 
                Loggable = true, Printable = true, Importable = false, Exportable = true, 
                IfNotCreator = false, HasRestriction = false, Permissible = true
            }
        };

        yield return new EntityTypeDefinition 
        { 
            Key = EntityTypes.EmploymentGroupSpecification, 
            NameFa = "مشخصات گروه استخدامی", 
            NameEn = "Employment Group Specification", 
            Code = "0008", 
            Ordering = 45,
            Attributes = new EntityTypeAttributes
            {
                Readable = true, Creatable = true, Editable = true, Deletable = true, 
                Loggable = true, Printable = true, Importable = false, Exportable = true, 
                IfNotCreator = false, HasRestriction = false, Permissible = true
            }
        };

        yield return new EntityTypeDefinition 
        { 
            Key = EntityTypes.Employee, 
            NameFa = "کارمند", 
            NameEn = "Employee", 
            Code = "0009", 
            Ordering = 50,
            Attributes = new EntityTypeAttributes
            {
                Readable = true, Creatable = true, Editable = true, Deletable = true, 
                Loggable = true, Printable = true, Importable = false, Exportable = true, 
                IfNotCreator = false, HasRestriction = false, Permissible = true
            }
        };

        yield return new EntityTypeDefinition 
        { 
            Key = EntityTypes.EmployeeEducation, 
            NameFa = "تحصیلات کارمند", 
            NameEn = "Employee Education", 
            Code = "0010", 
            Ordering = 60,
            Attributes = new EntityTypeAttributes
            {
                Readable = true, Creatable = true, Editable = true, Deletable = true, 
                Loggable = true, Printable = true, Importable = false, Exportable = true, 
                IfNotCreator = false, HasRestriction = false, Permissible = true
            }
        };

        yield return new EntityTypeDefinition 
        { 
            Key = EntityTypes.EmployeeWorkExperience, 
            NameFa = "سوابق کاری کارمند", 
            NameEn = "Employee Work Experience", 
            Code = "0011", 
            Ordering = 70,
            Attributes = new EntityTypeAttributes
            {
                Readable = true, Creatable = true, Editable = true, Deletable = true, 
                Loggable = true, Printable = true, Importable = false, Exportable = true, 
                IfNotCreator = false, HasRestriction = false, Permissible = true
            }
        };

        yield return new EntityTypeDefinition 
        { 
            Key = EntityTypes.EmployeeDependant, 
            NameFa = "افراد تحت تکفل کارمند", 
            NameEn = "Employee Dependant", 
            Code = "0012", 
            Ordering = 72,
            Attributes = new EntityTypeAttributes
            {
                Readable = true, Creatable = true, Editable = true, Deletable = true, 
                Loggable = true, Printable = true, Importable = false, Exportable = true, 
                IfNotCreator = false, HasRestriction = false, Permissible = true
            }
        };

        yield return new EntityTypeDefinition 
        { 
            Key = EntityTypes.EmployeeRelative, 
            NameFa = "بستگان کارمند", 
            NameEn = "Employee Relative", 
            Code = "0013", 
            Ordering = 74,
            Attributes = new EntityTypeAttributes
            {
                Readable = true, Creatable = true, Editable = true, Deletable = true, 
                Loggable = true, Printable = true, Importable = false, Exportable = true, 
                IfNotCreator = false, HasRestriction = false, Permissible = true
            }
        };

        yield return new EntityTypeDefinition 
        { 
            Key = EntityTypes.EmployeeWarriorRecord, 
            NameFa = "سوابق ایثارگری کارمند", 
            NameEn = "Employee Warrior Record", 
            Code = "0014", 
            Ordering = 76,
            Attributes = new EntityTypeAttributes
            {
                Readable = true, Creatable = true, Editable = true, Deletable = true, 
                Loggable = true, Printable = true, Importable = false, Exportable = true, 
                IfNotCreator = false, HasRestriction = false, Permissible = true
            }
        };

        yield return new EntityTypeDefinition 
        { 
            Key = EntityTypes.Job, 
            NameFa = "شغل", 
            NameEn = "Job", 
            Code = "0015", 
            Ordering = 80,
            Attributes = new EntityTypeAttributes
            {
                Readable = true, Creatable = true, Editable = true, Deletable = true, 
                Loggable = true, Printable = true, Importable = false, Exportable = true, 
                IfNotCreator = false, HasRestriction = false, Permissible = true
            }
        };

        yield return new EntityTypeDefinition 
        { 
            Key = EntityTypes.PositionJob, 
            NameFa = "شغل پست", 
            NameEn = "Position Job", 
            Code = "0017", 
            Ordering = 90,
            Attributes = new EntityTypeAttributes
            {
                Readable = true, Creatable = true, Editable = true, Deletable = true, 
                Loggable = true, Printable = true, Importable = false, Exportable = true, 
                IfNotCreator = false, HasRestriction = false, Permissible = true
            }
        };
    }
}
