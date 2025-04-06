using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WorkPulse.Entities.Users;

namespace WorkPulse.Persistence.Users;

public class UserEntityMap:IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");
        builder.HasKey(u => u.Id);

        builder.Property(u => u.Username).IsRequired().HasMaxLength(100);
        builder.HasIndex(u => u.Username).IsUnique();
        builder.Property(u => u.Email).IsRequired().HasMaxLength(255);
        builder.HasIndex(u => u.Email).IsUnique();
        builder.Property(u => u.Password).IsRequired().HasMaxLength(128);
        builder.Property(u => u.PhoneNumber).IsRequired(false).HasMaxLength(11);
        builder.Property(u => u.Avatar).IsRequired(false);
        builder.Property(u => u.Role).IsRequired();
        builder.Property(u => u.CreationDate).IsRequired();
        builder.Property(u => u.UpdatedAt).IsRequired(false);
        builder.Property(u => u.FirstName).IsRequired().HasMaxLength(100);
        builder.Property(u => u.LastName).IsRequired().HasMaxLength(100);
    }
}