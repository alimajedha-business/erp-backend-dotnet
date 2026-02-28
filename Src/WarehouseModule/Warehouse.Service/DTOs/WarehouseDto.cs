namespace NGErp.Warehouse.Service.DTOs;

public record WarehouseDto(
    Guid Id,
    string Code,
    string Title,
    bool IsActive,
    decimal MaxRialValue
);

public record WarehouseListDto(
    Guid Id,
    string Code,
    string Title,
    bool IsActive,
    decimal MaxRialValue,
    string WarehouseTypeTitle,
    string CompanyUnitTitle
);

public class CreateWarehouseDto
{
    public required string Code { get; set; } = default!;
    public required string Title { get; set; } = default!;
    public bool IsActive { get; set; } = false;
    public decimal MaxRialValue { get; set; }
    public required Guid TypeId { get; set; }
    public required Guid CompanyUnitId { get; set; }

    #region Warehouse
    public Guid? WarehouseSlaveAccountCompanyId { get; set; }
    public string? WarehouseAccountMasterValue { get; private set; }
    public string? WarehouseAccountSlaveValue { get; private set; }
    public string? WarehouseAccountDetailed1Value { get; private set; }
    public string? WarehouseAccountDetailed2Value { get; private set; }
    #endregion

    #region Sale
    public Guid? SaleSlaveAccountCompanyId { get; private set; }
    public string? SaleAccountMasterValue { get; private set; }
    public string? SaleAccountSlaveValue { get; private set; }
    public string? SaleAccountDetailed1Value { get; private set; }
    public string? SaleAccountDetailed2Value { get; private set; }
    #endregion

    #region ExportSale
    public Guid? ExportSaleSlaveAccountCompanyId { get; private set; }
    public string? ExportSaleAccountMasterValue { get; private set; }
    public string? ExportSaleAccountSlaveValue { get; private set; }
    public string? ExportSaleAccountDetailed1Value { get; private set; }
    public string? ExportSaleAccountDetailed2Value { get; private set; }
    #endregion

    #region ReturnFromSale
    public Guid? ReturnFromSaleSlaveAccountCompanyId { get; private set; }
    public string? ReturnFromSaleAccountMasterValue { get; private set; }
    public string? ReturnFromSaleAccountSlaveValue { get; private set; }
    public string? ReturnFromSaleAccountDetailed1Value { get; private set; }
    public string? ReturnFromSaleAccountDetailed2Value { get; private set; }
    #endregion

    #region ReturnFromPurchase
    public Guid? ReturnFromPurchaseSlaveAccountCompanyId { get; private set; }
    public string? ReturnFromPurchaseAccountMasterValue { get; private set; }
    public string? ReturnFromPurchaseAccountSlaveValue { get; private set; }
    public string? ReturnFromPurchaseAccountDetailed1Value { get; private set; }
    public string? ReturnFromPurchaseAccountDetailed2Value { get; private set; }
    #endregion
}

public class PatchWarehouseDto
{
    public string? Code { get; set; }
    public string? Title { get; set; }
    public bool? IsActive { get; set; }
    public decimal? MaxRialValue { get; set; }
}
