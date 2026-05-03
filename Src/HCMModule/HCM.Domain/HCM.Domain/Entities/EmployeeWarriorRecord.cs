using System.ComponentModel.DataAnnotations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using NGErp.Base.Domain.Entities;

namespace NGErp.HCM.Domain.Entities;
public enum VeteranServiceType
{
    VeteranStatus = 1,      
    WarDisability = 2       
}

public class EmployeeWarriorRecord :
    BaseEntity,
    IBaseEntityTypeConfiguration<EmployeeWarriorRecord>
{
    public Guid EmployeeId { get; set; }
    public string Code { get; set; } = default!;
    public decimal? DisabilityPercentage { get; set; }
    public string? IncentiveGroup { get; set; }
    public decimal? Score { get; set; }
    public DateTime EffectiveDate { get; set; }
    [EnumDataType(typeof(VeteranServiceType))]
    public int VeteranServiceType { get; set; }
    public string? Description { get; set; }
    public Employee Employee { get; set; } = default!;
    public void Map(EntityTypeBuilder<EmployeeWarriorRecord> builder)
    {
        builder
            .ToTable(nameof(EmployeeWarriorRecord), "HCM");

        builder
            .HasIndex(e => e.EmployeeId)
            .HasDatabaseName("IX_EmployeeWarriorRecord_Employee");

        builder
            .Property(e => e.EmployeeId)
            .IsRequired();
        builder
            .Property(e => e.Code)
            .IsRequired()
            .HasMaxLength(50);
        builder
            .Property(e => e.DisabilityPercentage)
            .HasPrecision(5, 2);
        builder
            .Property(e => e.IncentiveGroup)
            .HasMaxLength(50);
        builder
            .Property(e => e.Score)
            .HasPrecision(5, 2);
        builder
            .Property(e => e.EffectiveDate)
            .IsRequired();
        builder
            .Property(e => e.VeteranServiceType)
            .IsRequired();
        builder
            .Property(e => e.Description)
            .HasMaxLength(500);

        builder
            .HasOne(e => e.Employee)
            .WithMany()
            .HasForeignKey(e => e.EmployeeId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
