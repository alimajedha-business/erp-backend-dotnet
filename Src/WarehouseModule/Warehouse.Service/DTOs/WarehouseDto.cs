using NGErp.Accounting.Service.DTOs;
using NGErp.General.Service.DTOs;

namespace NGErp.Warehouse.Service.DTOs;

public record WarehouseDto
{
    public Guid Id { get; init; }
    public int Code { get; init; }
    public string Title { get; init; } = default!;
    public bool IsActive { get; init; }
    public decimal MaxMonetaryValue { get; init; }
    public WarehouseTypeDto warehouseType { get; init; } = default!;
    public CompanyUnitDto companyUnit { get; init; } = default!;

    public Guid? WarehouseSlaveAccountCompanyId { get; init; }
    public SlaveAccountCompanyDto? WarehouseSlaveAccountCompany { get; set; } = default!;
    public string WarehouseAccountMasterValue { get; init; } = default!;
    public string WarehouseAccountSlaveValue { get; init; } = default!;
    public string WarehouseAccountDetailed1Value { get; init; } = default!;
    public string WarehouseAccountDetailed2Value { get; init; } = default!;

    public Guid? ReturnFromPurchaseSlaveAccountCompanyId { get; init; }
    public SlaveAccountCompanyDto? ReturnFromPurchaseSlaveAccountCompany { get; set; }
    public string ReturnFromPurchaseAccountMasterValue { get; init; } = default!;
    public string ReturnFromPurchaseAccountSlaveValue { get; init; } = default!;
    public string ReturnFromPurchaseAccountDetailed1Value { get; init; } = default!;
    public string ReturnFromPurchaseAccountDetailed2Value { get; init; } = default!;
}

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
