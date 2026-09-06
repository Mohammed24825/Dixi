namespace Dixi.Domain.Entities;

public class StudentSessionGrade
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid SessionId { get; set; }
    public Session Session { get; set; } = null!;
    public Guid StudentId { get; set; }
    public AppUser Student { get; set; } = null!;

    public decimal? AttendanceScore { get; set; }
    public decimal? QuizScore { get; set; }
    public decimal? TaskScore { get; set; }

    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}