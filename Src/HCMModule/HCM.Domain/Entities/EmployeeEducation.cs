using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using NGErp.Base.Domain.Entities;

namespace NGErp.HCM.Domain.Entities;

public class EmployeeEducation : BaseEntity, IBaseEntityTypeConfiguration<EmployeeEducation>
{
    public Guid EmployeeId { get; set; }
    public Guid? EducationLevelId { get; set; }
    public Guid? EducationFieldId { get; set; }
    public string? MajoringCode { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public decimal? GPA { get; set; }
    public string? CenterCode { get; set; }
    public DateTime EffectiveDate { get; set; }
    public bool IsDefault { get; set; }
    public Employee Employee { get; set; } = default!;
    public EducationLevel? EducationLevel { get; set; }
    public EducationField? EducationField { get; set; }

    public void Map(EntityTypeBuilder<EmployeeEducation> builder)
    {
        builder
            .ToTable(nameof(EmployeeEducation), "HCM");

        builder
            .HasIndex(e => e.EmployeeId)
            .HasDatabaseName("IX_EmployeeEducation_Employee");

        builder
            .HasIndex(e => e.EducationLevelId)
            .HasDatabaseName("IX_EmployeeEducation_EducationLevel");

        builder
            .HasIndex(e => e.EducationFieldId)
            .HasDatabaseName("IX_EmployeeEducation_EducationField");

        builder
            .Property(e => e.EmployeeId)
            .IsRequired();
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

        builder
            .HasOne(e => e.EducationLevel)
            .WithMany()
            .HasForeignKey(e => e.EducationLevelId)
            .OnDelete(DeleteBehavior.NoAction);

        builder
            .HasOne(e => e.EducationField)
            .WithMany()
            .HasForeignKey(e => e.EducationFieldId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
