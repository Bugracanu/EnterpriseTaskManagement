using EnterpriseTaskManagement.Application.DTOs;
using EnterpriseTaskManagement.Domain.Entities;
using EnterpriseTaskManagement.Domain.Enums;
using EnterpriseTaskManagement.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace EnterpriseTaskManagement.Application.Commands.Users;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, UserDto>
{
    private readonly IUserRepository _userRepository;
    private readonly ILogger<CreateUserCommandHandler> _logger;

    public CreateUserCommandHandler(
        IUserRepository userRepository,
        ILogger<CreateUserCommandHandler> logger)
    {
        _userRepository = userRepository;
        _logger = logger;
    }

    public async Task<UserDto> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        // Email kontrolü
        if (await _userRepository.EmailExistsAsync(request.Email, cancellationToken))
        {
            throw new InvalidOperationException($"Email '{request.Email}' zaten kullanılıyor.");
        }

        // Yeni kullanıcı oluştur
        var user = new User
        {
            Email = request.Email,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
            FirstName = request.FirstName,
            LastName = request.LastName,
            PhoneNumber = request.PhoneNumber,
            Role = (UserRole)request.Role,
            IsActive = true
        };

        await _userRepository.AddAsync(user, cancellationToken);
        await _userRepository.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Yeni kullanıcı oluşturuldu: {Email}", request.Email);

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