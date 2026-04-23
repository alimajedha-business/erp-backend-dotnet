namespace NGErp.General.Service.DTOs;

public record CountryDto(
    Guid Id,
    string Name,
    int Code,
    CurrencyDto? Currency,
    int? TaxCode,
    string? Iso
);

public record CountryListDto(
    Guid Id,
    string Name,
    int Code,
    string? CurrencyTitle,
    int? TaxCode,
    string? Iso
);

public class CreateCountryDto
{
    public required string Name { get; set; }
    public int Code { get; set; }
    public Guid? CurrencyId { get; set; }
    public int? TaxCode { get; set; }
    public string? Iso { get; set; }
}

public class PatchCountryDto
{
    public string? Name { get; set; }
    public int? Code { get; set; }
    public Guid? CurrencyId { get; set; }
    public int? TaxCode { get; set; }
    public string? Iso { get; set; }
}
