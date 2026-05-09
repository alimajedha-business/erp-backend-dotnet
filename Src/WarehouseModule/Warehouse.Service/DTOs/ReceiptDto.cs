namespace NGErp.Warehouse.Service.DTOs;

public record ReceiptDto(
    Guid Id,
    string Number,
    DateOnly ReceiptDate,
    Guid ReceiptTypeId,
    string? Description,
    IReadOnlyList<ReceiptLineDto> ReceiptLines,
    IReadOnlyList<ReceiptFieldValueDto> ReceiptFieldValues
);

public record ReceiptListDto(
    Guid Id,
    string Number,
    DateOnly ReceiptDate,
    Guid ReceiptTypeId,
    string ReceiptTypeTitle,
    string? Description,
    int ReceiptLineCount
);

public class CreateReceiptDto
{
    public string Number { get; set; } = default!;
    public DateOnly ReceiptDate { get; set; }
    public Guid ReceiptTypeId { get; set; }
    public string? Description { get; set; }

    public List<CreateReceiptFieldValueDto> ReceiptFieldValues { get; set; } = [];
    public List<CreateReceiptLineDto> ReceiptLines { get; set; } = [];
}

public class PatchReceiptDto
{
    public string? Number { get; set; }
    public DateOnly? ReceiptDate { get; set; }
    public Guid? ReceiptTypeId { get; set; }
    public string? Description { get; set; }

    public List<CreateReceiptFieldValueDto>? ReceiptFieldValues { get; set; }
    public List<CreateReceiptLineDto>? ReceiptLines { get; set; }
}
