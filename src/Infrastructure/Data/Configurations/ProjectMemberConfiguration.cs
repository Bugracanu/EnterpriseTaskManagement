using EnterpriseTaskManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EnterpriseTaskManagement.Infrastructure.Data.Configurations;

public class ProjectMemberConfiguration : IEntityTypeConfiguration<ProjectMember>
{
    public void Configure(EntityTypeBuilder<ProjectMember> builder)
    {
        builder.ToTable("ProjectMembers");
        
        builder.HasKey(pm => pm.Id);
        
        builder.Property(pm => pm.ProjectId)
            .IsRequired();
        
        builder.Property(pm => pm.UserId)
            .IsRequired();
        
        builder.Property(pm => pm.Role)
            .IsRequired();
        
        builder.Property(pm => pm.JoinedDate)
            .IsRequired();
        
        builder.Property(pm => pm.IsActive)
            .IsRequired()
            .HasDefaultValue(true);
        
        // Unique constraint: Bir kullanıcı aynı projede sadece bir kez olabilir
        builder.HasIndex(pm => new { pm.ProjectId, pm.UserId })
            .IsUnique();
        
        // Navigation Properties
        builder.HasOne(pm => pm.Project)
            .WithMany(p => p.ProjectMembers)
            .HasForeignKey(pm => pm.ProjectId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasOne(pm => pm.User)
            .WithMany(u => u.ProjectMembers)
            .HasForeignKey(pm => pm.UserId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}