using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using NGErp.Base.Domain.Entities;
using NGErp.General.Domain.Entities;

namespace NGErp.HCM.Domain.Entities;

public class Job :
    BaseEntityWithCompany,
    IBaseEntityTypeConfiguration<Job>
{
    public required string Code { get; set; }
    public Guid ParentId { get; set; }
    public Guid NextJobId { get; set; }
    public required string Title { get; set; }
    public Guid JobCategoryId { get; set; }
    public string? Description { get; set; }
    public int LevelCode { get; set; }
    public bool Seniority { get; set; }

    public Job? ParentJob { get; set; }
    public Job? NextJob { get; set; }
    public JobCategory JobCategory { get; set; } = default!;

    public void Map(EntityTypeBuilder<Job> builder)
    {
        builder.
            ToTable(nameof(Job), "HCM");

        builder
            .HasIndex(i => new { i.Code })
            .IsUnique()
            .HasDatabaseName("UX_Job_Code");

        builder
            .Property(e => e.Title)
            .HasMaxLength(500);

        builder.HasOne(e => e.ParentJob)
           .WithMany()
           .HasForeignKey(e => e.ParentId)
           .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(e => e.NextJob)
            .WithMany()
            .HasForeignKey(e => e.NextJobId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(e => e.JobCategory)
            .WithMany()
            .HasForeignKey(e => e.JobCategoryId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}