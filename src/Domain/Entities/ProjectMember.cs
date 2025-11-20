namespace EnterpriseTaskManagement.Domain.Entities;

public class ProjectMember : BaseEntity
{
    public Guid ProjectId { get; set; }
    public Guid UserId { get; set; }
    public ProjectMemberRole Role { get; set; }
    public DateTime JoinedDate { get; set; }
    public DateTime? LeftDate { get; set; }
    public bool IsActive { get; set; }

    //Navigation Properties
    public Project Project { get; set; } = null!;
    public User User { get; set; } = null!;

}