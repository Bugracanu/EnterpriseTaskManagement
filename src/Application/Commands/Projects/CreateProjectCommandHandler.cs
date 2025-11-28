using EnterpriseTaskManagement.Application.DTOs;
using EnterpriseTaskManagement.Domain.Entities;
using EnterpriseTaskManagement.Domain.Enums;
using EnterpriseTaskManagement.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace EnterpriseTaskManagement.Application.Commands.Projects;

public class CreateProjectCommandHandler : IRequestHandler<CreateProjectCommand, ProjectDto>
{
    private readonly IProjectRepository _projectRepository;
    private readonly IUserRepository _userRepository;
    private readonly ILogger<CreateProjectCommandHandler> _logger;

    public CreateProjectCommandHandler(
        IProjectRepository projectRepository,
        IUserRepository userRepository,
        ILogger<CreateProjectCommandHandler> logger)
    {
        _projectRepository = projectRepository;
        _userRepository = userRepository;
        _logger = logger;
    }

    public async Task<ProjectDto> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
    {
        //code kontrolü (eğer verilmişse)
        if (!string.IsNullOrEmpty(request.Code))
        {
            if (await _projectRepository.CodeExistsAsync(request.Code, cancellationToken))
            {
                throw new InvalidOperationException($"Proje kodu '{request.Code}' zaten kullanılıyor.");
            }
        }

        //manager kontrolü (eğer verilmişse)
        if (request.ManagerId.HasValue)
        {
            var manager = await _userRepository.GetByIdAsync(request.ManagerId.Value, cancellationToken);
            if(manager == null)
            {
                throw new KeyNotFoundException($"Yönetici bulunamadı: {request.ManagerId.Value}");
            }
        }

        //yeni proje oluştur
        var project = new Project
        {
            Name = request.Name,
            Description = request.Description,
            Code = request.Code,
            StartDate = request.StartDate,
            EndDate = request.EndDate,
            Status = (ProjectStatus)request.Status,
            Priority = (ProjectPriority)request.Priority,
            Budget = request.Budget,
            ManagerId = request.ManagerId,
        };

        await _projectRepository.AddAsync(project, cancellationToken);
        await _projectRepository.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Yeni proje oluşturuldu: {Name}", request.Name);

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