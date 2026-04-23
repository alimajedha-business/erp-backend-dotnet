namespace NGErp.General.Service.DTOs;

public record CityDto(
    Guid Id,
    string Name,
    int Code,
    int? Code2,
    int? Code3,
    ProvinceDto Province
);

public record CityListDto(
    Guid Id,
    string Name,
    int Code,
    int? Code2,
    int? Code3,
    string ProvinceTitle
);

public class CreateCityDto
{
    public required string Name { get; set; }
    public int Code { get; set; }
    public int? Code2 { get; set; }
    public int? Code3 { get; set; }
    public Guid ProvinceId { get; set; }
}

public class PatchCityDto
{
    public string? Name { get; set; }
    public int? Code { get; set; }
    public int? Code2 { get; set; }
    public int? Code3 { get; set; }
    public Guid? ProvinceId { get; set; }
}