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

    Guid? SaleSlaveAccountCompanyId,
    string SaleAccountMasterValue,
    string SaleAccountSlaveValue,
    string SaleAccountDetailed1Value,
    string SaleAccountDetailed2Value,

    Guid? ExportSaleSlaveAccountCompanyId,
    string ExportSaleAccountMasterValue,
    string ExportSaleAccountSlaveValue,
    string ExportSaleAccountDetailed1Value,
    string ExportSaleAccountDetailed2Value,

    Guid? ReturnFromSaleSlaveAccountCompanyId,
    string ReturnFromSaleAccountMasterValue,
    string ReturnFromSaleAccountSlaveValue,
    string ReturnFromSaleAccountDetailed1Value,
    string ReturnFromSaleAccountDetailed2Value,

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

    #region Sale
    public Guid? SaleSlaveAccountCompanyId { get; set; }
    public string? SaleAccountMasterValue { get; set; }
    public string? SaleAccountSlaveValue { get; set; }
    public string? SaleAccountDetailed1Value { get; set; }
    public string? SaleAccountDetailed2Value { get; set; }
    #endregion

    #region ExportSale
    public Guid? ExportSaleSlaveAccountCompanyId { get; set; }
    public string? ExportSaleAccountMasterValue { get; set; }
    public string? ExportSaleAccountSlaveValue { get; set; }
    public string? ExportSaleAccountDetailed1Value { get; set; }
    public string? ExportSaleAccountDetailed2Value { get; set; }
    #endregion

    #region ReturnFromSale
    public Guid? ReturnFromSaleSlaveAccountCompanyId { get; set; }
    public string? ReturnFromSaleAccountMasterValue { get; set; }
    public string? ReturnFromSaleAccountSlaveValue { get; set; }
    public string? ReturnFromSaleAccountDetailed1Value { get; set; }
    public string? ReturnFromSaleAccountDetailed2Value { get; set; }
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
}
