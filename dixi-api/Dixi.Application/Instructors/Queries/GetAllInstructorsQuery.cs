using Dixi.Application.Common;
using Dixi.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Dixi.Application.Instructors.Queries;

public record InstructorDto(Guid Id, string FullName, string? Title, string? Bio, int CoursesCount);

public record GetAllInstructorsQuery : IRequest<List<InstructorDto>>;

public class GetAllInstructorsHandler(IDixiDbContext db) : IRequestHandler<GetAllInstructorsQuery, List<InstructorDto>>
{
    public async Task<List<InstructorDto>> Handle(GetAllInstructorsQuery request, CancellationToken ct)
    {
        return await db.Users
            .Where(u => u.Role == UserRole.Instructor)
            .Select(u => new InstructorDto(
                u.Id,
                u.FullName,
                u.Title,
                u.Bio,
                u.CoursesTaught.Count
            ))
            .ToListAsync(ct);
    }
}