namespace EnterpriseTaskManagement.Application.DTOs;

public class UpdateProjectDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string? Code { get; set; }
    public DateTime StartDate { get; set; }
    public int Status { get; set; }
    public int Priority { get; set; }
    public decimal Budget { get; set; }
    public Guid? ManagerId { get; set; }
}