using Dixi.Application.Common;
using Dixi.Domain.Enums;
using MediatR;

namespace Dixi.Application.Admin.Users.Commands;

public record SuspendUserCommand(Guid UserId) : IRequest<bool>;

public class SuspendUserHandler(IDixiDbContext db) : IRequestHandler<SuspendUserCommand, bool>
{
    public async Task<bool> Handle(SuspendUserCommand request, CancellationToken ct)
    {
        var user = await db.Users.FindAsync([request.UserId], ct)
            ?? throw new KeyNotFoundException("User not found.");

        if (user.Role == UserRole.Admin)
            throw new InvalidOperationException("Admins cannot be suspended.");

        user.IsSuspended = !user.IsSuspended;
        await db.SaveChangesAsync(ct);
        return user.IsSuspended;
    }
}