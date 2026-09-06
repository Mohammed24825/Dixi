using Dixi.Application.Auth.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Dixi.Api.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController(IMediator mediator) : ControllerBase
{
    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginCommand command)
    {
        var result = await mediator.Send(command);
        if (result is null) return Unauthorized(new { message = "Invalid credentials" });
        return Ok(result);
    }
}