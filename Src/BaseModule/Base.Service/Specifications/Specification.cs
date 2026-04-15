using NGErp.Base.Service.Repository.Contracts;

namespace NGErp.Base.Service.Specifications;

public class Specification<T>(Func<IQueryable<T>, IQueryable<T>> query)
    : ISpecification<T>
{
    public Func<IQueryable<T>, IQueryable<T>> Query { get; } = query;
}
