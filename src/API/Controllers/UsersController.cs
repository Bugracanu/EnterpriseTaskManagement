using EnterpriseTaskManagement.Application.Commands.Users;
using EnterpriseTaskManagement.Application.DTOs;
using EnterpriseTaskManagement.Application.Queries.Users;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EnterpriseTaskManagement.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IMediator _mediator;

    public UsersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    // GET: api/users
    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserDto>>> GetAllUsers(CancellationToken cancellationToken)
    {
        var query = new GetAllUsersQuery();
        var users = await _mediator.Send(query, cancellationToken);
        return Ok(users);
    }

    // GET: api/users/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<UserDto>> GetUserById(Guid id, CancellationToken cancellationToken)
    {
        var query = new GetUserByIdQuery { Id = id };
        var user = await _mediator.Send(query, cancellationToken);
        
        if (user == null)
        {
            return NotFound();
        }
        
        return Ok(user);
    }

    // POST: api/users
    [HttpPost]
    public async Task<ActionResult<UserDto>> CreateUser([FromBody] CreateUserCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var user = await _mediator.Send(command, cancellationToken);
            return CreatedAtAction(nameof(GetUserById), new { id = user.Id }, user);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    // PUT: api/users/{id}
    [HttpPut("{id}")]
    public async Task<ActionResult<UserDto>> UpdateUser(Guid id, [FromBody] UpdateUserCommand command, CancellationToken cancellationToken)
    {
        if (id != command.Id)
        {
            return BadRequest();
        }

        try
        {
            var user = await _mediator.Send(command, cancellationToken);
            return Ok(user);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }
}