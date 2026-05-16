using NGErp.Base.Domain.EntityTypeRegistration;
using NGErp.Payroll.Domain.Constants;

namespace NGErp.Payroll.Service.EntityTypeRegistrations;

public class PayrollModuleProfile : IEntityTypeModuleProfile
{
    public long ModuleId => 10; // Payroll Module ID
    public string Prefix => "payroll";
    public bool DeleteStale => true;

    public IEnumerable<EntityTypeDefinition> GetDefinitions()
    {
        yield return new EntityTypeDefinition
        {
            Key = EntityTypes.MODULE_ENTITY,
            NameFa = "موجودیت ماژول",
            NameEn = "Module Entity",
            Code = "0001",
            Attributes = new EntityTypeAttributes { Readable = false, Creatable = false, Editable = false, Deletable = false, Loggable = false, Printable = false, Importable = false, Exportable = false, IfNotCreator = false, HasRestriction = false, Permissible = true },
            Commands = new List<EntityTypeCommandDefinition>
            {
                new() { Key = EntityTypeCommands.MODULE_ENTITY_VIEW, NameFa = "مشاهده", NameEn = "View" }
            }
        };

        yield return new EntityTypeDefinition
        {
            Key = EntityTypes.PAYROLL_MODULE_ENTITY,
            NameFa = "موجودیت ماژول دستمزد",
            NameEn = "Payroll Module Entity",
            Code = "0002",
            Attributes = new EntityTypeAttributes { Readable = true, Creatable = true, Editable = true, Deletable = true, Loggable = true, Printable = true, Importable = false, Exportable = true, IfNotCreator = false, HasRestriction = false, Permissible = true }
        };
    }
}
