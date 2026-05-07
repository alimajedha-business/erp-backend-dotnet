using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using NGErp.Base.Domain.Entities;
using NGErp.General.Domain.Entities;

namespace NGErp.HCM.Domain.Entities;

public class PositionJob :
    BaseEntityWithCompany,
    IBaseEntityTypeConfiguration<PositionJob>
{
    public Guid PositionId { get; set; }
    public Guid JobId { get; set; }
    public Position Position { get; set; } = default!;
    public Job Job { get; set; } = default!;
    public void Map(EntityTypeBuilder<PositionJob> builder)
    {
        builder
            .ToTable(nameof(PositionJob), "HCM");

        builder
            .HasIndex(e => e.PositionId)
            .HasDatabaseName("IX_PositionJob_Position");
        builder
            .HasIndex(e => e.JobId)
            .HasDatabaseName("IX_PositionJob_Job");

        builder
            .Property(e => e.PositionId)
            .IsRequired();
        builder
            .Property(e => e.JobId)
            .IsRequired();

        builder
            .HasOne(e => e.Position)
            .WithMany()
            .HasForeignKey(e => e.PositionId)
            .OnDelete(DeleteBehavior.NoAction);
        builder
            .HasOne(e => e.Job)
            .WithMany()
            .HasForeignKey(e => e.JobId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
