using DocumentFormat.OpenXml.Wordprocessing;

namespace NGErp.General.Service.DTOs;


public class PersonBaseDto
{
    public Guid Id { get; set; }
    public string Typ { get; set; } = default!;
    public string Name { get; set; } = default!;
    public long Code { get; set; }

    public string? NaturalFamily { get; set; }
    public string? NaturalNationalCode { get; set; }
    public string? LegalNationalCode { get; set; }

    public string DisplayName=> $"{Name} {NaturalFamily}".Trim();
}


public record PersonDto(
    Guid Id,
    string Typ,
    string Name,
    string? NaturalFamily,
    long Code,
    string? EconomicCode,
    string? EconomicCodeOld,
    bool IsInternalCitizenship,
    string? CitizenCode,
    string? NaturalFatherName,
    string? NaturalNationalCode,
    string? PassportNumber,
    CityDto? BirthCity,
    DateTime? NaturalBirthDate,
    string? NaturalSex,
    string? LegalManagerName,
    string? LegalManagerFamily,
    string? LegalNationalCode,
    string? LegalRegisterNo,
    DateTime? LegalEstablishmentDate,
    string? IdNumber,
    bool IsGovernmental,
    ReligionDto? Religion,
    string? Photo
);


public class CreatePersonDto
{
    public required string Typ { get; set; }
    public required string Name { get; set; }
    public string? NaturalFamily { get; set; }
    public long Code { get; set; }

    public string? EconomicCode { get; set; }
    public string? EconomicCodeOld { get; set; }

    public bool IsInternalCitizenship { get; set; } = true;
    public string? CitizenCode { get; set; }

    public string? NaturalFatherName { get; set; }
    public string? NaturalNationalCode { get; set; }
    public string? PassportNumber { get; set; }
    public Guid? BirthCityId { get; set; }

    public DateTime? NaturalBirthDate { get; set; }
    public string? NaturalSex { get; set; }

    public string? LegalManagerName { get; set; }
    public string? LegalManagerFamily { get; set; }
    public string? LegalNationalCode { get; set; }
    public string? LegalRegisterNo { get; set; }
    public DateTime? LegalEstablishmentDate { get; set; }

    public string? IdNumber { get; set; }

    public bool IsGovernmental { get; set; } = false;

    public Guid? ReligionId { get; set; }

    public string? Photo { get; set; }
}

public class PatchPersonDto
{
    public string? Typ { get; set; }
    public string? Name { get; set; }
    public string? NaturalFamily { get; set; }
    public long? Code { get; set; }
    public string? EconomicCode { get; set; }
    public string? EconomicCodeOld { get; set; }

    public bool? IsInternalCitizenship { get; set; }
    public string? CitizenCode { get; set; }

    public string? NaturalFatherName { get; set; }
    public string? NaturalNationalCode { get; set; }
    public string? PassportNumber { get; set; }
    public Guid? BirthCityId { get; set; }

    public DateTime? NaturalBirthDate { get; set; }
    public string? NaturalSex { get; set; }

    public string? LegalManagerName { get; set; }
    public string? LegalManagerFamily { get; set; }
    public string? LegalNationalCode { get; set; }
    public string? LegalRegisterNo { get; set; }
    public DateTime? LegalEstablishmentDate { get; set; }

    public string? IdNumber { get; set; }

    public bool? IsGovernmental { get; set; }

    public Guid? ReligionId { get; set; }

    public string? Photo { get; set; }
}
