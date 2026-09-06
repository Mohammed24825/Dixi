using Dixi.Application.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Dixi.Application.Courses.Queries;

public record CourseDto(Guid Id, string Title, string Description, decimal Price, int DurationWeeks, string Level, string InstructorName);

public record GetAllCoursesQuery : IRequest<List<CourseDto>>;

public class GetAllCoursesHandler(IDixiDbContext db) : IRequestHandler<GetAllCoursesQuery, List<CourseDto>>
{
    public async Task<List<CourseDto>> Handle(GetAllCoursesQuery request, CancellationToken ct)
    {
        return await db.Courses
            .Include(c => c.Instructor)
            .Select(c => new CourseDto(c.Id, c.Title, c.Description, c.Price, c.DurationWeeks, c.Level, c.Instructor.FullName))
            .ToListAsync(ct);
    }
}