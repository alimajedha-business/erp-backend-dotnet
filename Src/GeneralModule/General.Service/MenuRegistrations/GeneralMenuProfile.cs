using NGErp.Base.Domain.Constants;
using NGErp.Base.Domain.EntityTypeRegistration;
using NGErp.General.Domain.Constants;

namespace NGErp.General.Service.MenuRegistrations;

public class GeneralMenuProfile : IMenuModuleProfile
{
    public long ModuleId => ModuleIds.General;
    public bool DeleteStale => false;

    public IEnumerable<MenuDefinition> GetMenus()
    {
        yield return new MenuDefinition
        {
            NameFa = "اطلاعات پایه",
            NameEn = "Basic Information",
            Order = 1
        };

        yield return new MenuDefinition
        {
            NameFa = "حقوق و دستمزد",
            NameEn = "HCM",
            Order = 2,
            Children = [
                new()
                {
                    NameFa = "مقطع تحصیلی",
                    NameEn = "Education Level",
                    Order = 1,
                    EntityTypeKey = EntityTypes.EducationLevel,
                    Link = "/hcm/education-levels"
                },
                new()
                {
                    NameFa = "رشته تحصیلی",
                    NameEn = "Education Field",
                    Order = 2,
                    EntityTypeKey = EntityTypes.EducationField,
                    Link = "/hcm/education-fields"
                },
                new()
                {
                    NameFa = "وضعیت تحصیلی",
                    NameEn = "Educational Status",
                    Order = 3,
                    EntityTypeKey = EntityTypes.EducationalStatus,
                    Link = "/hcm/educational-statuses"
                },
                new()
                {
                    NameFa = "وضعیت تاهل",
                    NameEn = "Marital Status",
                    Order = 4,
                    EntityTypeKey = EntityTypes.MaritalStatus,
                    Link = "/hcm/marital-statuses"
                },
                new()
                {
                    NameFa = "انواع وابستگان",
                    NameEn = "Relative Types",
                    Order = 5,
                    EntityTypeKey = EntityTypes.RelativeType,
                    Link = "/hcm/relative-types"
                },
                new()
                {
                    NameFa = "وضعیت نظام وظیفه",
                    NameEn = "Military Service Status",
                    Order = 6,
                    EntityTypeKey = EntityTypes.MilitaryServiceStatus,
                    Link = "/hcm/military-service-statuses"
                },
                new()
                {
                    NameFa = "رسته شغلی",
                    NameEn = "Job Category",
                    Order = 7,
                    EntityTypeKey = EntityTypes.JobCategory,
                    Link = "/hcm/job-categories"
                }
            ]
        };

        yield return new MenuDefinition
        {
            NameFa = "انبار",
            NameEn = "Warehouse",
            Order = 3,
            Children = [
                new()
                {
                    NameFa = "انواع کالا",
                    NameEn = "Item Types",
                    Order = 1,
                    EntityTypeKey = EntityTypes.ItemType,
                    Link = "/warehouse/item-types"
                },
                new()
                {
                    NameFa = "واحدهای اندازه‌گیری",
                    NameEn = "Unit of Measurements",
                    Order = 2,
                    EntityTypeKey = EntityTypes.UnitOfMeasurement,
                    Link = "/warehouse/unit-of-measurements"
                },
                new()
                {
                    NameFa = "انواع انبار",
                    NameEn = "Warehouse Types",
                    Order = 3,
                    EntityTypeKey = EntityTypes.WarehouseType,
                    Link = "/warehouse/warehouse-types"
                },
                new()
                {
                    NameFa = "باربری‌ها",
                    NameEn = "Shipping Companies",
                    Order = 4,
                    EntityTypeKey = EntityTypes.ShippingCompany,
                    Link = "/warehouse/shipping-companies"
                },
            ]
        };
    }
}
