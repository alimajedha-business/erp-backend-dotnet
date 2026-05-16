using NGErp.Base.Domain.EntityTypeRegistration;
using NGErp.HCM.Domain.Constants;

namespace NGErp.HCM.Service.EntityTypeRegistrations;

public class HCMModuleProfile : IEntityTypeModuleProfile
{
    public long ModuleId => 6;
    public string Prefix => "hcm";
    public bool DeleteStale => true;

    public IEnumerable<EntityTypeDefinition> GetDefinitions()
    {
        yield return new EntityTypeDefinition 
        { 
            Key = EntityTypes.MODULE_ENTITY, 
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
                new() { Key = EntityTypeCommands.MODULE_ENTITY_VIEW, NameFa = "مشاهده", NameEn = "View" }
            }
        };

        yield return new EntityTypeDefinition 
        { 
            Key = EntityTypes.DEPARTMENT, 
            NameFa = "واحد سازمانی", 
            NameEn = "Department", 
            Code = "0002", 
            Ordering = 10,
            Attributes = new EntityTypeAttributes
            {
                Readable = true, Creatable = true, Editable = true, Deletable = true, 
                Loggable = true, Printable = true, Importable = false, Exportable = true, 
                IfNotCreator = false, HasRestriction = false, Permissible = true
            }
        };

        yield return new EntityTypeDefinition 
        { 
            Key = EntityTypes.POSITION, 
            NameFa = "پست سازمانی", 
            NameEn = "Position", 
            Code = "0003", 
            Ordering = 20,
            Attributes = new EntityTypeAttributes
            {
                Readable = true, Creatable = true, Editable = true, Deletable = true, 
                Loggable = true, Printable = true, Importable = false, Exportable = true, 
                IfNotCreator = false, HasRestriction = false, Permissible = true
            }
        };

        yield return new EntityTypeDefinition 
        { 
            Key = EntityTypes.ORGANIZATIONAL_STRUCTURE, 
            NameFa = "ساختار سازمانی", 
            NameEn = "Organizational Structure", 
            Code = "0004", 
            Ordering = 30,
            Attributes = new EntityTypeAttributes
            {
                Readable = true, Creatable = true, Editable = true, Deletable = true, 
                Loggable = true, Printable = true, Importable = false, Exportable = true, 
                IfNotCreator = false, HasRestriction = false, Permissible = true
            }
        };

        yield return new EntityTypeDefinition 
        { 
            Key = EntityTypes.ORGANIZATIONAL_STRUCTURE_ITEM, 
            NameFa = "آیتم ساختار سازمانی", 
            NameEn = "Organizational Structure Item", 
            Code = "0005", 
            Ordering = 35,
            Attributes = new EntityTypeAttributes
            {
                Readable = true, Creatable = true, Editable = true, Deletable = true, 
                Loggable = true, Printable = true, Importable = false, Exportable = true, 
                IfNotCreator = false, HasRestriction = false, Permissible = true
            }
        };

        yield return new EntityTypeDefinition 
        { 
            Key = EntityTypes.ORGANIZATION_NODE, 
            NameFa = "گره سازمانی", 
            NameEn = "Organization Node", 
            Code = "0006", 
            Ordering = 37,
            Attributes = new EntityTypeAttributes
            {
                Readable = true, Creatable = true, Editable = true, Deletable = true, 
                Loggable = true, Printable = true, Importable = false, Exportable = true, 
                IfNotCreator = false, HasRestriction = false, Permissible = true
            }
        };

