namespace NGErp.Base.Domain.EntityTypeRegistration;

public class EntityTypeAttributes
{
    public bool Readable { get; init; }
    public bool Creatable { get; init; }
    public bool Editable { get; init; }
    public bool Deletable { get; init; }
    public bool Loggable { get; init; }
    public bool Printable { get; init; }
    public bool Importable { get; init; }
    public bool Exportable { get; init; }
    public bool IfNotCreator { get; init; }
    public bool HasRestriction { get; init; }
    public bool Permissible { get; init; } = true;
}

public class EntityTypeCommandDefinition
{
    public required string Key { get; init; }
    public required string NameFa { get; init; }
    public required string NameEn { get; init; }
    public short? Ordering { get; init; }
    public bool Permissible { get; init; } = true;
}

public class EntityTypeDefinition
{
    public required string Key { get; init; }
    public required string NameFa { get; init; }
    public required string NameEn { get; init; }
    public required string Code { get; init; }
    public Type? BaseModel { get; init; } // Mirroring Django's base_model
    public short? Ordering { get; init; }
    public required EntityTypeAttributes Attributes { get; init; }
    public List<EntityTypeCommandDefinition> Commands { get; init; } = [];
}

public interface IEntityTypeModuleProfile
{
    long ModuleId { get; }
    string Prefix { get; }
    bool DeleteStale { get; }
    IEnumerable<EntityTypeDefinition> GetDefinitions();
}
