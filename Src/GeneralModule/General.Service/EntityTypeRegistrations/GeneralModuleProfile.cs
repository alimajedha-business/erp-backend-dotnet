using NGErp.Base.Domain.Constants;
using NGErp.Base.Domain.EntityTypeRegistration;
using NGErp.General.Domain.Constants;

namespace NGErp.General.Service.EntityTypeRegistrations;

public class GeneralModuleProfile : IEntityTypeModuleProfile
{
    public long ModuleId => ModuleIds.General;
    public string Prefix => "general";
    public bool DeleteStale => false;

    public IEnumerable<EntityTypeDefinition> GetDefinitions()
    {
        yield return new EntityTypeDefinition
        {
            Key = EntityTypes.JobCategory,
            NameFa = "دسته بندی شغل",
            NameEn = "Job Category",
            Code = "0016",
            Ordering = 85,
            Attributes = new EntityTypeAttributes
            {
                Readable = true,
                Creatable = true,
                Editable = true,
                Deletable = true,
                Loggable = true,
                Printable = true,
                Importable = false,
                Exportable = true,
                IfNotCreator = false,
                HasRestriction = false,
                Permissible = true
            }
        };

        yield return new EntityTypeDefinition
        {
            Key = EntityTypes.EducationalStatus,
            NameFa = "وضعیت تحصیلی",
            NameEn = "Educational Status",
            Code = "0018",
            Ordering = 100,
            Attributes = new EntityTypeAttributes
            {
                Readable = true,
                Creatable = true,
                Editable = true,
                Deletable = true,
                Loggable = true,
                Printable = true,
                Importable = false,
                Exportable = true,
                IfNotCreator = false,
                HasRestriction = false,
                Permissible = true
            }
        };

        yield return new EntityTypeDefinition
        {
            Key = EntityTypes.EducationField,
            NameFa = "رشته تحصیلی",
            NameEn = "Education Field",
            Code = "0019",
            Ordering = 105,
            Attributes = new EntityTypeAttributes
            {
                Readable = true,
                Creatable = true,
                Editable = true,
                Deletable = true,
                Loggable = true,
                Printable = true,
                Importable = false,
                Exportable = true,
                IfNotCreator = false,
                HasRestriction = false,
                Permissible = true
            }
        };

        yield return new EntityTypeDefinition
        {
            Key = EntityTypes.EducationLevel,
            NameFa = "مقطع تحصیلی",
            NameEn = "Education Level",
            Code = "0020",
            Ordering = 110,
            Attributes = new EntityTypeAttributes
            {
                Readable = true,
                Creatable = true,
                Editable = true,
                Deletable = true,
                Loggable = true,
                Printable = true,
                Importable = false,
                Exportable = true,
                IfNotCreator = false,
                HasRestriction = false,
                Permissible = true
            }
        };

        yield return new EntityTypeDefinition
        {
            Key = EntityTypes.MaritalStatus,
            NameFa = "وضعیت تاهل",
            NameEn = "Marital Status",
            Code = "0021",
            Ordering = 120,
            Attributes = new EntityTypeAttributes
            {
                Readable = true,
                Creatable = true,
                Editable = true,
                Deletable = true,
                Loggable = true,
                Printable = true,
                Importable = false,
                Exportable = true,
                IfNotCreator = false,
                HasRestriction = false,
                Permissible = true
            }
        };

        yield return new EntityTypeDefinition
        {
            Key = EntityTypes.MilitaryServiceStatus,
            NameFa = "وضعیت خدمت سربازی",
            NameEn = "Military Service Status",
            Code = "0022",
            Ordering = 130,
            Attributes = new EntityTypeAttributes
            {
                Readable = true,
                Creatable = true,
                Editable = true,
                Deletable = true,
                Loggable = true,
                Printable = true,
                Importable = false,
                Exportable = true,
                IfNotCreator = false,
                HasRestriction = false,
                Permissible = true
            }
        };

        yield return new EntityTypeDefinition
        {
            Key = EntityTypes.RelativeType,
            NameFa = "نوع نسبت",
            NameEn = "Relative Type",
            Code = "0023",
            Ordering = 140,
            Attributes = new EntityTypeAttributes
            {
                Readable = true,
                Creatable = true,
                Editable = true,
                Deletable = true,
                Loggable = true,
                Printable = true,
                Importable = false,
                Exportable = true,
                IfNotCreator = false,
                HasRestriction = false,
                Permissible = true
            }
        };

        yield return new EntityTypeDefinition
        {
            Key = EntityTypes.ItemType,
            NameFa = "نوع کالا",
            NameEn = "Item Type",
            Code = "0008",
            Ordering = 70,
            Attributes = new EntityTypeAttributes
            {
                Readable = true,
                Creatable = true,
                Editable = true,
                Deletable = true,
                Loggable = true,
                Printable = true,
                Importable = false,
                Exportable = true,
                IfNotCreator = false,
                HasRestriction = false,
                Permissible = true
            },
        };

        yield return new EntityTypeDefinition
        {
            Key = EntityTypes.SiUnit,
            NameFa = "واحد SI",
            NameEn = "SI Unit",
            Code = "0036",
            Ordering = 190,
            Attributes = new EntityTypeAttributes
            {
                Readable = true,
                Creatable = true,
                Editable = true,
                Deletable = true,
                Loggable = true,
                Printable = true,
                Importable = false,
                Exportable = true,
                IfNotCreator = false,
                HasRestriction = false,
                Permissible = true
            }
        };

        yield return new EntityTypeDefinition
        {
            Key = EntityTypes.ShippingCompany,
            NameFa = "شرکت حمل و نقل",
            NameEn = "Shipping Company",
            Code = "0006",
            Ordering = 50,
            Attributes = new EntityTypeAttributes
            {
                Readable = true,
                Creatable = true,
                Editable = true,
                Deletable = true,
                Loggable = true,
                Printable = true,
                Importable = false,
                Exportable = true,
                IfNotCreator = false,
                HasRestriction = false,
                Permissible = true
            },
        };

        yield return new EntityTypeDefinition
        {
            Key = EntityTypes.UnitOfMeasurement,
            NameFa = "واحد اندازه‌گیری",
            NameEn = "Unit Of Measurement",
            Code = "0005",
            Ordering = 40,
            Attributes = new EntityTypeAttributes
            {
                Readable = true,
                Creatable = true,
                Editable = true,
                Deletable = true,
                Loggable = true,
                Printable = true,
                Importable = false,
                Exportable = true,
                IfNotCreator = false,
                HasRestriction = false,
                Permissible = true
            },
        };

        yield return new EntityTypeDefinition
        {
            Key = EntityTypes.WarehouseType,
            NameFa = "نوع انبار",
            NameEn = "Warehouse Type",
            Code = "0002",
            Ordering = 20,
            Attributes = new EntityTypeAttributes
            {
                Readable = true,
                Creatable = true,
                Editable = true,
                Deletable = true,
                Loggable = true,
                Printable = true,
                Importable = false,
                Exportable = true,
                IfNotCreator = false,
                HasRestriction = false,
                Permissible = true
            },
        };
    }
}
