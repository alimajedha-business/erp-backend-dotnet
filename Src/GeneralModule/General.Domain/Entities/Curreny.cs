using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using NGErp.Base.Domain.Entities;

namespace NGErp.General.Domain.Entities
{
    [Table("currencies",Schema ="General")]
    public class Currency 
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Name2 { get; set; }
        public int Code { get; set; }
        public string? Symbol { get; set; }
        public string? Iso { get; set; }

        public string DecimalPlaces { get; set; } = null!;
        public string RoundMethod { get; set; } = null!;
        public string? FractionalCurrencyUnit { get; set; }

        public ICollection<Country> Countries { get; set; } = [];

        //public void Map(EntityTypeBuilder<Currency> builder)
        //{
        //    builder.Property(x => x.Name)
        //           .HasMaxLength(100)
        //           .IsRequired();

            //    builder.Property(x => x.Name2)
            //           .HasMaxLength(100);

            //    builder.Property(x => x.Code)
            //           .IsRequired();

            //    builder.HasIndex(x => x.Code)
            //           .IsUnique();

            //    builder.Property(x => x.Symbol)
            //           .HasMaxLength(10);

            //    builder.Property(x => x.Iso)
            //           .HasMaxLength(3);

            //    builder.Property(x => x.DecimalPlaces)
            //           .HasMaxLength(1)
            //           .IsRequired();

            //    builder.Property(x => x.RoundMethod)
            //           .HasMaxLength(8)
            //           .IsRequired();

            //    builder.Property(x => x.FractionalCurrencyUnit)
            //           .HasMaxLength(50);

            //    builder.Property(x => x.CreatedAt)
            //           .IsRequired();

            //    builder.Property(x => x.UpdatedAt);
        //}
    }
}
