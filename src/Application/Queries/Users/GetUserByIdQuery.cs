using EnterpriseTaskManagement.Application.DTOs;
using EnterpriseTaskManagement.Domain.Entities;
using MediatR;

namespace EnterpriseTaskManagement.Application.Queries.Users;

public class GetUserByIdQuery : IRequest<UserDto?>
{
    public Guid Id { get; set; }
}