using System.ComponentModel.DataAnnotations.Schema;

using NGErp.Base.Domain.Entities;

namespace NGErp.General.Domain.Entities;
[Table("military_service_statuses",Schema ="General")]
public class MilitaryServiceStatus 
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;

    //public void Map(EntityTypeBuilder<MilitaryServiceStatus> builder)
    //{
    //    builder
    //        .ToTable(nameof(MilitaryServiceStatus), "General");

        //builder
        //    .HasIndex(e => e.Name)
        //    .IsUnique()
        //    .HasDatabaseName("UX_MilitaryServiceStatus_Name");

        //builder
        //    .Property(e => e.Name)
        //    .HasMaxLength(50);
    //}
}
