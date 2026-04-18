namespace NGErp.Base.Service.Repository.Contracts;

public interface ISpecification<T>
{
    Func<IQueryable<T>, IQueryable<T>> Query { get; }
}
