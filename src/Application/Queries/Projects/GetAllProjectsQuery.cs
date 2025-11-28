using EnterpriseTaskManagement.Application.DTOs;
using MediatR;

namespace EnterpriseTaskManagement.Application.Queries.Projects;

public class GetAllProjectsQuery : IRequest<IEnumerable<ProjectDto>>
{
}