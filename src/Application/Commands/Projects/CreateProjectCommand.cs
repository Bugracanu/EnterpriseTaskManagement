using EnterpriseTaskManagement.Application.DTOs;
using MediatR;

namespace EnterpriseTaskManagement.Application.Commands.Projects;

public class CreateProjectCommand : IRequest<ProjectDto>
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string? Code { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public int Status { get; set; }
    public int Priority { get; set; }
    public Decimal Budget { get; set; }
    public Guid? ManagerId { get; set; }
}