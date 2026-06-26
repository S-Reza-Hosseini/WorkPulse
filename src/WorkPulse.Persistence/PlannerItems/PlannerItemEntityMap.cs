using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WorkPulse.Entities.PlannerItems;
using WorkPulse.Entities.TaskItems.TaskPriorities;
using TaskStatus = WorkPulse.Entities.TaskItems.TaskStatuses.TaskStatus;

namespace WorkPulse.Persistence.PlannerItems;

public class PlannerItemEntityMap : IEntityTypeConfiguration<PlannerItem>
{
    public void Configure(EntityTypeBuilder<PlannerItem> builder)
    {
        builder.ToTable("PlannerItems");
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id).ValueGeneratedOnAdd();
        builder.Property(p => p.Title).IsRequired().HasMaxLength(150);
        builder.Property(p => p.Description).IsRequired(false).HasMaxLength(2000);
        builder.Property(p => p.Status).HasDefaultValue(TaskStatus.Todo);
        builder.Property(p => p.Priority).HasDefaultValue(TaskPriority.Medium);
        builder.Property(p => p.DueDate).IsRequired();
        builder.Property(p => p.CreatedAt).IsRequired();
        builder.Property(p => p.UpdatedAt).IsRequired();
        builder.Property(p => p.CompletedAt).IsRequired(false);

        builder.HasOne(p => p.User)
            .WithMany()
            .HasForeignKey(p => p.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
