using Dixi.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Dixi.Application.Common;

public interface IDixiDbContext
{
    DbSet<AppUser> Users { get; }
    DbSet<Course> Courses { get; }
    DbSet<Enrollment> Enrollments { get; }
    DbSet<Session> Sessions { get; }
    DbSet<StudentSessionGrade> StudentSessionGrades { get; }
    DbSet<SessionFeedback> SessionFeedbacks { get; }
    DbSet<ActivityLog> ActivityLogs { get; }
    Task<int> SaveChangesAsync(CancellationToken ct = default);
}