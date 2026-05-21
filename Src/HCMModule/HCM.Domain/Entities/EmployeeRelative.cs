using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using NGErp.Base.Domain.Entities;

namespace NGErp.HCM.Domain.Entities;

public class EmployeeRelative :
    BaseEntity,
    IBaseEntityTypeConfiguration<EmployeeRelative>
{
    public Guid EmployeeId { get; set; }
    public string Name { get; set; } = default!;
    public string Family { get; set; } = default!;
    public string? FatherName { get; set; }
    public DateTime? BirthDate { get; set; }
    public string? BirthPlace { get; set; }
    public Guid? MaritalStatusId { get; set; }
    public string? NationalCode { get; set; }
    public string? IdNumber { get; set; }
    public Guid RelativeTypeId { get; set; }
    public string? LevelCode { get; set; }
    public string? PhysicalCondition { get; set; }
    public Guid? EducationalStatusId { get; set; }
    public Employee Employee { get; set; } = default!;
    public MaritalStatus? MaritalStatus { get; set; }
    public RelativeType RelativeType { get; set; } = default!;
    public EducationalStatus? EducationalStatus { get; set; }
    public void Map(EntityTypeBuilder<EmployeeRelative> builder)
    {
        builder
            .ToTable(nameof(EmployeeRelative), "HCM");

        builder
            .HasIndex(e => e.EmployeeId)
            .HasDatabaseName("IX_EmployeeRelative_Employee");
        builder
            .HasIndex(e => e.MaritalStatusId)
            .HasDatabaseName("IX_EmployeeRelative_MaritalStatus");
        builder
            .HasIndex(e => e.RelativeTypeId)
            .HasDatabaseName("IX_EmployeeRelative_RelativeType");
        builder
            .HasIndex(e => e.EducationalStatusId)
            .HasDatabaseName("IX_EmployeeRelative_EducationalStatus");

        builder
            .Property(e => e.EmployeeId)
            .IsRequired();
        builder
            .Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(100);
        builder
            .Property(e => e.Family)
            .IsRequired()
            .HasMaxLength(100);
        builder
            .Property(e => e.FatherName)
            .HasMaxLength(100);
        builder
            .Property(e => e.BirthPlace)
            .HasMaxLength(100);
        builder
            .Property(e => e.NationalCode)
            .HasMaxLength(10);
        builder
            .Property(e => e.IdNumber)
            .HasMaxLength(50);
        builder
            .Property(e => e.RelativeTypeId)
            .IsRequired();
        builder
            .Property(e => e.LevelCode)
            .HasMaxLength(50);
        builder
            .Property(e => e.PhysicalCondition)
            .HasMaxLength(100);

        builder
            .HasOne(e => e.Employee)
            .WithMany()
            .HasForeignKey(e => e.EmployeeId)
            .OnDelete(DeleteBehavior.NoAction);
        builder
            .HasOne(e => e.MaritalStatus)
            .WithMany()
            .HasForeignKey(e => e.MaritalStatusId)
            .OnDelete(DeleteBehavior.NoAction);
        builder
            .HasOne(e => e.RelativeType)
            .WithMany()
            .HasForeignKey(e => e.RelativeTypeId)
            .OnDelete(DeleteBehavior.NoAction);
        builder
            .HasOne(e => e.EducationalStatus)
            .WithMany()
            .HasForeignKey(e => e.EducationalStatusId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
