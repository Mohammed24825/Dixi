namespace Dixi.Domain.Entities;

public class Session
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid CourseId { get; set; }
    public Course Course { get; set; } = null!;

    public string Title { get; set; } = string.Empty;
    public DateTime ScheduledAt { get; set; }

    public bool HasAttendanceGrade { get; set; }
    public bool HasQuizGrade { get; set; }
    public bool HasTaskGrade { get; set; }
    public decimal? MaxAttendanceScore { get; set; }
    public decimal? MaxQuizScore { get; set; }
    public decimal? MaxTaskScore { get; set; }

    public bool IsRemoved { get; set; } = false;

    public ICollection<StudentSessionGrade> Grades { get; set; } = new List<StudentSessionGrade>();
    public ICollection<SessionFeedback> Feedbacks { get; set; } = new List<SessionFeedback>();
}