using NGErp.Warehouse.Domain.Entities;

namespace NGErp.Warehouse.Service.DTOs;

public record ReceiptDto(
    Guid Id,
    long Number,
    DateOnly ReceiptDate,
    Guid ReceiptTypeId,
    ReceiptStatus Status,
    string? Description,
    IReadOnlyList<ReceiptLineDto> ReceiptLines,
    IReadOnlyList<ReceiptFieldValueDto> ReceiptFieldValues
);

public record ReceiptListDto(
    Guid Id,
    long Number,
    DateOnly ReceiptDate,
    Guid ReceiptTypeId,
    string ReceiptTypeTitle,
    ReceiptStatus Status,
    string? Description,
    int ReceiptLineCount
);

public class CreateReceiptDto
{
    public long Number { get; set; } = default!;
    public DateOnly ReceiptDate { get; set; }
    public Guid ReceiptTypeId { get; set; }
    public string? Description { get; set; }

    public List<CreateReceiptFieldValueDto> ReceiptFieldValues { get; set; } = [];
    public List<CreateReceiptLineDto> ReceiptLines { get; set; } = [];
}

public class PatchReceiptDto
{
    public long? Number { get; set; }
    public DateOnly? ReceiptDate { get; set; }
    public Guid? ReceiptTypeId { get; set; }
    public string? Description { get; set; }

    public List<CreateReceiptFieldValueDto>? ReceiptFieldValues { get; set; }
    public List<CreateReceiptLineDto>? ReceiptLines { get; set; }
}
