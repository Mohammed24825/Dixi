using Dixi.Application.Admin.Users.Commands;
using Dixi.Application.Admin.Users.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Dixi.Api.Controllers;

[ApiController]
[Route("api/admin/users")]
[Authorize(Roles = "Admin")]
public class AdminUsersController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] string? role) =>
        Ok(await mediator.Send(new GetAllUsersQuery(role)));

    [HttpPost]
    public async Task<IActionResult> Create(CreateUserCommand command) =>
        Ok(await mediator.Send(command));

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, UpdateUserCommand command)
    {
        if (id != command.Id) return BadRequest();
        await mediator.Send(command);
        return NoContent();
    }

    [HttpPost("{id}/suspend")]
    public async Task<IActionResult> Suspend(Guid id) =>
        Ok(await mediator.Send(new SuspendUserCommand(id)));

    [HttpGet("{id}/student-profile")]
    public async Task<IActionResult> GetStudentProfile(Guid id) =>
        Ok(await mediator.Send(new GetStudentProfileQuery(id)));

    [HttpGet("{id}/instructor-profile")]
    public async Task<IActionResult> GetInstructorProfile(Guid id) =>
        Ok(await mediator.Send(new GetInstructorProfileQuery(id)));
}