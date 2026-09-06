using Dixi.Application.Common;
using MediatR;

namespace Dixi.Application.Admin.Users.Commands;

public record UpdateUserCommand(
    Guid Id, string FullName, string? Email, string? PhoneNumber, string? Title, string? Bio
) : IRequest;

public class UpdateUserHandler(IDixiDbContext db) : IRequestHandler<UpdateUserCommand>
{
    public async Task Handle(UpdateUserCommand request, CancellationToken ct)
    {
        var user = await db.Users.FindAsync([request.Id], ct)
            ?? throw new KeyNotFoundException("User not found.");

        user.FullName = request.FullName;
        user.Email = request.Email;
        user.PhoneNumber = request.PhoneNumber;
        user.Title = request.Title;
        user.Bio = request.Bio;

        await db.SaveChangesAsync(ct);
    }
}