using System.Collections.Concurrent;

using Microsoft.Extensions.DependencyInjection;

namespace NGErp.Base.Domain.EntitySchemas;

public sealed class FilterSchemaProvider(IServiceProvider sp) : IFilterSchemaProvider
{
    private readonly IServiceProvider _sp = sp;
    private static readonly ConcurrentDictionary<Type, FilterSchema> Cache = new();

    public FilterSchema GetSchema<T>()
    {
        return Cache.GetOrAdd(typeof(T), _ =>
        {
            var schemaBuilder = _sp.GetRequiredService<IFilterSchema<T>>();
            return schemaBuilder.Build();
        });
    }
}
