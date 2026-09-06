using Dixi.Application.Common;
using Dixi.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Dixi.Application.Admin.Users.Queries;

public record EnrolledCourseDto(Guid CourseId, string Title, bool IsRemoved);
public record SessionGradeDto(Guid SessionId, string SessionTitle, string CourseTitle, bool SessionRemoved,
    decimal? AttendanceScore, decimal? QuizScore, decimal? TaskScore);
public record LogEntryDto(string Action, DateTime Timestamp);

public record StudentProfileDto(
    Guid Id, string FullName, string? Email, string? PhoneNumber, bool IsSuspended,
    List<EnrolledCourseDto> Courses,
    List<SessionGradeDto> Grades,
    List<LogEntryDto> Logs
);

public record GetStudentProfileQuery(Guid StudentId) : IRequest<StudentProfileDto>;

public class GetStudentProfileHandler(IDixiDbContext db) : IRequestHandler<GetStudentProfileQuery, StudentProfileDto>
{
    public async Task<StudentProfileDto> Handle(GetStudentProfileQuery request, CancellationToken ct)
    {
        var student = await db.Users.FirstOrDefaultAsync(u => u.Id == request.StudentId && u.Role == UserRole.Student, ct)
            ?? throw new KeyNotFoundException("Student not found.");

        var courses = await db.Enrollments
            .Where(e => e.StudentId == request.StudentId)
            .Select(e => new EnrolledCourseDto(e.CourseId, e.Course.Title, e.IsRemoved || e.Course.IsRemoved))
            .ToListAsync(ct);

        var grades = await db.StudentSessionGrades
            .Where(g => g.StudentId == request.StudentId)
            .Select(g => new SessionGradeDto(
                g.SessionId, g.Session.Title, g.Session.Course.Title, g.Session.IsRemoved,
                g.AttendanceScore, g.QuizScore, g.TaskScore))
            .ToListAsync(ct);

        var logs = await db.ActivityLogs
            .Where(l => l.UserId == request.StudentId)
            .OrderByDescending(l => l.Timestamp)
            .Select(l => new LogEntryDto(l.Action, l.Timestamp))
            .ToListAsync(ct);

        return new StudentProfileDto(student.Id, student.FullName, student.Email, student.PhoneNumber,
            student.IsSuspended, courses, grades, logs);
    }
}