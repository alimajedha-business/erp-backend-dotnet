namespace NGErp.Base.Service.ResponseModels;

public record ListQueryResult<T>(IReadOnlyList<T> items, int count);
