using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using NGErp.Base.Domain.Entities;

namespace NGErp.HCM.Domain.Entities;

public class JobCategory :
    BaseEntity,
    IBaseEntityTypeConfiguration<JobCategory>
{
    public int Code { get; set; }
    public required string Title { get; set; }

    public void Map(EntityTypeBuilder<JobCategory> builder)
    {
        builder.
            ToTable(nameof(JobCategory), "HCM");

        builder
            .HasIndex(i => new { i.Code })
            .IsUnique()
            .HasDatabaseName("UX_JobCategory_Code");

        builder
            .Property(e => e.Title)
            .HasMaxLength(500);
    }
}