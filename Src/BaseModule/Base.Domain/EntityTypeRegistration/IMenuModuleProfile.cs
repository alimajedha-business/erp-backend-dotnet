namespace NGErp.Base.Domain.EntityTypeRegistration;

public interface IMenuModuleProfile
{
    long ModuleId { get; }
    bool DeleteStale { get; }
    IEnumerable<MenuDefinition> GetMenus();
}
