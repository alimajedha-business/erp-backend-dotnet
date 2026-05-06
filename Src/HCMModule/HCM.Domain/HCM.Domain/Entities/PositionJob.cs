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

public class PositionJob :
    BaseEntity,
    IBaseEntityTypeConfiguration<PositionJob>
{
    public Guid PositionId { get; set; }
    public Guid JobId { get; set; }

    public Position Position { get; set; } = default!;
    public Job Job { get; set; } = default!;

    public void Map(EntityTypeBuilder<PositionJob> builder)
    {
        builder.
            ToTable(nameof(PositionJob), "HCM");

        builder.HasOne(e => e.Position)
           .WithMany()
           .HasForeignKey(e => e.PositionId)
           .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(e => e.Job)
            .WithMany()
            .HasForeignKey(e => e.JobId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}