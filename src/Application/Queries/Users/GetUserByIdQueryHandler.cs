using EnterpriseTaskManagement.Application.DTOs;
using EnterpriseTaskManagement.Domain.Entities;
using EnterpriseTaskManagement.Domain.Interfaces;
using MediatR;

namespace EnterpriseTaskManagement.Application.Queries.Users;

public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, UserDto?>
{
    private readonly IUserRepository _userRepository;

    public GetUserByIdQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    public async Task<UserDto?> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.Id, cancellationToken);

        if(user == null)
        {
            return null;
        }
        return new UserDto
        {
            Id = user.Id,
            Email = user.Email,
            FirstName = user.FirstName,
            LastName = user.LastName,
            FullName = user.FullName,
            PhoneNumber = user.PhoneNumber,
            ProfilePictureUrl = user.ProfilePictureUrl,
            Role = (int)user.Role,
            RoleName = user.Role.ToString(),
            IsActive = user.IsActive,
            CreatedAt = user.CreatedAt
        };
    }
}