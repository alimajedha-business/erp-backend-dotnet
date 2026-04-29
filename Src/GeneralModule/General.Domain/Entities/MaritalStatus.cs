using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using NGErp.Base.Domain.Entities;
using NGErp.General.Domain.Entities;

namespace NGErp.HCM.Domain.Entities
{
    public enum MaritalStatusType
    {
        Single = 1,
        Married = 2,
        HeadOfHouseHold = 3
    }

    [Table("marital_statuses",Schema ="General")]
    public class MaritalStatus 
    {

        public Guid Id { get; set; }
        public string Title { get; set; } = default!;
        public MaritalStatusType Type { get; set; }

        //public void Map(EntityTypeBuilder<MaritalStatus> builder)
        //{
        //    builder.ToTable("MaritalStatus", "HCM");

        //    builder.Property(e => e.Type)
        //        .IsRequired();

        //    builder.Property(e => e.Title)
        //        .IsRequired();

        //    builder.HasIndex(e => new { e.Title })
        //        .IsUnique()
        //        .HasDatabaseName("UX_MaritalStatus_Title");

        //}

    }
}
