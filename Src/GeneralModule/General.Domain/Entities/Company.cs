using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace NGErp.General.Domain.Entities
{
    public class Company : IEntityTypeConfiguration<Company>
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; } = default!;

        public void Configure(EntityTypeBuilder<Company> builder) { }

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
