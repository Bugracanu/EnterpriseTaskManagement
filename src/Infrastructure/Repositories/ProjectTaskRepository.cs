using EnterpriseTaskManagement.Domain.Entities;
using EnterpriseTaskManagement.Domain.Interfaces;
using EnterpriseTaskManagement.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace EnterpriseTaskManagement.Infrastructure.Repositories;

public class ProjectTaskRepository : GenericRepository<ProjectTask>, IProjectTaskRepository
{
    public ProjectTaskRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<ProjectTask>> GetByProjectIdAsync(Guid projectId, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Where(t => t.ProjectId == projectId)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<ProjectTask>> GetByAssignedToIdAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Where(t => t.AssignedToId == userId)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<ProjectTask>> GetSubTasksAsync(Guid parentTaskId, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Where(t => t.ParentTaskId == parentTaskId)
            .ToListAsync(cancellationToken);
    }
}