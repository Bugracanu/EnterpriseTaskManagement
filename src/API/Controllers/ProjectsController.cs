using EnterpriseTaskManagement.Application.Commands.Projects;
using EnterpriseTaskManagement.Application.DTOs;
using EnterpriseTaskManagement.Application.Queries.Projects;
using EnterpriseTaskManagement.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EnterpriseTaskManagement.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProjectsController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProjectsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    // GET: api/projects
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProjectDto>>> GetAllProjects(CancellationToken cancellationToken)
    {
        var query = new GetAllProjectsQuery();
        var projects = await _mediator.Send(query, cancellationToken);
        return Ok(projects);
    }

    //GET api/projects/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<ProjectDto>> GetProjectById(Guid id, CancellationToken cancellationToken)
    {
        var query = new GetProjectByIdQuery { Id = id };
        var project = await _mediator.Send(query, cancellationToken);

        if (project == null)
        {
            return NotFound();
        }

        return Ok(project);
    }
    //POST : api/projects
    [HttpPost]
    public async Task<ActionResult<ProjectDto>> CreateProject([FromBody] CreateProjectCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var project = await _mediator.Send(command, cancellationToken);
            return CreatedAtAction(nameof(GetProjectById), new { id = project.Id }, project);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }

    //PUT : api/projects/{id}
    [HttpPut("{id}")]
    public async Task<ActionResult<ProjectDto>> UpdateProject(Guid id, [FromBody] UpdateProjectCommand command, CancellationToken cancellationToken)
    {
        if (id != command.Id)
        {
            return BadRequest();
        }
        try
        {
            var project = await _mediator.Send(command, cancellationToken);
            return Ok(project);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

}