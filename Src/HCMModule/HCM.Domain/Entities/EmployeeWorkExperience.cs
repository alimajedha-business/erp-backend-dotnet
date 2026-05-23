using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NGErp.Base.Domain.Entities;

namespace NGErp.HCM.Domain.Entities;

public class EmployeeWorkExperience :
    BaseEntity,
    IBaseEntityTypeConfiguration<EmployeeWorkExperience>
{
    public Guid EmployeeId { get; set; }
    public string CompanyName { get; set; } = default!;
    public string Position { get; set; } = default!;
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public Employee Employee { get; set; } = default!;
    public void Map(EntityTypeBuilder<EmployeeWorkExperience> builder)
    {
        builder
            .ToTable(nameof(EmployeeWorkExperience), "HCM");

        builder
            .HasIndex(e => e.EmployeeId)
            .HasDatabaseName("IX_EmployeeWorkExperience_Employee");

        builder
            .Property(e => e.EmployeeId)
            .IsRequired();
        builder
            .Property(e => e.CompanyName)
            .IsRequired()
            .HasMaxLength(200);
        builder
            .Property(e => e.Position)
            .IsRequired()
            .HasMaxLength(150);
        builder
            .Property(e => e.StartDate)
            .IsRequired();

        builder
            .HasOne(e => e.Employee)
            .WithMany()
            .HasForeignKey(e => e.EmployeeId)
            .OnDelete(DeleteBehavior.NoAction);
    }

}
