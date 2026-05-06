namespace NGErp.Warehouse.Service.DTOs;

public record RemittanceTypeDto(
    Guid Id,
    int Code,
    string Title,
    bool AddToStock
);

public record RemittanceTypeSlimDto(
    Guid Id,
    int Code,
    string Title
);

public class CreateRemittanceTypeDto
{
    public int Code { get; set; }
    public string? Title { get; set; }
    public bool? AddToStock { get; set; }
}

public class PatchRemittanceTypeDto
{
    public int? Code { get; set; }
    public string? Title { get; set; }
    public bool? AddToStock { get; set; }
}
