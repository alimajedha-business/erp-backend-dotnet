using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using NGErp.Base.Domain.Entities;
using NGErp.General.Domain.Entities;

namespace NGErp.HCM.Domain.Entities;

public class Job :
    BaseEntityWithCompany,
    IBaseEntityTypeConfiguration<Job>
{
    public string Code { get; set; } = default!;
    public Guid? ParentId { get; set; }
    public Guid? NextJobId { get; set; }
    public string Title { get; set; } = default!;
    public Guid JobCategoryId { get; set; }
    public string? Description { get; set; }
    public int LevelCode { get; set; }
    public bool? Seniority { get; set; }
    public Job? Parent { get; set; }
    public Job? NextJob { get; set; }
    public JobCategory JobCategory { get; set; } = default!;
    public void Map(EntityTypeBuilder<Job> builder)
    {
        builder
            .ToTable(nameof(Job), "HCM");

        builder
            .HasIndex(e => new { e.CompanyId, e.Code })
            .IsUnique()
            .HasDatabaseName("UX_Job_CompanyId_Code");
        builder
            .HasIndex(e => e.ParentId)
            .HasDatabaseName("IX_Job_Parent");
        builder
            .HasIndex(e => e.NextJobId)
            .HasDatabaseName("IX_Job_NextJob");
        builder
            .HasIndex(e => e.JobCategoryId)
            .HasDatabaseName("IX_Job_JobCategory");

        builder
            .Property(e => e.Code)
            .IsRequired()
            .HasMaxLength(50);
        builder
            .Property(e => e.Title)
            .IsRequired()
            .HasMaxLength(500);
        builder
            .Property(e => e.JobCategoryId)
            .IsRequired();
        builder
            .Property(e => e.Description)
            .HasMaxLength(1000);
        builder
            .Property(e => e.LevelCode)
            .IsRequired();
        builder
            .Property(e => e.Seniority);

        builder
            .HasOne(e => e.Parent)
            .WithMany()
            .HasForeignKey(e => e.ParentId)
            .OnDelete(DeleteBehavior.NoAction);
        builder
            .HasOne(e => e.NextJob)
            .WithMany()
            .HasForeignKey(e => e.NextJobId)
            .OnDelete(DeleteBehavior.NoAction);
        builder
            .HasOne(e => e.JobCategory)
            .WithMany()
            .HasForeignKey(e => e.JobCategoryId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
