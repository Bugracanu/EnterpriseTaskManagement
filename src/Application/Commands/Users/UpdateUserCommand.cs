using System.Reflection.Metadata;
using EnterpriseTaskManagement.Application.DTOs;
using EnterpriseTaskManagement.Domain.Entities;
using MediatR;

namespace EnterpriseTaskManagement.Application.Commands.Users;

public class UpdateUserCommand : IRequest<UserDto>
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string? PhoneNumber { get; set; }
    public string? ProfilePictureUrl { get; set; }
    public int Role { get; set; }
    public bool IsActive { get; set; }
}