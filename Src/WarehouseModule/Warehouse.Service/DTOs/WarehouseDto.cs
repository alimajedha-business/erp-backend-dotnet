using NGErp.General.Service.DTOs;

namespace NGErp.Warehouse.Service.DTOs;

public record WarehouseDto(
    Guid Id,
    int Code,
    string Title,
    bool IsActive,
    decimal MaxMonetaryValue,
    WarehouseTypeDto warehouseType,
    CompanyUnitDto companyUnit,

    Guid? WarehouseSlaveAccountCompanyId,
    string WarehouseAccountMasterValue,
    string WarehouseAccountSlaveValue,
    string WarehouseAccountDetailed1Value,
    string WarehouseAccountDetailed2Value,

    Guid? ReturnFromPurchaseSlaveAccountCompanyId,
    string ReturnFromPurchaseAccountMasterValue,
    string ReturnFromPurchaseAccountSlaveValue,
    string ReturnFromPurchaseAccountDetailed1Value,
    string ReturnFromPurchaseAccountDetailed2Value
);

public record WarehouseSlimDto(
    Guid Id,
    int Code,
    string Title
);

public record WarehouseListDto(
    Guid Id,
    int Code,
    string Title,
    bool IsActive,
    decimal MaxMonetaryValue,
    string WarehouseTypeTitle,
    string CompanyUnitTitle
);

public class CreateWarehouseDto
{
    public required int Code { get; set; }
    public required string Title { get; set; }
    public bool IsActive { get; set; } = true;
    public decimal MaxMonetaryValue { get; set; }
    public Guid WarehouseTypeId { get; set; }
    public Guid CompanyUnitId { get; set; }

    #region Warehouse
    public Guid? WarehouseSlaveAccountCompanyId { get; set; }
    public string? WarehouseAccountMasterValue { get; set; }
    public string? WarehouseAccountSlaveValue { get; set; }
    public string? WarehouseAccountDetailed1Value { get; set; }
    public string? WarehouseAccountDetailed2Value { get; set; }
    #endregion

    #region ReturnFromPurchase
    public Guid? ReturnFromPurchaseSlaveAccountCompanyId { get; set; }
    public string? ReturnFromPurchaseAccountMasterValue { get; set; }
    public string? ReturnFromPurchaseAccountSlaveValue { get; set; }
    public string? ReturnFromPurchaseAccountDetailed1Value { get; set; }
    public string? ReturnFromPurchaseAccountDetailed2Value { get; set; }
    #endregion
}

public class PatchWarehouseDto
{
    public int? Code { get; set; }
    public string? Title { get; set; }
    public decimal? MaxMonetaryValue { get; set; }
    public Guid? WarehouseTypeId { get; set; }
    public Guid? CompanyUnitId { get; set; }
    public bool? IsActive { get; set; }

    #region Warehouse
    public Guid? WarehouseSlaveAccountCompanyId { get; set; }
    public string? WarehouseAccountMasterValue { get; set; }
    public string? WarehouseAccountSlaveValue { get; set; }
    public string? WarehouseAccountDetailed1Value { get; set; }
    public string? WarehouseAccountDetailed2Value { get; set; }
    #endregion

    #region ReturnFromPurchase
    public Guid? ReturnFromPurchaseSlaveAccountCompanyId { get; set; }
    public string? ReturnFromPurchaseAccountMasterValue { get; set; }
    public string? ReturnFromPurchaseAccountSlaveValue { get; set; }
    public string? ReturnFromPurchaseAccountDetailed1Value { get; set; }
    public string? ReturnFromPurchaseAccountDetailed2Value { get; set; }
    #endregion
}
