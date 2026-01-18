using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NGErp.Base.Domain.Entities;

namespace NGErp.General.Domain.Entities
{

    public class Company : BaseEntity, IBaseEntityTypeConfiguration<Company>
    {
        public string Name { get; private set; } = default!;


        public void Map(EntityTypeBuilder<Company> builder)
        {
            builder
                .ToTable(nameof(Company), "General");

            builder
                .Property(e => e.Name)
                .HasMaxLength(255);
        }
    }
}
