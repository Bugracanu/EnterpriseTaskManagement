using EnterpriseTaskManagement.Domain.Enums;

namespace EnterpriseTaskManagement.Domain.Entities;

public class ProjectTask : BaseEntity
{
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; } = string.Empty;
    public Guid ProjectId { get; set; }
    public Guid? AssignedToId { get; set; } // Kime atandı
    public Guid? ParentTaskId { get; set; } //Alt görev için
    public EnterpriseTaskManagement.Domain.Enums.TaskStatus Status { get; set; }
    public TaskPriority Priority { get; set; }
    public DateTime? DueDate { get; set; } // Bitiş tarihi
    public DateTime? ComletedDate { get; set; }
    public int EstimatedHours { get; set; }
    public int ActualHours { get; set; }
    public string? Tags { get; set; } // Etiketler

    //Navigation Properties
    public Project Project { get; set; } = null!;
    public User? AssignedTo { get; set; }
    public ProjectTask? ParentTask { get; set; }
    public ICollection<ProjectTask> SubTasks { get; set; } = new List<ProjectTask>();
}