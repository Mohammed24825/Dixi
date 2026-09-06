using Dixi.Application.Common;
using Dixi.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Dixi.Application.Admin.Users.Queries;

public record TaughtCourseDto(Guid CourseId, string Title, bool IsRemoved);
public record InstructorFeedbackDto(string StudentName, string SessionTitle, int Rating, string? Comment);

public record InstructorProfileDto(
    Guid Id, string FullName, string? Email, string? PhoneNumber, string? Title, string? Bio, bool IsSuspended,
    List<TaughtCourseDto> Courses,
    List<InstructorFeedbackDto> Feedback
);

public record GetInstructorProfileQuery(Guid InstructorId) : IRequest<InstructorProfileDto>;

public class GetInstructorProfileHandler(IDixiDbContext db) : IRequestHandler<GetInstructorProfileQuery, InstructorProfileDto>
{
    public async Task<InstructorProfileDto> Handle(GetInstructorProfileQuery request, CancellationToken ct)
    {
        var instructor = await db.Users.FirstOrDefaultAsync(u => u.Id == request.InstructorId && u.Role == UserRole.Instructor, ct)
            ?? throw new KeyNotFoundException("Instructor not found.");

        var courses = await db.Courses
            .Where(c => c.InstructorId == request.InstructorId)
            .Select(c => new TaughtCourseDto(c.Id, c.Title, c.IsRemoved))
            .ToListAsync(ct);

        var feedback = await db.SessionFeedbacks
            .Where(f => f.Session.Course.InstructorId == request.InstructorId)
            .Select(f => new InstructorFeedbackDto(f.Student.FullName, f.Session.Title, f.Rating, f.Comment))
            .ToListAsync(ct);

        return new InstructorProfileDto(instructor.Id, instructor.FullName, instructor.Email, instructor.PhoneNumber,
            instructor.Title, instructor.Bio, instructor.IsSuspended, courses, feedback);
    }
}