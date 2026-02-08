using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using NGErp.Base.Domain.Entities;
using NGErp.General.Domain.Entities;

namespace NGErp.HCM.Domain.Entities
{
    public class Position : BaseEntityWithCompany, IBaseEntityTypeConfiguration<Position>
    {
        public string? Code { get; set; }

        public required string Name { get; set; }

        public string? Description { get; set; }

        public bool Status { get; private set; } = true;

        public DateTime? StatusChangeDate { get; private set; }

        public void ChangeStatus(bool newStatus, DateTime now)
        {
            if (Status == newStatus)
                return;

            Status = newStatus;
            StatusChangeDate = newStatus ? null : now;
        }

        public void Map(EntityTypeBuilder<Position> builder)
        {
            builder.ToTable("Position", "HCM");
            builder.Property(e => e.Name).HasMaxLength(200);
            builder.Property(e => e.Description).HasMaxLength(500);
            builder.Property(e => e.Code).HasMaxLength(100);
        }
    }
}
