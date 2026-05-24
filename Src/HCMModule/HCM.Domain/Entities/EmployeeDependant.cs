using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using NGErp.Base.Domain.Entities;

namespace NGErp.HCM.Domain.Entities;

public class EmployeeDependant :
    BaseEntity,
    IBaseEntityTypeConfiguration<EmployeeDependant>
{
    public Guid EmployeeRelativeId { get; set; }
    public DateTime FromDate { get; set; }
    public DateTime? ToDate { get; set; }
    public EmployeeRelative EmployeeRelative { get; set; } = default!;
    public void Map(EntityTypeBuilder<EmployeeDependant> builder)
    {
        builder
            .ToTable(nameof(EmployeeDependant), "HCM");

        builder
            .HasIndex(e => e.EmployeeRelativeId)
            .HasDatabaseName("IX_EmployeeDependant_EmployeeRelative");

        builder
            .Property(e => e.EmployeeRelativeId)
            .IsRequired();
        builder
            .Property(e => e.FromDate)
            .IsRequired();

        builder
            .HasOne(e => e.EmployeeRelative)
            .WithMany()
            .HasForeignKey(e => e.EmployeeRelativeId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
