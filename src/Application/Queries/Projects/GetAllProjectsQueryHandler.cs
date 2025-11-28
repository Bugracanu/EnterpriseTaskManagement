using EnterpriseTaskManagement.Application.DTOs;
using EnterpriseTaskManagement.Domain.Entities;
using EnterpriseTaskManagement.Domain.Interfaces;
using MediatR;

namespace EnterpriseTaskManagement.Application.Queries.Projects;

public class GetAllProjectsQueryHandler : IRequestHandler<GetAllProjectsQuery, IEnumerable<ProjectDto>>
{
    private readonly IProjectRepository _projectRepository;
    private readonly IUserRepository _userRepository;

    public GetAllProjectsQueryHandler(
        IProjectRepository projectRepository,
        IUserRepository userRepository)
    {
        _projectRepository = projectRepository;
        _userRepository = userRepository;
    }

    
    public async Task<IEnumerable<ProjectDto>> Handle(GetAllProjectsQuery request, CancellationToken cancellationToken)
    {
        var projects = await _projectRepository.GetAllAsync(cancellationToken);

        var ProjectDtos = new List<ProjectDto>();

        foreach(var project in projects)
        {
            var managerName = project.ManagerId.HasValue
                ? (await _userRepository.GetByIdAsync(project.ManagerId.Value, cancellationToken))?.FullName
                : null;

            ProjectDtos.Add(new ProjectDto
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
            });

        }
        return ProjectDtos;
    }
}