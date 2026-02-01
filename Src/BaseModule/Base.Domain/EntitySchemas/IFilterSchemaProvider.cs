namespace NGErp.Base.Domain.EntitySchemas;

public interface IFilterSchemaProvider
{
    FilterSchema GetSchema<TEntity>();
}