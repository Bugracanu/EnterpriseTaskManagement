using System.Net.Http.Headers;
using EnterpriseTaskManagement.Application.DTOs;
using EnterpriseTaskManagement.Domain.Enums;
using EnterpriseTaskManagement.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace EnterpriseTaskManagement.Application.Commands.Projects;

public class UpdateProjectCommandHandler : IRequestHandler<UpdateProjectCommand, ProjectDto>
{
    private readonly IProjectRepository _projectRepository;
    private readonly IUserRepository _userRepository;
    private readonly ILogger<UpdateProjectCommandHandler> _logger;

    public UpdateProjectCommandHandler(
        IProjectRepository projectRepository,
        IUserRepository userRepository,
        ILogger<UpdateProjectCommandHandler> logger
    )
    {
        _projectRepository = projectRepository;
        _userRepository = userRepository;
        _logger = logger;
    }

    public async Task<ProjectDto> Handle(UpdateProjectCommand request, CancellationToken cancellationToken)
    {
        var project = await _projectRepository.GetByIdAsync(request.Id, cancellationToken);
        if (project == null)
        {
            throw new KeyNotFoundException($"proje bulunamadı: {request.Id}");
        }

        // code kontrolü (değiştirilmişse ve başka projede kullanılıyorsa)
        if (!string.IsNullOrEmpty(request.Code) && request.Code != project.Code)
        {
            if (await _projectRepository.CodeExistsAsync(request.Code, cancellationToken))
            {
                throw new InvalidOperationException($"proje kodu'{request.Code}' zaten kullanılıyor.");
            }
        }

        //manager kontrolü (eğer verilmişse)
        if (request.ManagerId.HasValue)
        {
            var manager = await _userRepository.GetByIdAsync(request.ManagerId.Value, cancellationToken);
            if (manager == null)
            {
                throw new KeyNotFoundException($"Yönetici bulunamadı: {request.ManagerId.Value}");
            }
        }

        //güncelle

        project.Name = request.Name;
        project.Description = request.Description;
        project.Code = request.Code;
        project.StartDate = request.StartDate;
        project.EndDate = request.EndDate;
        project.Status = (ProjectStatus)request.Status;
        project.Priority = (ProjectPriority)request.Priority;
        project.Budget = request.Budget;
        project.ManagerId = request.ManagerId;

        _projectRepository.Update(project);
        await _projectRepository.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Proje güncellendi: {Id}", request.Id);

        //manager bilgisini al
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
            CreatedAt = project.CreatedAt,
        };
    }
}