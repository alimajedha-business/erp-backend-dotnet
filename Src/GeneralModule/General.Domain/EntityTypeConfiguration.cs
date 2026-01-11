using Microsoft.EntityFrameworkCore.Metadata.Builders;

using NGErp.Base.Domain.Entities;

namespace NGErp.General.Domain;

internal class EntityTypeConfiguration
{
    public interface IBaseEntityTypeConfiguration<T> where T : BaseEntity
    {
        public void Map(EntityTypeBuilder<T> builder);
    }

    public interface IViewModelTypeConfiguration<T> where T : class
    {
        public void Map(EntityTypeBuilder<T> builder);
    }
}
