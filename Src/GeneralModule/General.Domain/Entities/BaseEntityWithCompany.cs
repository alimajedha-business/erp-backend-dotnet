using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NGErp.Base.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace NGErp.General.Domain.Entities;

public class BaseEntityWithCompany : BaseEntity
{    
    public Guid CompanyId { get; private set; }

    public required  Company Company { get;  set; }

    public static void Map(EntityTypeBuilder builder)
    {
        builder
            .HasOne(typeof(Company))
            .WithMany()
            .HasForeignKey("CompanyId")
            .OnDelete(DeleteBehavior.Restrict);
    }
}
