using Dixi.Application.Instructors.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Dixi.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class InstructorsController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll() => Ok(await mediator.Send(new GetAllInstructorsQuery()));
}