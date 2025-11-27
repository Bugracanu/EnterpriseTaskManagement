using EnterpriseTaskManagement.Application.DTOs;
using MediatR;

namespace EnterpriseTaskManagement.Application.Commands.Users;

public class CreateUserCommand : IRequest<UserDto>
{
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string? PhoneNumber { get; set; }
    public int Role { get; set; }
}