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
            Ordering = null,
            Attributes = new EntityTypeAttributes
            {
                Readable = false,
                Creatable = false,
                Editable = false,
                Deletable = false,
                Loggable = false,
                Printable = false,
                Importable = false,
                Exportable = false,
                IfNotCreator = false,
                HasRestriction = false,
                Permissible = true
            },
            Commands =
            [
                new() { Key = EntityTypeCommands.ModuleEntityView, NameFa = "مشاهده", NameEn = "View" }
            ]
        };

        yield return new EntityTypeDefinition
        {
            Key = EntityTypes.Attribute,
            NameFa = "ویژگی",
            NameEn = "Attribute",
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
            },
        };

        yield return new EntityTypeDefinition
        {
            Key = EntityTypes.AttributeEnumValue,
            NameFa = "مقدار شمارشی ویژگی",
            NameEn = "Attribute Enum Value",
            Code = "0022",
            Ordering = 121,
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
                Permissible = true
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

        yield return new EntityTypeDefinition
        {
            Key = EntityTypes.Item,
            NameFa = "کالا",
            NameEn = "Item",
            Code = "0009",
            Ordering = 71,
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
            Key = EntityTypes.ItemAttribute,
            NameFa = "ویژگی کالا",
            NameEn = "Item Attribute",
            Code = "0010",
            Ordering = 72,
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
            Key = EntityTypes.ItemUnitOfMeasurement,
            NameFa = "واحد اندازه‌گیری کالا",
            NameEn = "Item Unit Of Measurement",
            Code = "0011",
            Ordering = 73,
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
            Key = EntityTypes.ItemUnitOfMeasurementConversion,
            NameFa = "تبدیل واحد کالا",
            NameEn = "Item Unit Conversion",
            Code = "0012",
            Ordering = 74,
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
            Key = EntityTypes.ItemWarehouse,
            NameFa = "کالای انبار",
            NameEn = "Item Warehouse",
            Code = "0013",
            Ordering = 75,
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
            Key = EntityTypes.ItemWarehouseLocation,
            NameFa = "محل کالای انبار",
            NameEn = "Item Warehouse Location",
            Code = "0014",
            Ordering = 76,
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
            Key = EntityTypes.Receipt,
            NameFa = "رسید انبار",
            NameEn = "Receipt",
            Code = "0024",
            Ordering = 150,
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
            Key = EntityTypes.ReceiptFieldDefinition,
            NameFa = "تعریف ورودی رسید",
            NameEn = "Receipt Field Definition",
            Code = "0025",
            Ordering = 151,
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
            Key = EntityTypes.ReceiptFieldValue,
            NameFa = "مقدار ورودی رسید",
            NameEn = "Receipt Field Value",
            Code = "0026",
            Ordering = 152,
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
            Key = EntityTypes.ReceiptLine,
            NameFa = "ردیف‌ رسید",
            NameEn = "Receipt Line",
            Code = "0027",
            Ordering = 153,
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
            Key = EntityTypes.ReceiptLineAttributeValue,
            NameFa = "مشخصه ردیف رسید",
            NameEn = "Receipt Line Attribute Value",
            Code = "0028",
            Ordering = 154,
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
            Key = EntityTypes.ReceiptLineAttributeValue,
            NameFa = "مقدار مشخصه ردیف رسید",
            NameEn = "Receipt Line Attribute Value",
            Code = "0029",
            Ordering = 155,
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
            Key = EntityTypes.ReceiptLineMeasurementValue,
            NameFa = "مقدار واحد اندازه گیری ردیف رسید",
            NameEn = "Receipt Line Measurement Value",
            Code = "0030",
            Ordering = 156,
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
            Key = EntityTypes.ReceiptSourceOfSupply,
            NameFa = "منبع تامین رسید",
            NameEn = "Receipt Source of Supply",
            Code = "0031",
            Ordering = 157,
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
            Key = EntityTypes.ReceiptType,
            NameFa = "نوع رسید",
            NameEn = "Receipt Type",
            Code = "0032",
            Ordering = 158,
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
            Key = EntityTypes.ReceiptTypeConfiguration,
            NameFa = "پیکربندی نوع رسید",
            NameEn = "Receipt Type Configuration",
            Code = "0033",
            Ordering = 159,
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
            Key = EntityTypes.ReceiptTypeFieldConfiguration,
            NameFa = "پیکربندی ورودی نوع رسید",
            NameEn = "Receipt Type Field Configuration",
            Code = "0034",
            Ordering = 160,
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
            Key = EntityTypes.RemittanceType,
            NameFa = "نوع حواله",
            NameEn = "Remittance Type",
            Code = "0035",
            Ordering = 170,
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
            Key = EntityTypes.Remittance,
            NameFa = "حواله انبار",
            NameEn = "Remittance",
            Code = "0035",
            Ordering = 171,
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
            Key = EntityTypes.Warehouse,
            NameFa = "انبار",
            NameEn = "Warehouse",
            Code = "0003",
            Ordering = 21,
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
            Key = EntityTypes.WarehouseLocation,
            NameFa = "محل انبار",
            NameEn = "Warehouse Location",
            Code = "0004",
            Ordering = 22,
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
