using NGErp.Base.Domain.Constants;
using NGErp.Base.Domain.EntityTypeRegistration;
using NGErp.Shared.Domain.Constants;

namespace NGErp.Shared.Service.EntityTypeRegistrations;

public class SharedModuleProfile : IEntityTypeModuleProfile
{
    public long ModuleId => ModuleIds.Shared;
    public string Prefix => "shared";
    public bool DeleteStale => false;

    public IEnumerable<EntityTypeDefinition> GetDefinitions()
    {
        yield return new EntityTypeDefinition 
        { 
            Key = EntityTypes.Department, 
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
            Key = EntityTypes.Position, 
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
            Key = EntityTypes.OrganizationalStructure, 
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
            Key = EntityTypes.OrganizationalStructureItem, 
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
            Key = EntityTypes.OrganizationNode, 
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
            Key = EntityTypes.Category,
            NameFa = "طبقه‌بندی کالا",
            NameEn = "Category",
            Code = "0019",
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
            },
        };

        yield return new EntityTypeDefinition
        {
            Key = EntityTypes.CategoryAttributeRule,
            NameFa = "قانون ویژگی دسته بندی",
            NameEn = "Category Attr Rule",
            Code = "0023",
            Ordering = 101,
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
                Permissible = false
            },
        };

        yield return new EntityTypeDefinition
        {
            Key = EntityTypes.CategoryLevelConstraint,
            NameFa = "محدودیت سطح دسته بندی",
            NameEn = "Category Level Constraint",
            Code = "0020",
            Ordering = 102,
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
