using NGErp.Base.Domain.Constants;
using NGErp.Base.Domain.EntityTypeRegistration;
using NGErp.Warehouse.Domain.Constants;

namespace NGErp.Warehouse.Service.EntityTypeRegistrations;

public class WarehouseModuleProfile : IEntityTypeModuleProfile
{
    public long ModuleId => ModuleIds.Warehouse;
    public string Prefix => "warehouse";
    public bool DeleteStale => true;

    public IEnumerable<EntityTypeDefinition> GetDefinitions()
    {
        yield return new EntityTypeDefinition
        {
            Key = EntityTypes.ModuleEntity,
            NameFa = "موجودیت ماژول",
            NameEn = "Module Entity",
            Code = "0001",
            Attributes = new EntityTypeAttributes { Readable = false, Creatable = false, Editable = false, Deletable = false, Loggable = false, Printable = false, Importable = false, Exportable = false, IfNotCreator = false, HasRestriction = false, Permissible = true },
            Commands = new List<EntityTypeCommandDefinition>
            {
                new() { Key = EntityTypeCommands.ModuleEntityView, NameFa = "مشاهده", NameEn = "View" }
            }
        };

        yield return new EntityTypeDefinition
        {
            Key = EntityTypes.WarehouseType,
            NameFa = "نوع انبار",
            NameEn = "Warehouse Type",
            Code = "0002",
            Ordering = 10,
            Attributes = new EntityTypeAttributes { Readable = true, Creatable = true, Editable = true, Deletable = true, Loggable = true, Printable = true, Importable = false, Exportable = true, IfNotCreator = false, HasRestriction = false, Permissible = true }
        };

        yield return new EntityTypeDefinition
        {
            Key = EntityTypes.Warehouse,
            NameFa = "انبار",
            NameEn = "Warehouse",
            Code = "0003",
            Ordering = 20,
            Attributes = new EntityTypeAttributes { Readable = true, Creatable = true, Editable = true, Deletable = true, Loggable = true, Printable = true, Importable = false, Exportable = true, IfNotCreator = false, HasRestriction = false, Permissible = true }
        };

        yield return new EntityTypeDefinition
        {
            Key = EntityTypes.WarehouseLocation,
            NameFa = "محل انبار",
            NameEn = "Warehouse Location",
            Code = "0004",
            Ordering = 30,
            Attributes = new EntityTypeAttributes { Readable = true, Creatable = true, Editable = true, Deletable = true, Loggable = true, Printable = true, Importable = false, Exportable = true, IfNotCreator = false, HasRestriction = false, Permissible = true }
        };

        yield return new EntityTypeDefinition
        {
            Key = EntityTypes.UnitOfMeasurement,
            NameFa = "واحد اندازه‌گیری",
            NameEn = "Unit Of Measurement",
            Code = "0005",
            Ordering = 40,
            Attributes = new EntityTypeAttributes { Readable = true, Creatable = true, Editable = true, Deletable = true, Loggable = true, Printable = true, Importable = false, Exportable = true, IfNotCreator = false, HasRestriction = false, Permissible = true }
        };

        yield return new EntityTypeDefinition
        {
            Key = EntityTypes.ShippingCompany,
            NameFa = "شرکت حمل و نقل",
            NameEn = "Shipping Company",
            Code = "0006",
            Ordering = 50,
            Attributes = new EntityTypeAttributes { Readable = true, Creatable = true, Editable = true, Deletable = true, Loggable = true, Printable = true, Importable = false, Exportable = true, IfNotCreator = false, HasRestriction = false, Permissible = true }
        };

        yield return new EntityTypeDefinition
        {
            Key = EntityTypes.MeasurementDimension,
            NameFa = "بعد اندازه‌گیری",
            NameEn = "Measurement Dimension",
            Code = "0007",
            Ordering = 60,
            Attributes = new EntityTypeAttributes { Readable = true, Creatable = true, Editable = true, Deletable = false, Loggable = true, Printable = true, Importable = false, Exportable = true, IfNotCreator = false, HasRestriction = false, Permissible = true }
        };

        yield return new EntityTypeDefinition
        {
            Key = EntityTypes.ItemType,
            NameFa = "نوع کالا",
            NameEn = "Item Type",
            Code = "0008",
            Ordering = 70,
            Attributes = new EntityTypeAttributes { Readable = true, Creatable = true, Editable = true, Deletable = true, Loggable = true, Printable = true, Importable = false, Exportable = true, IfNotCreator = false, HasRestriction = false, Permissible = true }
        };

        yield return new EntityTypeDefinition
        {
            Key = EntityTypes.Item,
            NameFa = "کالا",
            NameEn = "Item",
            Code = "0009",
            Ordering = 80,
            Attributes = new EntityTypeAttributes { Readable = true, Creatable = true, Editable = true, Deletable = true, Loggable = true, Printable = true, Importable = false, Exportable = true, IfNotCreator = false, HasRestriction = false, Permissible = true }
        };

        yield return new EntityTypeDefinition
        {
            Key = EntityTypes.ItemAttribute,
            NameFa = "ویژگی کالا",
            NameEn = "Item Attribute",
            Code = "0010",
            Ordering = 82,
            Attributes = new EntityTypeAttributes { Readable = true, Creatable = true, Editable = true, Deletable = true, Loggable = true, Printable = true, Importable = false, Exportable = true, IfNotCreator = false, HasRestriction = false, Permissible = true }
        };

        yield return new EntityTypeDefinition
        {
            Key = EntityTypes.ItemUnitOfMeasurement,
            NameFa = "واحد اندازه‌گیری کالا",
            NameEn = "Item Unit Of Measurement",
            Code = "0011",
            Ordering = 84,
            Attributes = new EntityTypeAttributes { Readable = true, Creatable = true, Editable = true, Deletable = true, Loggable = true, Printable = true, Importable = false, Exportable = true, IfNotCreator = false, HasRestriction = false, Permissible = true }
        };

        yield return new EntityTypeDefinition
        {
            Key = EntityTypes.ItemUnitOfMeasurementConversion,
            NameFa = "تبدیل واحد کالا",
            NameEn = "Item Unit Conversion",
            Code = "0012",
            Ordering = 86,
            Attributes = new EntityTypeAttributes { Readable = true, Creatable = true, Editable = true, Deletable = true, Loggable = true, Printable = true, Importable = false, Exportable = true, IfNotCreator = false, HasRestriction = false, Permissible = true }
        };

        yield return new EntityTypeDefinition
        {
            Key = EntityTypes.ItemWarehouse,
            NameFa = "کالای انبار",
            NameEn = "Item Warehouse",
            Code = "0013",
            Ordering = 87,
            Attributes = new EntityTypeAttributes { Readable = true, Creatable = true, Editable = true, Deletable = true, Loggable = true, Printable = true, Importable = false, Exportable = true, IfNotCreator = false, HasRestriction = false, Permissible = true }
        };

        yield return new EntityTypeDefinition
        {
            Key = EntityTypes.ItemWarehouseLocation,
            NameFa = "محل کالای انبار",
            NameEn = "Item Warehouse Location",
            Code = "0014",
            Ordering = 88,
            Attributes = new EntityTypeAttributes { Readable = true, Creatable = true, Editable = true, Deletable = true, Loggable = true, Printable = true, Importable = false, Exportable = true, IfNotCreator = false, HasRestriction = false, Permissible = true }
        };

        yield return new EntityTypeDefinition
        {
            Key = EntityTypes.InventoryMovementType,
            NameFa = "نوع حرکت موجودی",
            NameEn = "Inventory Movement Type",
            Code = "0015",
            Ordering = 90,
            Attributes = new EntityTypeAttributes { Readable = true, Creatable = true, Editable = true, Deletable = true, Loggable = true, Printable = true, Importable = false, Exportable = true, IfNotCreator = false, HasRestriction = false, Permissible = true }
        };

        yield return new EntityTypeDefinition
        {
            Key = EntityTypes.InventoryMovement,
            NameFa = "حرکت موجودی",
            NameEn = "Inventory Movement",
            Code = "0016",
            Ordering = 95,
            Attributes = new EntityTypeAttributes { Readable = true, Creatable = true, Editable = true, Deletable = true, Loggable = true, Printable = true, Importable = false, Exportable = true, IfNotCreator = false, HasRestriction = false, Permissible = true }
        };

        yield return new EntityTypeDefinition
        {
            Key = EntityTypes.InventoryLot,
            NameFa = "بچ موجودی",
            NameEn = "Inventory Lot",
            Code = "0017",
            Ordering = 97,
            Attributes = new EntityTypeAttributes { Readable = true, Creatable = true, Editable = true, Deletable = true, Loggable = true, Printable = true, Importable = false, Exportable = true, IfNotCreator = false, HasRestriction = false, Permissible = true }
        };

        yield return new EntityTypeDefinition
        {
            Key = EntityTypes.InventoryLotAttributeValue,
            NameFa = "مقدار ویژگی بچ موجودی",
            NameEn = "Inventory Lot Attr Value",
            Code = "0018",
            Ordering = 98,
            Attributes = new EntityTypeAttributes { Readable = true, Creatable = true, Editable = true, Deletable = true, Loggable = true, Printable = true, Importable = false, Exportable = true, IfNotCreator = false, HasRestriction = false, Permissible = true }
        };

        yield return new EntityTypeDefinition
        {
            Key = EntityTypes.Category,
            NameFa = "دسته بندی",
            NameEn = "Category",
            Code = "0019",
            Ordering = 100,
            Attributes = new EntityTypeAttributes { Readable = true, Creatable = true, Editable = true, Deletable = true, Loggable = true, Printable = true, Importable = false, Exportable = true, IfNotCreator = false, HasRestriction = false, Permissible = true }
        };

        yield return new EntityTypeDefinition
        {
            Key = EntityTypes.CategoryLevelConstraint,
            NameFa = "محدودیت سطح دسته بندی",
            NameEn = "Category Level Constraint",
            Code = "0020",
            Ordering = 110,
            Attributes = new EntityTypeAttributes { Readable = true, Creatable = true, Editable = true, Deletable = true, Loggable = true, Printable = true, Importable = false, Exportable = true, IfNotCreator = false, HasRestriction = false, Permissible = true }
        };

        yield return new EntityTypeDefinition
        {
            Key = EntityTypes.Attribute,
            NameFa = "ویژگی",
            NameEn = "Attribute",
            Code = "0021",
            Ordering = 120,
            Attributes = new EntityTypeAttributes { Readable = true, Creatable = true, Editable = true, Deletable = true, Loggable = true, Printable = true, Importable = false, Exportable = true, IfNotCreator = false, HasRestriction = false, Permissible = true }
        };

        yield return new EntityTypeDefinition
        {
            Key = EntityTypes.AttributeEnumValue,
            NameFa = "مقدار شمارشی ویژگی",
            NameEn = "Attribute Enum Value",
            Code = "0022",
            Ordering = 130,
            Attributes = new EntityTypeAttributes { Readable = true, Creatable = true, Editable = true, Deletable = true, Loggable = true, Printable = true, Importable = false, Exportable = true, IfNotCreator = false, HasRestriction = false, Permissible = true }
        };

        yield return new EntityTypeDefinition
        {
            Key = EntityTypes.CategoryAttributeRule,
            NameFa = "قانون ویژگی دسته بندی",
            NameEn = "Category Attr Rule",
            Code = "0023",
            Ordering = 140,
            Attributes = new EntityTypeAttributes { Readable = true, Creatable = true, Editable = true, Deletable = true, Loggable = true, Printable = true, Importable = false, Exportable = true, IfNotCreator = false, HasRestriction = false, Permissible = true }
        };

        yield return new EntityTypeDefinition
        {
            Key = EntityTypes.ReceiptType,
            NameFa = "نوع رسید",
            NameEn = "Receipt Type",
            Code = "0024",
            Ordering = 150,
            Attributes = new EntityTypeAttributes { Readable = true, Creatable = true, Editable = true, Deletable = true, Loggable = true, Printable = true, Importable = false, Exportable = true, IfNotCreator = false, HasRestriction = false, Permissible = true }
        };

        yield return new EntityTypeDefinition
        {
            Key = EntityTypes.RemittanceType,
            NameFa = "نوع حواله",
            NameEn = "Remittance Type",
            Code = "0025",
            Ordering = 160,
            Attributes = new EntityTypeAttributes { Readable = true, Creatable = true, Editable = true, Deletable = true, Loggable = true, Printable = true, Importable = false, Exportable = true, IfNotCreator = false, HasRestriction = false, Permissible = true }
        };

        yield return new EntityTypeDefinition
        {
            Key = EntityTypes.WarehouseReceipt,
            NameFa = "رسید انبار",
            NameEn = "Warehouse Receipt",
            Code = "0026",
            Ordering = 170,
            Attributes = new EntityTypeAttributes { Readable = true, Creatable = true, Editable = true, Deletable = true, Loggable = true, Printable = true, Importable = false, Exportable = true, IfNotCreator = false, HasRestriction = false, Permissible = true }
        };

        yield return new EntityTypeDefinition
        {
            Key = EntityTypes.WarehouseRemittance,
            NameFa = "حواله انبار",
            NameEn = "Warehouse Remittance",
            Code = "0027",
            Ordering = 180,
            Attributes = new EntityTypeAttributes { Readable = true, Creatable = true, Editable = true, Deletable = true, Loggable = true, Printable = true, Importable = false, Exportable = true, IfNotCreator = false, HasRestriction = false, Permissible = true }
        };

        yield return new EntityTypeDefinition
        {
            Key = EntityTypes.SiUnit,
            NameFa = "واحد SI",
            NameEn = "SI Unit",
            Code = "0028",
            Ordering = 190,
            Attributes = new EntityTypeAttributes { Readable = true, Creatable = true, Editable = true, Deletable = true, Loggable = true, Printable = true, Importable = false, Exportable = true, IfNotCreator = false, HasRestriction = false, Permissible = true }
        };
    }
}
