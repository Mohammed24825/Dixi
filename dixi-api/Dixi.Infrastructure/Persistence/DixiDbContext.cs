using Dixi.Application.Common;
using Dixi.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Dixi.Infrastructure.Persistence;

public class DixiDbContext(DbContextOptions<DixiDbContext> options) : DbContext(options), IDixiDbContext
{
    public DbSet<AppUser> Users => Set<AppUser>();
    public DbSet<Course> Courses => Set<Course>();
    public DbSet<Enrollment> Enrollments => Set<Enrollment>();
    public DbSet<Session> Sessions => Set<Session>();
    public DbSet<StudentSessionGrade> StudentSessionGrades => Set<StudentSessionGrade>();
    public DbSet<SessionFeedback> SessionFeedbacks => Set<SessionFeedback>();
    public DbSet<ActivityLog> ActivityLogs => Set<ActivityLog>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AppUser>().HasIndex(u => u.Email).IsUnique();
        modelBuilder.Entity<AppUser>().HasIndex(u => u.PhoneNumber).IsUnique();

        modelBuilder.Entity<Course>()
            .HasOne(c => c.Instructor)
            .WithMany(u => u.CoursesTaught)
            .HasForeignKey(c => c.InstructorId);

        modelBuilder.Entity<Enrollment>()
            .HasOne(e => e.Student).WithMany(u => u.Enrollments).HasForeignKey(e => e.StudentId);
        modelBuilder.Entity<Enrollment>()
            .HasOne(e => e.Course).WithMany(c => c.Enrollments).HasForeignKey(e => e.CourseId);

        modelBuilder.Entity<Session>()
            .HasOne(s => s.Course)
            .WithMany(c => c.Sessions)
            .HasForeignKey(s => s.CourseId);

        modelBuilder.Entity<StudentSessionGrade>()
            .HasOne(g => g.Session)
            .WithMany(s => s.Grades)
            .HasForeignKey(g => g.SessionId);
        modelBuilder.Entity<StudentSessionGrade>()
            .HasOne(g => g.Student)
            .WithMany(u => u.SessionGrades)
            .HasForeignKey(g => g.StudentId);
        // one grade row per student per session
        modelBuilder.Entity<StudentSessionGrade>()
            .HasIndex(g => new { g.SessionId, g.StudentId })
            .IsUnique();

        modelBuilder.Entity<SessionFeedback>()
            .HasOne(f => f.Session)
            .WithMany(s => s.Feedbacks)
            .HasForeignKey(f => f.SessionId);
        modelBuilder.Entity<SessionFeedback>()
            .HasOne(f => f.Student)
            .WithMany(u => u.FeedbacksGiven)
            .HasForeignKey(f => f.StudentId);

        modelBuilder.Entity<ActivityLog>()
            .HasOne(l => l.User)
            .WithMany(u => u.Logs)
            .HasForeignKey(l => l.UserId);


        foreach (var fk in modelBuilder.Model.GetEntityTypes()
                     .SelectMany(e => e.GetForeignKeys())
                     .Where(fk => fk.PrincipalEntityType.ClrType == typeof(AppUser)))
        {
            fk.DeleteBehavior = DeleteBehavior.Restrict;
        }
    }
}