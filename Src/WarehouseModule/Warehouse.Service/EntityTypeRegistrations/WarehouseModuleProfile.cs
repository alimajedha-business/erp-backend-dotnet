using NGErp.Base.Domain.EntityTypeRegistration;
using NGErp.Warehouse.Domain.Constants;

namespace NGErp.Warehouse.Service.EntityTypeRegistrations;

public class WarehouseModuleProfile : IEntityTypeModuleProfile
{
    public long ModuleId => 4;
    public string Prefix => "warehouse";
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
            Key = EntityTypes.WAREHOUSE_TYPE,
            NameFa = "نوع انبار",
            NameEn = "Warehouse Type",
            Code = "0002",
            Ordering = 10,
            Attributes = new EntityTypeAttributes { Readable = true, Creatable = true, Editable = true, Deletable = true, Loggable = true, Printable = true, Importable = false, Exportable = true, IfNotCreator = false, HasRestriction = false, Permissible = true }
        };

        yield return new EntityTypeDefinition
        {
            Key = EntityTypes.WAREHOUSE,
            NameFa = "انبار",
            NameEn = "Warehouse",
            Code = "0003",
            Ordering = 20,
            Attributes = new EntityTypeAttributes { Readable = true, Creatable = true, Editable = true, Deletable = true, Loggable = true, Printable = true, Importable = false, Exportable = true, IfNotCreator = false, HasRestriction = false, Permissible = true }
        };

        yield return new EntityTypeDefinition
        {
            Key = EntityTypes.WAREHOUSE_LOCATION,
            NameFa = "محل انبار",
            NameEn = "Warehouse Location",
            Code = "0004",
            Ordering = 30,
            Attributes = new EntityTypeAttributes { Readable = true, Creatable = true, Editable = true, Deletable = true, Loggable = true, Printable = true, Importable = false, Exportable = true, IfNotCreator = false, HasRestriction = false, Permissible = true }
        };

        yield return new EntityTypeDefinition
        {
            Key = EntityTypes.UNIT_OF_MEASUREMENT,
            NameFa = "واحد اندازه‌گیری",
            NameEn = "Unit Of Measurement",
            Code = "0005",
            Ordering = 40,
            Attributes = new EntityTypeAttributes { Readable = true, Creatable = true, Editable = true, Deletable = true, Loggable = true, Printable = true, Importable = false, Exportable = true, IfNotCreator = false, HasRestriction = false, Permissible = true }
        };

        yield return new EntityTypeDefinition
        {
            Key = EntityTypes.SHIPPING_COMPANY,
            NameFa = "شرکت حمل و نقل",
            NameEn = "Shipping Company",
            Code = "0006",
            Ordering = 50,
            Attributes = new EntityTypeAttributes { Readable = true, Creatable = true, Editable = true, Deletable = true, Loggable = true, Printable = true, Importable = false, Exportable = true, IfNotCreator = false, HasRestriction = false, Permissible = true }
        };

        yield return new EntityTypeDefinition
        {
            Key = EntityTypes.MEASUREMENT_DIMENSION,
            NameFa = "بعد اندازه‌گیری",
            NameEn = "Measurement Dimension",
            Code = "0007",
            Ordering = 60,
            Attributes = new EntityTypeAttributes { Readable = true, Creatable = true, Editable = true, Deletable = false, Loggable = true, Printable = true, Importable = false, Exportable = true, IfNotCreator = false, HasRestriction = false, Permissible = true }
        };

        yield return new EntityTypeDefinition
        {
            Key = EntityTypes.ITEM_TYPE,
            NameFa = "نوع کالا",
            NameEn = "Item Type",
            Code = "0008",
            Ordering = 70,
            Attributes = new EntityTypeAttributes { Readable = true, Creatable = true, Editable = true, Deletable = true, Loggable = true, Printable = true, Importable = false, Exportable = true, IfNotCreator = false, HasRestriction = false, Permissible = true }
        };

        yield return new EntityTypeDefinition
        {
            Key = EntityTypes.ITEM,
            NameFa = "کالا",
            NameEn = "Item",
            Code = "0009",
            Ordering = 80,
            Attributes = new EntityTypeAttributes { Readable = true, Creatable = true, Editable = true, Deletable = true, Loggable = true, Printable = true, Importable = false, Exportable = true, IfNotCreator = false, HasRestriction = false, Permissible = true }
        };

        yield return new EntityTypeDefinition
        {
            Key = EntityTypes.ITEM_ATTRIBUTE,
            NameFa = "ویژگی کالا",
            NameEn = "Item Attribute",
            Code = "0010",
            Ordering = 82,
            Attributes = new EntityTypeAttributes { Readable = true, Creatable = true, Editable = true, Deletable = true, Loggable = true, Printable = true, Importable = false, Exportable = true, IfNotCreator = false, HasRestriction = false, Permissible = true }
        };

        yield return new EntityTypeDefinition
        {
            Key = EntityTypes.ITEM_UNIT_OF_MEASUREMENT,
            NameFa = "واحد اندازه‌گیری کالا",
            NameEn = "Item Unit Of Measurement",
            Code = "0011",
            Ordering = 84,
            Attributes = new EntityTypeAttributes { Readable = true, Creatable = true, Editable = true, Deletable = true, Loggable = true, Printable = true, Importable = false, Exportable = true, IfNotCreator = false, HasRestriction = false, Permissible = true }
        };

        yield return new EntityTypeDefinition
        {
            Key = EntityTypes.ITEM_UNIT_OF_MEASUREMENT_CONVERSION,
            NameFa = "تبدیل واحد کالا",
            NameEn = "Item Unit Conversion",
            Code = "0012",
            Ordering = 86,
            Attributes = new EntityTypeAttributes { Readable = true, Creatable = true, Editable = true, Deletable = true, Loggable = true, Printable = true, Importable = false, Exportable = true, IfNotCreator = false, HasRestriction = false, Permissible = true }
        };

        yield return new EntityTypeDefinition
        {
            Key = EntityTypes.ITEM_WAREHOUSE,
            NameFa = "کالای انبار",
            NameEn = "Item Warehouse",
            Code = "0013",
            Ordering = 87,
            Attributes = new EntityTypeAttributes { Readable = true, Creatable = true, Editable = true, Deletable = true, Loggable = true, Printable = true, Importable = false, Exportable = true, IfNotCreator = false, HasRestriction = false, Permissible = true }
        };

        yield return new EntityTypeDefinition
        {
            Key = EntityTypes.ITEM_WAREHOUSE_LOCATION,
            NameFa = "محل کالای انبار",
            NameEn = "Item Warehouse Location",
            Code = "0014",
            Ordering = 88,
            Attributes = new EntityTypeAttributes { Readable = true, Creatable = true, Editable = true, Deletable = true, Loggable = true, Printable = true, Importable = false, Exportable = true, IfNotCreator = false, HasRestriction = false, Permissible = true }
        };

        yield return new EntityTypeDefinition
        {
            Key = EntityTypes.INVENTORY_MOVEMENT_TYPE,
            NameFa = "نوع حرکت موجودی",
            NameEn = "Inventory Movement Type",
            Code = "0015",
            Ordering = 90,
            Attributes = new EntityTypeAttributes { Readable = true, Creatable = true, Editable = true, Deletable = true, Loggable = true, Printable = true, Importable = false, Exportable = true, IfNotCreator = false, HasRestriction = false, Permissible = true }
        };

        yield return new EntityTypeDefinition
        {
            Key = EntityTypes.INVENTORY_MOVEMENT,
            NameFa = "حرکت موجودی",
            NameEn = "Inventory Movement",
            Code = "0016",
            Ordering = 95,
            Attributes = new EntityTypeAttributes { Readable = true, Creatable = true, Editable = true, Deletable = true, Loggable = true, Printable = true, Importable = false, Exportable = true, IfNotCreator = false, HasRestriction = false, Permissible = true }
        };

        yield return new EntityTypeDefinition
        {
            Key = EntityTypes.INVENTORY_LOT,
            NameFa = "بچ موجودی",
            NameEn = "Inventory Lot",
            Code = "0017",
            Ordering = 97,
            Attributes = new EntityTypeAttributes { Readable = true, Creatable = true, Editable = true, Deletable = true, Loggable = true, Printable = true, Importable = false, Exportable = true, IfNotCreator = false, HasRestriction = false, Permissible = true }
        };

        yield return new EntityTypeDefinition
        {
            Key = EntityTypes.INVENTORY_LOT_ATTRIBUTE_VALUE,
            NameFa = "مقدار ویژگی بچ موجودی",
            NameEn = "Inventory Lot Attr Value",
            Code = "0018",
            Ordering = 98,
            Attributes = new EntityTypeAttributes { Readable = true, Creatable = true, Editable = true, Deletable = true, Loggable = true, Printable = true, Importable = false, Exportable = true, IfNotCreator = false, HasRestriction = false, Permissible = true }
        };

        yield return new EntityTypeDefinition
        {
            Key = EntityTypes.CATEGORY,
            NameFa = "دسته بندی",
            NameEn = "Category",
            Code = "0019",
            Ordering = 100,
            Attributes = new EntityTypeAttributes { Readable = true, Creatable = true, Editable = true, Deletable = true, Loggable = true, Printable = true, Importable = false, Exportable = true, IfNotCreator = false, HasRestriction = false, Permissible = true }
        };

        yield return new EntityTypeDefinition
        {
            Key = EntityTypes.CATEGORY_LEVEL_CONSTRAINT,
            NameFa = "محدودیت سطح دسته بندی",
            NameEn = "Category Level Constraint",
            Code = "0020",
            Ordering = 110,
            Attributes = new EntityTypeAttributes { Readable = true, Creatable = true, Editable = true, Deletable = true, Loggable = true, Printable = true, Importable = false, Exportable = true, IfNotCreator = false, HasRestriction = false, Permissible = true }
        };

        yield return new EntityTypeDefinition
        {
            Key = EntityTypes.ATTRIBUTE,
            NameFa = "ویژگی",
            NameEn = "Attribute",
            Code = "0021",
            Ordering = 120,
            Attributes = new EntityTypeAttributes { Readable = true, Creatable = true, Editable = true, Deletable = true, Loggable = true, Printable = true, Importable = false, Exportable = true, IfNotCreator = false, HasRestriction = false, Permissible = true }
        };

        yield return new EntityTypeDefinition
        {
            Key = EntityTypes.ATTRIBUTE_ENUM_VALUE,
            NameFa = "مقدار شمارشی ویژگی",
            NameEn = "Attribute Enum Value",
            Code = "0022",
            Ordering = 130,
            Attributes = new EntityTypeAttributes { Readable = true, Creatable = true, Editable = true, Deletable = true, Loggable = true, Printable = true, Importable = false, Exportable = true, IfNotCreator = false, HasRestriction = false, Permissible = true }
        };

        yield return new EntityTypeDefinition
        {
            Key = EntityTypes.CATEGORY_ATTRIBUTE_RULE,
            NameFa = "قانون ویژگی دسته بندی",
            NameEn = "Category Attr Rule",
            Code = "0023",
            Ordering = 140,
            Attributes = new EntityTypeAttributes { Readable = true, Creatable = true, Editable = true, Deletable = true, Loggable = true, Printable = true, Importable = false, Exportable = true, IfNotCreator = false, HasRestriction = false, Permissible = true }
        };

        yield return new EntityTypeDefinition
        {
            Key = EntityTypes.RECEIPT_TYPE,
            NameFa = "نوع رسید",
            NameEn = "Receipt Type",
            Code = "0024",
            Ordering = 150,
            Attributes = new EntityTypeAttributes { Readable = true, Creatable = true, Editable = true, Deletable = true, Loggable = true, Printable = true, Importable = false, Exportable = true, IfNotCreator = false, HasRestriction = false, Permissible = true }
        };

        yield return new EntityTypeDefinition
        {
            Key = EntityTypes.REMITTANCE_TYPE,
            NameFa = "نوع حواله",
            NameEn = "Remittance Type",
            Code = "0025",
            Ordering = 160,
            Attributes = new EntityTypeAttributes { Readable = true, Creatable = true, Editable = true, Deletable = true, Loggable = true, Printable = true, Importable = false, Exportable = true, IfNotCreator = false, HasRestriction = false, Permissible = true }
        };
    }
}
