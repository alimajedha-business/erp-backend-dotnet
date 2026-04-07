using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using NGErp.Base.Domain.Entities;
using NGErp.General.Domain.Entities;

namespace NGErp.Warehouse.Domain.Entities;

public class Item :
    BaseEntityWithCompany,
    IBaseEntityTypeConfiguration<Item>
{
    public required string Code { get; set; }
    public required string Title { get; set; }
    public string TitleInEnglish { get; set; } = default!;
    public string TechnicalNumber { get; set; } = default!;
    public string Sku { get; set; } = default!;
    public string Barcode { get; set; } = default!;
    public bool IsActive { get; set; } = true;
    public Guid PrimaryUnitOfMeasurementId { get; set; }
    public Guid ItemTypeId {  get; set; }
    public Guid CategoryId { get; set; }

    public UnitOfMeasurement PrimaryUnitOfMeasurement { get; set; } = default!;
    public ItemType ItemType { get; set; } = default!;
    public Category Category { get; set; } = default!;

    public ICollection<ItemAttribute> ItemAttributes { get; set; } = [];
    public ICollection<ItemUnitOfMeasurement> ItemUnitOfMeasurements { get; set; } = [];

    public void Map(EntityTypeBuilder<Item> builder)
    {
        builder.
            ToTable(nameof(Item), "Warehouse");

        builder
            .HasIndex(i => i.CategoryId)
            .HasDatabaseName("IX_Item_Category");

        builder
            .HasIndex(i => new { i.CompanyId, i.Code })
            .IsUnique()
            .HasDatabaseName("UX_Item_Company_Code");

        builder
            .HasIndex(i => new { i.CompanyId, i.Sku })
            .IsUnique()
            .HasDatabaseName("UX_Item_Company_Sku");

        builder
            .Property(e => e.Code)
            .HasMaxLength(80);

        builder
            .Property(e => e.Title)
            .HasMaxLength(255);

        builder
            .Property(e => e.TitleInEnglish)
            .HasMaxLength(255);

        builder
            .Property(e => e.TechnicalNumber)
            .HasMaxLength(80);

        builder
            .Property(e => e.Barcode)
            .HasMaxLength(80);

        builder
            .Property(e => e.Sku)
            .HasMaxLength(80);

        builder
            .Property(e => e.IsActive)
            .HasDefaultValue(true);

        builder
            .HasOne(e => e.Category)
            .WithMany(e => e.Items)
            .HasForeignKey(e => e.CategoryId)
            .OnDelete(DeleteBehavior.NoAction);

        builder
            .HasOne(e => e.ItemType)
            .WithMany(e => e.Items)
            .HasForeignKey(e => e.ItemTypeId)
            .OnDelete(DeleteBehavior.NoAction);

        builder
            .HasOne(e => e.PrimaryUnitOfMeasurement)
            .WithMany()
            .HasForeignKey(e => e.PrimaryUnitOfMeasurementId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
