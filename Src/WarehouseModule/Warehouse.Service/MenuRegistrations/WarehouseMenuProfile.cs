using NGErp.Base.Domain.Constants;
using NGErp.Base.Domain.EntityTypeRegistration;
using NGErp.Warehouse.Domain.Constants;

namespace NGErp.Warehouse.Service.MenuRegistrations;

public class WarehouseMenuProfile : IMenuModuleProfile
{
    public long ModuleId => ModuleIds.Warehouse;
    public bool DeleteStale => false;

    public IEnumerable<MenuDefinition> GetMenus()
    {
        yield return new MenuDefinition
        {
            NameFa = "اطلاعات پایه",
            NameEn = "Basic Information",
            Order = 1,
            Children = new List<MenuDefinition>
            {
                new() { NameFa = "انبارها", NameEn = "Warehouses", Order = 1, EntityTypeKey = EntityTypes.Warehouse, Link = "/warehouse/basic-information/warehouses" },
                new() { NameFa = "انواع انبار", NameEn = "Warehouse Types", Order = 2, EntityTypeKey = EntityTypes.WarehouseType, Link = "/warehouse/basic-information/warehouse-types" },
                new() { NameFa = "محل‌های انبار", NameEn = "Warehouse Locations", Order = 3, EntityTypeKey = EntityTypes.WarehouseLocation, Link = "/warehouse/basic-information/warehouse-locations" },
                new() { NameFa = "کالاها", NameEn = "Items", Order = 4, EntityTypeKey = EntityTypes.Item, Link = "/warehouse/basic-information/items" },
                new() { NameFa = "انواع کالا", NameEn = "Item Types", Order = 5, EntityTypeKey = EntityTypes.ItemType, Link = "/warehouse/basic-information/item-types" },
                new() { NameFa = "دسته‌بندی کالاها", NameEn = "Categories", Order = 6, EntityTypeKey = EntityTypes.Category, Link = "/warehouse/basic-information/categories" },
                new() { NameFa = "واحدهای اندازه‌گیری", NameEn = "Units of Measurement", Order = 7, EntityTypeKey = EntityTypes.UnitOfMeasurement, Link = "/warehouse/basic-information/uoms" },
                new() { NameFa = "ابعاد اندازه‌گیری", NameEn = "Measurement Dimensions", Order = 8, EntityTypeKey = EntityTypes.MeasurementDimension, Link = "/warehouse/basic-information/dimensions" },
                new() { NameFa = "شرکت‌های حمل و نقل", NameEn = "Shipping Companies", Order = 9, EntityTypeKey = EntityTypes.ShippingCompany, Link = "/warehouse/basic-information/shipping-companies" },
            }
        };

        yield return new MenuDefinition
        {
            NameFa = "مدیریت موجودی",
            NameEn = "Inventory Management",
            Order = 2,
            Children = new List<MenuDefinition>
            {
                new() { NameFa = "حرکات موجودی", NameEn = "Inventory Movements", Order = 1, EntityTypeKey = EntityTypes.InventoryMovement, Link = "/warehouse/inventory-management/movements" },
                new() { NameFa = "انواع حرکات موجودی", NameEn = "Movement Types", Order = 2, EntityTypeKey = EntityTypes.InventoryMovementType, Link = "/warehouse/inventory-management/movement-types" },
                new() { NameFa = "بچ‌های موجودی", NameEn = "Inventory Lots", Order = 3, EntityTypeKey = EntityTypes.InventoryLot, Link = "/warehouse/inventory-management/lots" },
                new() { NameFa = "ویژگی‌های بچ موجودی", NameEn = "Lot Attribute Values", Order = 4, EntityTypeKey = EntityTypes.InventoryLotAttributeValue, Link = "/warehouse/inventory-management/lot-attributes" },
            }
        };

        yield return new MenuDefinition
        {
            NameFa = "امور جاری",
            NameEn = "Current Affairs",
            Order = 3,
            Children = new List<MenuDefinition>
            {
                new() { NameFa = "رسید انبار", NameEn = "Warehouse Receipt", Order = 1, EntityTypeKey = EntityTypes.WarehouseReceipt, Link = "/warehouse/current-affairs/receipts" },
                new() { NameFa = "حواله انبار", NameEn = "Warehouse Remittance", Order = 2, EntityTypeKey = EntityTypes.WarehouseRemittance, Link = "/warehouse/current-affairs/remittances" }
            }
        };

        yield return new MenuDefinition
        {
            NameFa = "اسناد انبار",
            NameEn = "Warehouse Documents",
            Order = 4,
            Children = new List<MenuDefinition>
            {
                new() { NameFa = "انواع رسید", NameEn = "Receipt Types", Order = 1, EntityTypeKey = EntityTypes.ReceiptType, Link = "/warehouse/documents/receipt-types" },
                new() { NameFa = "انواع حواله", NameEn = "Remittance Types", Order = 2, EntityTypeKey = EntityTypes.RemittanceType, Link = "/warehouse/documents/remittance-types" },
            }
        };

        yield return new MenuDefinition
        {
            NameFa = "تنظیمات و ویژگی‌ها",
            NameEn = "Settings & Attributes",
            Order = 5,
            Children = new List<MenuDefinition>
            {
                new() { NameFa = "ویژگی‌های کالا", NameEn = "Item Attributes", Order = 1, EntityTypeKey = EntityTypes.ItemAttribute, Link = "/warehouse/settings/item-attributes" },
                new() { NameFa = "واحدهای اندازه‌گیری کالا", NameEn = "Item UoMs", Order = 2, EntityTypeKey = EntityTypes.ItemUnitOfMeasurement, Link = "/warehouse/settings/item-uoms" },
                new() { NameFa = "تبدیل واحدهای کالا", NameEn = "Item Unit Conversions", Order = 3, EntityTypeKey = EntityTypes.ItemUnitOfMeasurementConversion, Link = "/warehouse/settings/item-unit-conversions" },
                new() { NameFa = "کالاهای انبار", NameEn = "Item Warehouses", Order = 4, EntityTypeKey = EntityTypes.ItemWarehouse, Link = "/warehouse/settings/item-warehouses" },
                new() { NameFa = "محل کالاهای انبار", NameEn = "Item Warehouse Locations", Order = 5, EntityTypeKey = EntityTypes.ItemWarehouseLocation, Link = "/warehouse/settings/item-warehouse-locations" },
                new() { NameFa = "ویژگی‌ها", NameEn = "Attributes", Order = 6, EntityTypeKey = EntityTypes.Attribute, Link = "/warehouse/settings/attributes" },
                new() { NameFa = "مقادیر شمارشی ویژگی‌ها", NameEn = "Attribute Enum Values", Order = 7, EntityTypeKey = EntityTypes.AttributeEnumValue, Link = "/warehouse/settings/attribute-enums" },
                new() { NameFa = "قوانین ویژگی‌های دسته بندی", NameEn = "Category Attr Rules", Order = 8, EntityTypeKey = EntityTypes.CategoryAttributeRule, Link = "/warehouse/settings/category-attr-rules" },
                new() { NameFa = "محدودیت‌های سطوح دسته‌بندی", NameEn = "Category Level Constraints", Order = 9, EntityTypeKey = EntityTypes.CategoryLevelConstraint, Link = "/warehouse/settings/category-level-constraints" },
            }
        };
    }
}
