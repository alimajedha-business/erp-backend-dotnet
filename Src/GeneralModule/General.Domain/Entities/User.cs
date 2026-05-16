using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NGErp.Base.Domain.Entities;

namespace NGErp.General.Domain.Entities;

public class User : IViewModelTypeConfiguration<User>
{
    public Guid Id { get; set; }
    public string Username { get; set; } = default!;
    public string Password { get; set; } = default!;
    public bool IsSuperuser { get; set; }
    public bool IsActive { get; set; }
    public Guid? PersonId { get; set; }

    public virtual Person? Person { get; set; }

    public void Map(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("users", "general");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id).HasColumnName("id");
        builder.Property(e => e.Username).HasColumnName("username").HasMaxLength(150).IsRequired();
        builder.Property(e => e.Password).HasColumnName("password").HasMaxLength(128).IsRequired();
        builder.Property(e => e.IsSuperuser).HasColumnName("is_superuser");
        builder.Property(e => e.IsActive).HasColumnName("is_active");
        builder.Property(e => e.PersonId).HasColumnName("person_id");

        builder.HasOne(e => e.Person)
            .WithOne()
            .HasForeignKey<User>(e => e.PersonId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
