using EnterpriseTaskManagement.Application.DTOs;
using MediatR;

namespace EnterpriseTaskManagement.Application.Queries.Projects;

public class GetProjectByIdQuery : IRequest<ProjectDto>
{
    public Guid Id { get; set; }
}