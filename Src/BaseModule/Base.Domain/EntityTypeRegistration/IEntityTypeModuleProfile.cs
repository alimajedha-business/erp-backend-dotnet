namespace NGErp.Base.Domain.EntityTypeRegistration;

public interface IEntityTypeModuleProfile
{
    long ModuleId { get; }
    string Prefix { get; }
    bool DeleteStale { get; }
    IEnumerable<EntityTypeDefinition> GetDefinitions();
}
