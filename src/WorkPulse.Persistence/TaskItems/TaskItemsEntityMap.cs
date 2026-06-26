using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WorkPulse.Entities.TaskItems;
using WorkPulse.Entities.TaskItems.TaskPriorities;
using TaskStatus = WorkPulse.Entities.TaskItems.TaskStatuses.TaskStatus;

namespace WorkPulse.Persistence.TaskItems;

public class TaskItemsEntityMap : IEntityTypeConfiguration<TaskItem>
{
    public void Configure(EntityTypeBuilder<TaskItem> builder)
    {
        builder.ToTable("TaskItems");
        builder.HasKey(ti => ti.Id);
        
        builder.Property(ti => ti.Id).ValueGeneratedOnAdd();
        builder.Property(ti => ti.Title).IsRequired().HasMaxLength(150);
        builder.Property(ti => ti.Description).IsRequired(false).HasMaxLength(2000);
        builder.Property(ti => ti.Status).HasDefaultValue(TaskStatus.Todo);
        builder.Property(ti => ti.CreatedAt).IsRequired();
        builder.Property(ti => ti.UpdatedAt);
        builder.Property(ti => ti.Priority).HasDefaultValue(TaskPriority.Medium);
        builder.Property(ti => ti.CreatorId).IsRequired();
        builder.Property(ti => ti.ActorId).IsRequired();
        builder.Property(ti => ti.CompletedAt).IsRequired(false);
        builder.Property(ti => ti.EstimatedTime).IsRequired();
        builder.Property(ti => ti.DueDate).IsRequired();
        
        
        builder.HasOne(ti => ti.Team)
            .WithMany(t => t.TaskItems)
            .HasForeignKey(ti => ti.TeamId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}