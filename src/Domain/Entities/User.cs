using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EnterpriseTaskManagement.Domain.Entities;

namespace EnterpriseTaskManagement.Domain.Entities;
{
    public class User : BaseEntity
    {
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string? PhoneNumber { get; set; }
        public string? ProfilePictureUrl { get; set; }
        public UserRole Role { get; set; }
        public bool IsActive { get; set; }

        //ilişkiler - Navigation Properties
        public ICollection<ProjectMember> ProjectMembers { get; set; } = new List<ProjectMember>();
        public ICollection<ProjectTask> AssignedTasks { get; set; } = new List<ProjectTask>();
        public ICollection<ProjectTask> CreatedTasks { get; set; } = new List<ProjectTask>();
        public string FullName => $"{FullName} {LastName}";
    }
}