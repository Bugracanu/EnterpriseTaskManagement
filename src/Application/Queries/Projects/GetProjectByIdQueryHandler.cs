using EnterpriseTaskManagement.Application.DTOs;
using EnterpriseTaskManagement.Domain.Entities;
using EnterpriseTaskManagement.Domain.Interfaces;
using MediatR;

namespace EnterpriseTaskManagement.Application.Queries.Projects;

public class GetProjectByIdQueryHandler : IRequestHandler<GetProjectByIdQuery, ProjectDto?>
{
    private readonly IProjectRepository _projectRepository;
    private readonly IUserRepository _userRepository;

    public GetProjectByIdQueryHandler(
        IProjectRepository projectRepository,
        IUserRepository userRepository)
    {
        _projectRepository = projectRepository;
        _userRepository = userRepository;
    }
    public async Task<ProjectDto?> Handle(GetProjectByIdQuery request, CancellationToken cancellationToken)
    {
        var project = await _projectRepository.GetByIdAsync(request.Id, cancellationToken);

        if(project == null)
        {
            return null;
        }

        //manager bilgisi al
        var managerName = project.ManagerId.HasValue
            ? (await _userRepository.GetByIdAsync(project.ManagerId.Value, cancellationToken))?.FullName
            : null;

        return new ProjectDto
        {
            Id = project.Id,
            Name = project.Name,
            Description = project.Description,
            Code = project.Code,
            StartDate = project.StartDate,
            EndDate = project.EndDate,
            Status = (int)project.Status,
            StatusName = project.Status.ToString(),
            Priority = (int)project.Priority,
            PriorityName = project.Priority.ToString(),
            Budget = project.Budget,
            ManagerId = project.ManagerId,
            ManagerName = managerName,
            CreatedAt = project.CreatedAt
        };
    }
}