namespace NGErp.Warehouse.Service.DTOs;

public record ReceiptTypeDto(
    Guid Id,
    int Code,
    string Title,
    bool AddToStock
);

public record ReceiptTypeSlimDto(
    Guid Id,
    int Code,
    string Title
);

public class CreateReceiptTypeDto
{
    public int Code { get; set; }
    public string? Title { get; set; }
    public bool? AddToStock { get; set; }
}

public class PatchReceiptTypeDto
{
    public int? Code { get; set; }
    public string? Title { get; set; }
    public bool? AddToStock { get; set; }
}
