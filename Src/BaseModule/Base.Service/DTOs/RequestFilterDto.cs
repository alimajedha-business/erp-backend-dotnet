using System.Text.Json.Serialization;

namespace NGErp.Base.Service.DTOs;

[JsonPolymorphic(TypeDiscriminatorPropertyName = "type")]
[JsonDerivedType(typeof(FilterGroupDto), "group")]
[JsonDerivedType(typeof(FilterConditionDto), "condition")]
public abstract record FilterNodeDto;

public record FilterGroupDto(string Op, List<FilterNodeDto> Children) : FilterNodeDto;
// Op: "and" | "or"

public record FilterConditionDto(string Field, string Operator, object? Value) : FilterNodeDto;
// Operator examples: "eq","ne","gt","ge","lt","le","startsWith","contains","endsWith"