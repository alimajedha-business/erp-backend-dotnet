using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using NGErp.Base.Domain.Entities;
using NGErp.General.Domain.Entities;

namespace NGErp.HCM.Domain.Entities
{
    public class OrganizationalStructure : BaseEntityWithCompany, IBaseEntityTypeConfiguration<OrganizationalStructure>
    {
        public Guid ParentId { get; set; }
        public Guid DepartmentId { get; set; }
        public Guid PositionId { get; set; }
        public char NodeType { get; set; }
        public DateOnly ValidFrom { get; set; }
        public DateOnly ValidTo { get; set; }
        public string? Description { get; set; }

        public OrganizationalStructure? Parent { get; set; }
        public Department? Department { get; set; }
        public Position? Position { get; set; }

        public void Map(EntityTypeBuilder<OrganizationalStructure> builder)
        {
            builder
                .ToTable(nameof(OrganizationalStructure), "HCM");

            builder.HasOne(typeof(Department))
                .WithMany()
                .HasForeignKey("DepartmentId")
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(typeof(Position))
                .WithMany()
                .HasForeignKey("PositionId")
                .OnDelete(DeleteBehavior.NoAction);

        }
    }
}
