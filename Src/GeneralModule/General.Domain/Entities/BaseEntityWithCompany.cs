using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NGErp.Base.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace NGErp.General.Domain.Entities;

public abstract class BaseEntityWithCompany : BaseEntity
{
    public Guid CompanyId { get; set; }

    public static new void MapBase(EntityTypeBuilder builder)
    {
        builder
            .HasOne(typeof(Company))
            .WithMany()
            .HasForeignKey("CompanyId")
            .OnDelete(DeleteBehavior.NoAction);
    }
}
