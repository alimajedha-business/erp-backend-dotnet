using System.Reflection;

namespace NGErp.Tools.EntityTypeSync;

internal static class entity_types
{
    public const string DEPARTMENT = "DEPARTMENT";
    public const string POSITION = "POSITION";
    public const string ORGANIZATIONAL_STRUCTURE = "ORGANIZATIONAL_STRUCTURE";
    public const string EMPLOYMENT_GROUP = "EMPLOYMENT_GROUP";
    public const string EMPLOYEE = "EMPLOYEE";
    public const string EMPLOYEE_EDUCATION = "EMPLOYEE_EDUCATION";
    public const string EMPLOYEE_WORK_EXPERIENCE = "EMPLOYEE_WORK_EXPERIENCE";

    public const string WAREHOUSE_TYPE = "WAREHOUSE_TYPE";
    public const string WAREHOUSE = "WAREHOUSE";
    public const string WAREHOUSE_LOCATION = "WAREHOUSE_LOCATION";
    public const string UNIT_OF_MEASUREMENT = "UNIT_OF_MEASUREMENT";
    public const string SHIPPING_COMPANY = "SHIPPING_COMPANY";
    public const string MEASUREMENT_DIMENSION = "MEASUREMENT_DIMENSION";
    public const string ITEM_TYPE = "ITEM_TYPE";
    public const string ITEM = "ITEM";
    public const string INVENTORY_MOVEMENT_TYPE = "INVENTORY_MOVEMENT_TYPE";
    public const string CATEGORY = "CATEGORY";
    public const string CATEGORY_LEVEL_CONSTRAINT = "CATEGORY_LEVEL_CONSTRAINT";
    public const string ATTRIBUTE = "ATTRIBUTE";
    public const string ATTRIBUTE_ENUM_VALUE = "ATTRIBUTE_ENUM_VALUE";

    public static IReadOnlySet<string> DeclaredKeys { get; } = typeof(entity_types)
        .GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)
        .Where(field => field is { IsLiteral: true, IsInitOnly: false } && field.FieldType == typeof(string))
        .Select(field => (string)field.GetRawConstantValue()!)
        .ToHashSet(StringComparer.Ordinal);
}
