using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WorkPulse.Entities.TeamMemberships;

namespace WorkPulse.Persistence.TeamMemberships;

public class TeamMembershipEntityMap:IEntityTypeConfiguration<TeamMembership>
{
    public void Configure(EntityTypeBuilder<TeamMembership> builder)
    {
        builder.ToTable("TeamMemberships");
        builder.HasKey(t => t.Id);

        builder.Property(t => t.Id).ValueGeneratedOnAdd();
        builder.Property(t => t.Role).IsRequired();
        builder.Property(t => t.JoinedAt).IsRequired();
        builder.Property(t => t.Permissions)
            .IsRequired().HasColumnType("NVARCHAR(MAX)");
        
        builder.HasOne(t => t.User)
            .WithMany(u => u.TeamMemberships)
            .HasForeignKey(t => t.UserId)
            .OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(t => t.Team)
            .WithMany(t => t.TeamMemberships)
            .HasForeignKey(t => t.TeamId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}