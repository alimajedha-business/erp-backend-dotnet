using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NGErp.Base.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NGErp.General.Domain.Entities;

namespace NGErp.HCM.Domain.Entities
{
    public class Department : BaseEntityWithCompany, IBaseEntityTypeConfiguration<Department>
    {
        public string? Code { get; set; }

        public required string Name { get; set; }

        public string? Description { get; set; }

        public bool Status { get; set; } = true;

        public DateTime StatusChangeDate { get; set; }

        public void Map(EntityTypeBuilder<Department> builder)
        {
            builder.ToTable("Department", "HCM");
            builder.Property(e => e.Name).HasMaxLength(200);
            builder.Property(e => e.Description).HasMaxLength(500);
            builder.Property(e => e.Code).HasMaxLength(100);
        }
    }
}
