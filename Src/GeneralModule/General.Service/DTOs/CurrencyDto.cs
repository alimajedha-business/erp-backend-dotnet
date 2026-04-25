namespace NGErp.General.Service.DTOs;

public record CurrencyDto(
    Guid Id,
    string Name,
    string? Name2,
    int Code,
    string? Symbol,
    string? Iso,
    string DecimalPlaces,
    string RoundMethod,
    string? FractionalCurrencyUnit
);

public record CurrencyListDto(
    Guid Id,
    string Name,
    string? Name2,
    int Code,
    string? Symbol,
    string? Iso,
    string DecimalPlaces,
    string RoundMethod,
    string? FractionalCurrencyUnit
);

public class CreateCurrencyDto
{
    public required string Name { get; set; }
    public string? Name2 { get; set; }
    public int Code { get; set; }
    public string? Symbol { get; set; }
    public string? Iso { get; set; }
    public required string DecimalPlaces { get; set; }
    public required string RoundMethod { get; set; }
    public string? FractionalCurrencyUnit { get; set; }
}

public class PatchCurrencyDto
{
    public string? Name { get; set; }
    public string? Name2 { get; set; }
    public int? Code { get; set; }
    public string? Symbol { get; set; }
    public string? Iso { get; set; }
    public string? DecimalPlaces { get; set; }
    public string? RoundMethod { get; set; }
    public string? FractionalCurrencyUnit { get; set; }
}
