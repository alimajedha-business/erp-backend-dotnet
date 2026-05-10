using System.Reflection;

namespace NGErp.Tools.EntityTypeSync;

internal static class entity_types
{
    // HCM Module (6)
    public const string DEPARTMENT = "DEPARTMENT";
    public const string POSITION = "POSITION";
    public const string ORGANIZATIONAL_STRUCTURE = "ORGANIZATIONAL_STRUCTURE";
    public const string ORGANIZATIONAL_STRUCTURE_ITEM = "ORGANIZATIONAL_STRUCTURE_ITEM";
    public const string ORGANIZATION_NODE = "ORGANIZATION_NODE";
    public const string EMPLOYMENT_GROUP = "EMPLOYMENT_GROUP";
    public const string EMPLOYMENT_GROUP_SPECIFICATION = "EMPLOYMENT_GROUP_SPECIFICATION";
    public const string EMPLOYEE = "EMPLOYEE";
    public const string EMPLOYEE_EDUCATION = "EMPLOYEE_EDUCATION";
    public const string EMPLOYEE_WORK_EXPERIENCE = "EMPLOYEE_WORK_EXPERIENCE";
    public const string EMPLOYEE_DEPENDANT = "EMPLOYEE_DEPENDANT";
    public const string EMPLOYEE_RELATIVE = "EMPLOYEE_RELATIVE";
    public const string EMPLOYEE_WARRIOR_RECORD = "EMPLOYEE_WARRIOR_RECORD";
    public const string JOB = "JOB";
    public const string JOB_CATEGORY = "JOB_CATEGORY";
    public const string POSITION_JOB = "POSITION_JOB";
    public const string EDUCATIONAL_STATUS = "EDUCATIONAL_STATUS";
    public const string EDUCATION_FIELD = "EDUCATION_FIELD";
    public const string EDUCATION_LEVEL = "EDUCATION_LEVEL";
    public const string MARITAL_STATUS = "MARITAL_STATUS";
    public const string MILITARY_SERVICE_STATUS = "MILITARY_SERVICE_STATUS";
    public const string RELATIVE_TYPE = "RELATIVE_TYPE";

    // Warehouse Module (4)
    public const string WAREHOUSE_TYPE = "WAREHOUSE_TYPE";
    public const string WAREHOUSE = "WAREHOUSE";
    public const string WAREHOUSE_LOCATION = "WAREHOUSE_LOCATION";
    public const string UNIT_OF_MEASUREMENT = "UNIT_OF_MEASUREMENT";
    public const string SHIPPING_COMPANY = "SHIPPING_COMPANY";
    public const string MEASUREMENT_DIMENSION = "MEASUREMENT_DIMENSION";
    public const string ITEM_TYPE = "ITEM_TYPE";
    public const string ITEM = "ITEM";
    public const string ITEM_ATTRIBUTE = "ITEM_ATTRIBUTE";
    public const string ITEM_UNIT_OF_MEASUREMENT = "ITEM_UNIT_OF_MEASUREMENT";
    public const string ITEM_UNIT_OF_MEASUREMENT_CONVERSION = "ITEM_UNIT_OF_MEASUREMENT_CONVERSION";
    public const string ITEM_WAREHOUSE = "ITEM_WAREHOUSE";
    public const string ITEM_WAREHOUSE_LOCATION = "ITEM_WAREHOUSE_LOCATION";
    public const string INVENTORY_MOVEMENT_TYPE = "INVENTORY_MOVEMENT_TYPE";
    public const string INVENTORY_MOVEMENT = "INVENTORY_MOVEMENT";
    public const string INVENTORY_LOT = "INVENTORY_LOT";
    public const string INVENTORY_LOT_ATTRIBUTE_VALUE = "INVENTORY_LOT_ATTRIBUTE_VALUE";
    public const string CATEGORY = "CATEGORY";
    public const string CATEGORY_ATTRIBUTE_RULE = "CATEGORY_ATTRIBUTE_RULE";
    public const string CATEGORY_LEVEL_CONSTRAINT = "CATEGORY_LEVEL_CONSTRAINT";
    public const string ATTRIBUTE = "ATTRIBUTE";
    public const string ATTRIBUTE_ENUM_VALUE = "ATTRIBUTE_ENUM_VALUE";
    public const string RECEIPT_TYPE = "RECEIPT_TYPE";
    public const string REMITTANCE_TYPE = "REMITTANCE_TYPE";

    public static IReadOnlySet<string> DeclaredKeys { get; } = typeof(entity_types)
        .GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)
        .Where(field => field is { IsLiteral: true, IsInitOnly: false } && field.FieldType == typeof(string))
        .Select(field => (string)field.GetRawConstantValue()!)
        .ToHashSet(StringComparer.Ordinal);
}
