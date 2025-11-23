using EnterpriseTaskManagement.Domain.Entities;

namespace EnterpriseTaskManagement.Domain.Interfaces;

public interface IProjectRepository : IGenericRepository<Project>
{
    Task<IEnumerable<Project>> GetByManagerIdAsync(Guid managerId, CancellationToken cancellationToken = default);
    Task<Project?> GetByCodeAsync(string code, CancellationToken cancellationToken = default);
    Task<bool> CodeExistsAsync(string code, CancellationToken cancellationToken = default);
}