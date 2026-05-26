namespace NGErp.Accounting.Service.DTOs;

using System.Text.Json.Serialization;

using Newtonsoft.Json;

public sealed record SlaveAccountCompanyDto
{
    public Guid Id { get; init; }

    public MasterDto Master { get; init; } = default!;

    public AccountNodeDto Slave { get; init; } = default!;

    [JsonProperty("detailed_account_1")]
    [JsonPropertyName("detailed_account_1")]
    public AccountNodeDto? DetailedAccount1 { get; init; }

    [JsonProperty("detailed_account_2")]
    [JsonPropertyName("detailed_account_2")]
    public AccountNodeDto? DetailedAccount2 { get; init; }
}

public sealed record MasterDto
{
    public Guid Id { get; init; }

    public int Code { get; init; }

    public string Title { get; init; } = string.Empty;

    public string? Title2 { get; init; }
}

public sealed record AccountNodeDto
{
    public Guid Id { get; init; }

    public int Code { get; init; }

    [JsonProperty("slave_code")]
    [JsonPropertyName("slave_code")]
    public int SlaveCode { get; init; }

    public string Title { get; init; } = string.Empty;

    public string? Title2 { get; init; }

    public string Nature { get; init; } = default!;

    public CategoryDto Category { get; init; } = default!;

    public GroupDto Group { get; init; } = default!;

    public int Level { get; init; }

    [JsonProperty("last_level")]
    [JsonPropertyName("last_level")]
    public bool LastLevel { get; init; }

    [JsonProperty("parent_id")]
    [JsonPropertyName("parent_id")]
    public Guid? ParentId { get; init; }
}

public sealed record CategoryDto
{
    public int Id { get; init; }

    public string Name { get; init; } = string.Empty;
}

public sealed record GroupDto
{
    public Guid Id { get; init; }

    public string Name { get; init; } = string.Empty;

    public int Code { get; init; }
}
