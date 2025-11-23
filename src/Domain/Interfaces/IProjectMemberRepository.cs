using EnterpriseTaskManagement.Domain.Entities;

namespace EnterpriseTaskManagement.Domain.Interfaces;

public interface IProjectMemberRepository : IGenericRepository<ProjectMember>
{
    Task<ProjectMember?> GetByProjectAndUserAsync(Guid projectId, Guid userId, CancellationToken cancellationToken = default);
    Task<IEnumerable<ProjectMember>> GetByProjectIdAsync(Guid projectId, CancellationToken cancellationToken = default);
    Task<IEnumerable<ProjectMember>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);
}