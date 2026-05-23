using NGErp.Base.Domain.Constants;
using NGErp.Base.Domain.EntityTypeRegistration;
using NGErp.Warehouse.Domain.Constants;

namespace NGErp.Warehouse.Service.MenuRegistrations;

public class WarehouseMenuProfile : IMenuModuleProfile
{
    public long ModuleId => ModuleIds.Warehouse;
    public bool DeleteStale => true;

    public IEnumerable<MenuDefinition> GetMenus()
    {
        yield return new MenuDefinition
        {
            NameFa = "اطلاعات پایه",
            NameEn = "Basic Information",
            Order = 1,
            Children = [
                new()
                {
                    NameFa = "انبارها",
                    NameEn = "Warehouses",
                    Order = 1,
                    EntityTypeKey = EntityTypes.Warehouse,
                    Link = "/warehouse/basic-information/warehouses"
                },
                new()
                {
                    NameFa = "مشخصه‌های شناور",
                    NameEn = "Attributes",
                    Order = 3,
                    EntityTypeKey = EntityTypes.Attribute,
                    Link = "/warehouse/settings/attributes"
                },
                new()
                {
                    NameFa = "کالاها",
                    NameEn = "Items",
                    Order = 3,
                    EntityTypeKey = EntityTypes.Item,
                    Link = "/warehouse/basic-information/items"
                },
            ]
        };

        yield return new MenuDefinition
        {
            NameFa = "امور جاری",
            NameEn = "Current Affairs",
            Order = 3,
            Children = [
                new()
                {
                    NameFa = "رسید انبار",
                    NameEn = "Warehouse Receipt",
                    Order = 1,
                    EntityTypeKey = EntityTypes.Receipt,
                    Link = "/warehouse/current-affairs/receipts"
                },
                new()
                {
                    NameFa = "حواله انبار",
                    NameEn = "Warehouse Remittance",
                    Order = 2,
                    EntityTypeKey = EntityTypes.Remittance,
                    Link = "/warehouse/current-affairs/remittances"
                }
            ]
        };

        yield return new MenuDefinition
        {
            NameFa = "اسناد انبار",
            NameEn = "Warehouse Documents",
            Order = 4,
            Children = [
                new()
                {
                    NameFa = "انواع رسید",
                    NameEn = "Receipt Types",
                    Order = 1,
                    EntityTypeKey = EntityTypes.ReceiptType,
                    Link = "/warehouse/documents/receipt-types"
                },
                new()
                {
                    NameFa = "انواع حواله",
                    NameEn = "Remittance Types",
                    Order = 2,
                    EntityTypeKey = EntityTypes.RemittanceType,
                    Link = "/warehouse/documents/remittance-types"
                },
                new()
                {
                    NameFa = "انواع فعالیت‌ها",
                    NameEn = "Inventory Movement Types",
                    Order = 3,
                    EntityTypeKey = EntityTypes.InventoryMovementTypes,
                    Link = "/warehouse/documents/inventory-movement-types"
                }
            ]
        };

        yield return new MenuDefinition
        {
            NameFa = "امکانات",
            NameEn = "Faciliteis",
            Order = 5,
            Children = [
                new()
                {
                    NameFa = "تنظیم ویژگی‌ها",
                    NameEn = "Feature Settings",
                    Order = 1,
                    EntityTypeKey = EntityTypes.FeatureSettings,
                    Link = "/warehouse/facilities/feature-settings"
                },
            ]
        };
    }
}
