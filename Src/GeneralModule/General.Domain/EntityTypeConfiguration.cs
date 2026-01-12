using Microsoft.EntityFrameworkCore.Metadata.Builders;

using NGErp.General.Domain.Entities;

namespace NGErp.General.Domain;

public interface IBaseEntityTypeConfiguration<T> where T : BaseEntity
{
    public void Map(EntityTypeBuilder<T> builder);
}

public interface IViewModelTypeConfiguration<T> where T : class
{
    public void Map(EntityTypeBuilder<T> builder);
}
