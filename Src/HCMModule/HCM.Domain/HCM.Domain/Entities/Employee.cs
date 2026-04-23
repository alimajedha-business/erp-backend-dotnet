using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using NGErp.Base.Domain.Entities;
using NGErp.General.Domain.Entities;

namespace NGErp.HCM.Domain.Entities;

public class Employee :
    BaseEntityWithCompany,
    IBaseEntityTypeConfiguration<Employee>
{
    public Guid PersonId { get; set; }
    public string Code { get; set; } = default!;
    public string CaseNumber { get; set; } = default!;
    public Guid? MilitaryServiceStatusId { get; set; }
    public Guid MaritalStatusId { get; set; }
    public DateTime? MarriageDate { get; set; }

    public Person Person { get; set; } = default!;
    public MilitaryServiceStatus? MilitaryServiceStatus { get; set; }
    public MaritalStatus MaritalStatus { get; set; } = default!;

    public void Map(EntityTypeBuilder<Employee> builder)
    {
        builder
            .ToTable(nameof(Employee), "HCM");

        builder
            .HasIndex(e => e.PersonId)
            .HasDatabaseName("IX_Employee_Person");

        builder
            .HasIndex(e => e.MaritalStatusId)
            .HasDatabaseName("IX_Employee_MaritalStatus");

        builder
            .HasIndex(e => e.MilitaryServiceStatusId)
            .HasDatabaseName("IX_Employee_MilitaryServiceStatus");

        builder
            .HasIndex(e => new { e.CompanyId, e.Code })
            .IsUnique()
            .HasDatabaseName("UX_Employee_Company_Code");

        builder
            .Property(e => e.Code)
            .HasMaxLength(50);

        builder
            .Property(e => e.CaseNumber)
            .HasMaxLength(50);

        builder
            .HasOne(e => e.Person)
            .WithMany()
            .HasForeignKey(e => e.PersonId)
            .OnDelete(DeleteBehavior.NoAction);

        builder
            .HasOne(e => e.MaritalStatus)
            .WithMany()
            .HasForeignKey(e => e.MaritalStatusId)
            .OnDelete(DeleteBehavior.NoAction);

        builder
            .HasOne(e => e.MilitaryServiceStatus)
            .WithMany()
            .HasForeignKey(e => e.MilitaryServiceStatusId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