        yield return new EntityTypeDefinition 
        { 
            Key = EntityTypes.EMPLOYMENT_GROUP, 
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
            Key = EntityTypes.EMPLOYMENT_GROUP_SPECIFICATION, 
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
            Key = EntityTypes.EMPLOYEE, 
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
            Key = EntityTypes.EMPLOYEE_EDUCATION, 
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
            Key = EntityTypes.EMPLOYEE_WORK_EXPERIENCE, 
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
            Key = EntityTypes.EMPLOYEE_DEPENDANT, 
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
            Key = EntityTypes.EMPLOYEE_RELATIVE, 
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
            Key = EntityTypes.EMPLOYEE_WARRIOR_RECORD, 
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
            Key = EntityTypes.JOB, 
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
            Key = EntityTypes.JOB_CATEGORY, 
            NameFa = "دسته بندی شغل", 
            NameEn = "Job Category", 
            Code = "0016", 
            Ordering = 85,
            Attributes = new EntityTypeAttributes
            {
                Readable = true, Creatable = true, Editable = true, Deletable = true, 
                Loggable = true, Printable = true, Importable = false, Exportable = true, 
                IfNotCreator = false, HasRestriction = false, Permissible = true
            }
        };

        yield return new EntityTypeDefinition 
        { 
            Key = EntityTypes.POSITION_JOB, 
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

        yield return new EntityTypeDefinition 
        { 
            Key = EntityTypes.EDUCATIONAL_STATUS, 
            NameFa = "وضعیت تحصیلی", 
            NameEn = "Educational Status", 
            Code = "0018", 
            Ordering = 100,
            Attributes = new EntityTypeAttributes
            {
                Readable = true, Creatable = true, Editable = true, Deletable = true, 
                Loggable = true, Printable = true, Importable = false, Exportable = true, 
                IfNotCreator = false, HasRestriction = false, Permissible = true
            }
        };

        yield return new EntityTypeDefinition 
        { 
            Key = EntityTypes.EDUCATION_FIELD, 
            NameFa = "رشته تحصیلی", 
            NameEn = "Education Field", 
            Code = "0019", 
            Ordering = 105,
            Attributes = new EntityTypeAttributes
            {
                Readable = true, Creatable = true, Editable = true, Deletable = true, 
                Loggable = true, Printable = true, Importable = false, Exportable = true, 
                IfNotCreator = false, HasRestriction = false, Permissible = true
            }
        };

        yield return new EntityTypeDefinition 
        { 
            Key = EntityTypes.EDUCATION_LEVEL, 
            NameFa = "مقطع تحصیلی", 
            NameEn = "Education Level", 
            Code = "0020", 
            Ordering = 110,
            Attributes = new EntityTypeAttributes
            {
                Readable = true, Creatable = true, Editable = true, Deletable = true, 
                Loggable = true, Printable = true, Importable = false, Exportable = true, 
                IfNotCreator = false, HasRestriction = false, Permissible = true
            }
        };

        yield return new EntityTypeDefinition 
        { 
            Key = EntityTypes.MARITAL_STATUS, 
            NameFa = "وضعیت تاهل", 
            NameEn = "Marital Status", 
            Code = "0021", 
            Ordering = 120,
            Attributes = new EntityTypeAttributes
            {
                Readable = true, Creatable = true, Editable = true, Deletable = true, 
                Loggable = true, Printable = true, Importable = false, Exportable = true, 
                IfNotCreator = false, HasRestriction = false, Permissible = true
            }
        };

        yield return new EntityTypeDefinition 
        { 
            Key = EntityTypes.MILITARY_SERVICE_STATUS, 
            NameFa = "وضعیت خدمت سربازی", 
            NameEn = "Military Service Status", 
            Code = "0022", 
            Ordering = 130,
            Attributes = new EntityTypeAttributes
            {
                Readable = true, Creatable = true, Editable = true, Deletable = true, 
                Loggable = true, Printable = true, Importable = false, Exportable = true, 
                IfNotCreator = false, HasRestriction = false, Permissible = true
            }
        };

        yield return new EntityTypeDefinition 
        { 
            Key = EntityTypes.RELATIVE_TYPE, 
            NameFa = "نوع نسبت", 
            NameEn = "Relative Type", 
            Code = "0023", 
            Ordering = 140,
            Attributes = new EntityTypeAttributes
            {
                Readable = true, Creatable = true, Editable = true, Deletable = true, 
                Loggable = true, Printable = true, Importable = false, Exportable = true, 
                IfNotCreator = false, HasRestriction = false, Permissible = true
            }
        };
    }
}
