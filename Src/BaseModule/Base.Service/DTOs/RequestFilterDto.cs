namespace NGErp.Base.Service.DTOs;

public abstract record FilterNodeDto;

public record FilterGroupDto(string Op, List<FilterNodeDto> Children) : FilterNodeDto;
// Op: "and" | "or"

public record FilterConditionDto(string Field, string Operator, object? Value) : FilterNodeDto;
// Operator examples: "eq","ne","gt","ge","lt","le","startsWith","contains","endsWith"