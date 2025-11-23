using EnterpriseTaskManagement.Domain.Entities;
using EnterpriseTaskManagement.Domain.Interfaces;
using EnterpriseTaskManagement.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace EnterpriseTaskManagement.Infrastructure.Repositories;

public class ProjectMemberRepository : GenericRepository<ProjectMember>, IProjectMemberRepository
{
    public ProjectMemberRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<ProjectMember?> GetByProjectAndUserAsync(Guid projectId, Guid userId, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .FirstOrDefaultAsync(pm => pm.ProjectId == projectId && pm.UserId == userId, cancellationToken);
    }

    public async Task<IEnumerable<ProjectMember>> GetByProjectIdAsync(Guid projectId, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Where(pm => pm.ProjectId == projectId)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<ProjectMember>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Where(pm => pm.UserId == userId)
            .ToListAsync(cancellationToken);
    }
}