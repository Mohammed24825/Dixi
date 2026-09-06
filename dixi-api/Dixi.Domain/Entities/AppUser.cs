using Dixi.Domain.Enums;

namespace Dixi.Domain.Entities;

public class AppUser
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string FullName { get; set; } = string.Empty;
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
    public string PasswordHash { get; set; } = string.Empty;
    public UserRole Role { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public string? Bio { get; set; }
    public string? Title { get; set; }

    public ICollection<Course> CoursesTaught { get; set; } = new List<Course>();
    public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();

    public bool IsSuspended { get; set; } = false;
    public ICollection<ActivityLog> Logs { get; set; } = new List<ActivityLog>();
    public ICollection<SessionFeedback> FeedbacksGiven { get; set; } = new List<SessionFeedback>();
    public ICollection<StudentSessionGrade> SessionGrades { get; set; } = new List<StudentSessionGrade>();

}