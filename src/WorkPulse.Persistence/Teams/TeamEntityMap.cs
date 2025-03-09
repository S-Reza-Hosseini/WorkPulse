using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WorkPulse.Entities.Teams;

namespace WorkPulse.Persistence.Teams;

public class TeamEntityMap : IEntityTypeConfiguration<Team>
{
    public void Configure(EntityTypeBuilder<Team> builder)
    {
        builder.ToTable("Teams");
        builder.HasKey(t => t.Id);
        
        builder.Property(t => t.Id).ValueGeneratedOnAdd();
        builder.Property(t => t.Name).IsRequired().HasMaxLength(100);
        builder.Property(t => t.Description).IsRequired(false).HasMaxLength(2000);
        builder.Property(t => t.Avatar).IsRequired(false);
        
    }
}