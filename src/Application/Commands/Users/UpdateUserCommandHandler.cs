using EnterpriseTaskManagement.Application.DTOs;
using EnterpriseTaskManagement.Domain.Entities;
using EnterpriseTaskManagement.Domain.Enums;
using EnterpriseTaskManagement.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace EnterpriseTaskManagement.Application.Commands.Users;

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, UserDto>
{
    private readonly IUserRepository _userRepository;
    private readonly ILogger<UpdateUserCommandHandler> _logger;

    public UpdateUserCommandHandler(
        IUserRepository userRepository,
        ILogger<UpdateUserCommandHandler> logger)
    {
        _userRepository = userRepository;
        _logger = logger;
    }

    public async Task<UserDto> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.Id, cancellationToken);
        
        if(user == null)
        {
            throw new KeyNotFoundException($"Kullanıcı bulunamadı: {request.Id}");
        }

        //güncelle
        user.FirstName =request.FirstName;
        user.LastName =request.LastName;
        user.PhoneNumber =request.PhoneNumber;
        user.ProfilePictureUrl =request.ProfilePictureUrl;
        user.Role =(UserRole)request.Role;
        user.IsActive =request.IsActive;

        _userRepository.Update(user);
        await _userRepository.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Kullanıcı güncellendi: {Id}", request.Id);

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
            CreatedAt = user.CreatedAt,
        };
        
    }
}