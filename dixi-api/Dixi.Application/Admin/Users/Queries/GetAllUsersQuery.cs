using Dixi.Application.Common;
using Dixi.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Dixi.Application.Admin.Users.Queries;

public record AdminUserDto(Guid Id, string FullName, string? Email, string? PhoneNumber, string Role, bool IsSuspended);

public record GetAllUsersQuery(string? Role) : IRequest<List<AdminUserDto>>;

public class GetAllUsersHandler(IDixiDbContext db) : IRequestHandler<GetAllUsersQuery, List<AdminUserDto>>
{
    public async Task<List<AdminUserDto>> Handle(GetAllUsersQuery request, CancellationToken ct)
    {
        var query = db.Users.AsQueryable();

        if (!string.IsNullOrWhiteSpace(request.Role) &&
            Enum.TryParse<UserRole>(request.Role, true, out var role))
        {
            query = query.Where(u => u.Role == role);
        }

        return await query
            .Select(u => new AdminUserDto(u.Id, u.FullName, u.Email, u.PhoneNumber, u.Role.ToString(), u.IsSuspended))
            .ToListAsync(ct);
    }
}