namespace EnterpriseTaskManagement.Application.DTOs;

public class ProjectDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string? Code { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public int Status { get; set; }
    public string StatusName { get; set; } = string.Empty;
    public int Priority { get; set; }
    public string PriorityName { get; set; } = string.Empty;

    public decimal Budget { get; set; }
    public Guid? ManagerId { get; set; }
    public string? ManagerName { get; set; }
    public DateTime CreatedAt { get; set; }
}