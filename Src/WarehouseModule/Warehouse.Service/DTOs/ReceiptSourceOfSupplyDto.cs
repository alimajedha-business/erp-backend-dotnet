namespace NGErp.Warehouse.Service.DTOs;

public record ReceiptSourceOfSupplyDto(
    Guid Id,
    int Code,
    string Title,
    bool IsActive
);

public record ReceiptSourceOfSupplySlimDto(
    Guid Id,
    int Code,
    string Title
);

public class CreateReceiptSourceOfSupplyDto
{
    public int? Code { get; set; }
    public string Title { get; set; } = default!;
    public bool? IsActive { get; set; }
}

public class PatchReceiptSourceOfSupplyDto
{
    public int? Code { get; set; }
    public string? Title { get; set; }
    public bool? IsActive { get; set; }
}
