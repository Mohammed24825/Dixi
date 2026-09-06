using Dixi.Application.Common;
using Dixi.Domain.Entities;
using Dixi.Domain.Enums;
using MediatR;

namespace Dixi.Application.Admin.Users.Commands;

public record CreateUserCommand(
    string FullName, string? Email, string? PhoneNumber, string Password, string Role,
    string? Title, string? Bio
) : IRequest<Guid>;

public class CreateUserHandler(IDixiDbContext db) : IRequestHandler<CreateUserCommand, Guid>
{
    public async Task<Guid> Handle(CreateUserCommand request, CancellationToken ct)
    {
        if (!Enum.TryParse<UserRole>(request.Role, true, out var role))
            throw new ArgumentException("Invalid role.");

        var user = new AppUser
        {
            FullName = request.FullName,
            Email = request.Email,
            PhoneNumber = request.PhoneNumber,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
            Role = role,
            Title = request.Title,
            Bio = request.Bio
        };

        db.Users.Add(user);
        await db.SaveChangesAsync(ct);
        return user.Id;
    }
}