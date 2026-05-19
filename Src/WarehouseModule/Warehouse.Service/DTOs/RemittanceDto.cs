using NGErp.Warehouse.Domain.Entities;

namespace NGErp.Warehouse.Service.DTOs;

public record RemittanceDto(
    Guid Id,
    long Number,
    DateOnly RemittanceDate,
    Guid RemittanceTypeId,
    RemittanceStatus Status,
    string? Description,
    IReadOnlyList<RemittanceLineDto> RemittanceLines,
    IReadOnlyList<RemittanceFieldValueDto> RemittanceFieldValues
);

public record RemittanceListDto(
    Guid Id,
    long Number,
    DateOnly RemittanceDate,
    Guid RemittanceTypeId,
    string RemittanceTypeTitle,
    RemittanceStatus Status,
    string? Description,
    int RemittanceLineCount
);

public class CreateRemittanceDto
{
    public long Number { get; set; }
    public DateOnly RemittanceDate { get; set; }
    public Guid RemittanceTypeId { get; set; }
    public string? Description { get; set; }

    public List<CreateRemittanceFieldValueDto> RemittanceFieldValues { get; set; } = [];
    public List<CreateRemittanceLineDto> RemittanceLines { get; set; } = [];
}

public class PatchRemittanceDto
{
    public long? Number { get; set; }
    public DateOnly? RemittanceDate { get; set; }
    public Guid? RemittanceTypeId { get; set; }
    public string? Description { get; set; }

    public List<CreateRemittanceFieldValueDto>? RemittanceFieldValues { get; set; }
    public List<CreateRemittanceLineDto>? RemittanceLines { get; set; }
}
