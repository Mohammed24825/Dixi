using Dixi.Application.Common;
using Dixi.Domain.Entities;
using MediatR;

namespace Dixi.Application.Courses.Commands;

public record CreateCourseCommand(string Title, string Description, decimal Price, int DurationWeeks, string Level, Guid InstructorId) : IRequest<Guid>;

public class CreateCourseHandler(IDixiDbContext db) : IRequestHandler<CreateCourseCommand, Guid>
{
    public async Task<Guid> Handle(CreateCourseCommand request, CancellationToken ct)
    {
        var course = new Course
        {
            Title = request.Title,
            Description = request.Description,
            Price = request.Price,
            DurationWeeks = request.DurationWeeks,
            Level = request.Level,
            InstructorId = request.InstructorId
        };
        db.Courses.Add(course);
        await db.SaveChangesAsync(ct);
        return course.Id;
    }
}