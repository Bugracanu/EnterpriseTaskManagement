using EnterpriseTaskManagement.Domain.Entities;
using EnterpriseTaskManagement.Domain.Interfaces;
using EnterpriseTaskManagement.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace EnterpriseTaskManagement.Infrastructure.Repositories;

public class ProjectRepository : GenericRepository<Project>, IProjectRepository
{
    public ProjectRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Project>> GetByManagerIdAsync(Guid managerId, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Where(p => p.ManagerId == managerId)
            .ToListAsync(cancellationToken);
    }

    public async Task<Project?> GetByCodeAsync(string code, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .FirstOrDefaultAsync(p => p.Code == code, cancellationToken);
    }

    public async Task<bool> CodeExistsAsync(string code, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .AnyAsync(p => p.Code == code, cancellationToken);
    }
}