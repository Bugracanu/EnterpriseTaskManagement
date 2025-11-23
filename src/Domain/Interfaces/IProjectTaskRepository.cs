using EnterpriseTaskManagement.Domain.Entities;

namespace EnterpriseTaskManagement.Domain.Interfaces;

public interface IProjectTaskRepository : IGenericRepository<ProjectTask>
{
    Task<IEnumerable<ProjectTask>> GetByProjectIdAsync(Guid projectId, CancellationToken cancellationToken = default);
    Task<IEnumerable<ProjectTask>> GetByAssignedToIdAsync(Guid userId, CancellationToken cancellationToken = default);
    Task<IEnumerable<ProjectTask>> GetSubTasksAsync(Guid parentTaskId, CancellationToken cancellationToken = default);
}