using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using NGErp.Base.Domain.Entities;

namespace NGErp.Warehouse.Domain.Entities;

public class ShippingCompany :
    BaseEntity,
    IBaseEntityTypeConfiguration<ShippingCompany>
{
    public required int Code { get; set; }
    public required string Title { get; set; }
    public required string ManagerFirstName { get; set; }
    public required string ManagerLastName { get; set; }
    public required string MobileNumber { get; set; }
    public string PhoneNumber { get; set; } = default!;
    public string Address { get; set; } = default!;

    public void Map(EntityTypeBuilder<ShippingCompany> builder)
    {
        builder.
            ToTable(nameof(ShippingCompany), "Warehouse");

        builder
            .Property(e => e.Title)
            .HasMaxLength(200);

        builder
            .Property(e => e.ManagerFirstName)
            .HasMaxLength(100);

        builder
            .Property(e => e.ManagerLastName)
            .HasMaxLength(100);

        builder
            .Property(e => e.Address)
            .HasMaxLength(255);
    }
}
