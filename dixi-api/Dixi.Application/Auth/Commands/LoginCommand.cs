using Dixi.Application.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Dixi.Application.Auth.Commands;

public record LoginResult(string Token, Guid UserId, string FullName, string Role);

public record LoginCommand(string Identifier, string Password) : IRequest<LoginResult?>;

public class LoginHandler(IDixiDbContext db, IJwtTokenGenerator jwt) : IRequestHandler<LoginCommand, LoginResult?>
{
    public async Task<LoginResult?> Handle(LoginCommand request, CancellationToken ct)
    {
        var user = await db.Users.FirstOrDefaultAsync(
            u => u.Email == request.Identifier || u.PhoneNumber == request.Identifier, ct);

        if (user is null) return null;
        if (!BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash)) return null;

        var token = jwt.GenerateToken(user);
        return new LoginResult(token, user.Id, user.FullName, user.Role.ToString());
    }
}