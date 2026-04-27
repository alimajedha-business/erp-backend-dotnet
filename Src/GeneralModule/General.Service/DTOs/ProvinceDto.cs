namespace NGErp.General.Service.DTOs;

public record ProvinceDto(
    Guid Id,
    string Name,
    int Code,
    CountryDto Country
);

public record ProvinceListDto(
    Guid Id,
    string Name,
    int Code,
    string CountryTitle
);

public class CreateProvinceDto
{
    public required string Name { get; set; }
    public int Code { get; set; }
    public Guid CountryId { get; set; }
}

public class PatchProvinceDto
{
    public string? Name { get; set; }
    public int? Code { get; set; }
    public Guid? CountryId { get; set; }
}
