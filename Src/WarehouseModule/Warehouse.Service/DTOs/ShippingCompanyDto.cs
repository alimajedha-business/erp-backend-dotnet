namespace NGErp.Warehouse.Service.DTOs;

public record ShippingCompanyDto(
    Guid Id,
    int Code,
    string Title,
    string ManagerFirstName,
    string ManagerLastName,
    string MobileNumber,
    string PhoneNumber,
    string Address
);

public record ShippingCompanyListDto(
    Guid Id,
    int Code,
    string Title,
    string ManagerFullName,
    string MobileNumber,
    string PhoneNumber,
    string Address
);

public class CreateShippingCompanyDto
{
    public required int Code { get; set; }
    public required string Title { get; set; }
    public required string ManagerFirstName { get; set; }
    public required string ManagerLastName { get; set; }
    public required string MobileNumber { get; set; }
    public string PhoneNumber { get; set; } = default!;
    public string Address { get; set; } = default!;
}

public class PatchShippingCompanyDto
{
    public int? Code { get; set; }
    public string? Title { get; set; }
    public string? ManagerFirstName { get; set; }
    public string? ManagerLastName { get; set; }
    public string? MobileNumber { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Address { get; set; }
}
