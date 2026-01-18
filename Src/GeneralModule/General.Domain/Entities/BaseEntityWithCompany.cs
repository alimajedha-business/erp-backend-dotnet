using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NGErp.Base.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace NGErp.General.Domain.Entities;

public abstract class BaseEntityWithCompany : BaseEntity
{
    public Guid CompanyId { get; private set; }

    
    public static new void MapBase(EntityTypeBuilder builder)
    {
        builder
            .HasOne(typeof(Company))
            .WithMany()
            .HasForeignKey("CompanyId")
            .OnDelete(DeleteBehavior.NoAction);
    }
}
