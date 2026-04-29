using System.ComponentModel.DataAnnotations.Schema;

using NGErp.Base.Domain.Entities;

namespace NGErp.General.Domain.Entities;
public enum MilitaryStatusType 
    {
       NotInclude = 1,
       Included = 2,
       Exempt = 3,
       Completed = 4,
       StudentExeption = 5,
       Absent = 6,
       InProgress = 7,
    }


[Table("military_service_statuses",Schema ="General")]
public class MilitaryServiceStatus 
{
    public Guid Id { get; set; }
    public string Title { get; set; } = default!;
    public MilitaryStatusType Type { get; set; }

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
