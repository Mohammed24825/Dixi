using Dixi.Application.Courses.Commands;
using Dixi.Application.Courses.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Dixi.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CoursesController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll() => Ok(await mediator.Send(new GetAllCoursesQuery()));

    [HttpPost]
    public async Task<IActionResult> Create(CreateCourseCommand command)
    {
        var id = await mediator.Send(command);
        return CreatedAtAction(nameof(GetAll), new { id });
    }
}