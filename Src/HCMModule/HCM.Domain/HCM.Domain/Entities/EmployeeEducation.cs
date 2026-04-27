using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using NGErp.Base.Domain.Entities;

namespace NGErp.HCM.Domain.Entities;

public class EmployeeEducation : BaseEntity, IBaseEntityTypeConfiguration<EmployeeEducation>
{
    public Guid EmployeeId { get; set; }
    public string? LevelCode { get; set; }
    public string? FieldCode { get; set; }
    public string? MajoringCode { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public decimal? GPA { get; set; }
    public string? CenterCode { get; set; }
    public DateTime EffectiveDate { get; set; }
    public Employee Employee { get; set; } = default!;
    public void Map(EntityTypeBuilder<EmployeeEducation> builder)
    {
        builder
            .ToTable(nameof(EmployeeEducation), "HCM");

        builder
            .HasIndex(e => e.EmployeeId)
            .HasDatabaseName("IX_EmployeeEducation_Employee");

        builder
            .Property(e => e.EmployeeId)
            .IsRequired();
        builder
            .Property(e => e.LevelCode)
            .HasMaxLength(50);
        builder
            .Property(e => e.FieldCode)
            .HasMaxLength(50);
        builder
            .Property(e => e.MajoringCode)
            .HasMaxLength(50);
        builder
            .Property(e => e.StartDate)
            .IsRequired();
        builder
            .Property(e => e.GPA)
            .HasPrecision(5, 2);
        builder
            .Property(e => e.CenterCode)
            .HasMaxLength(50);
        builder
            .Property(e => e.EffectiveDate)
            .IsRequired();

        builder
            .HasOne(e => e.Employee)
            .WithMany()
            .HasForeignKey(e => e.EmployeeId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
