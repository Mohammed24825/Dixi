namespace Dixi.Domain.Entities;

public class Course
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int DurationWeeks { get; set; }
    public string Level { get; set; } = "Beginner";
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public Guid InstructorId { get; set; }
    public AppUser Instructor { get; set; } = null!;
    public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();

    public bool IsRemoved { get; set; } = false;
    public ICollection<Session> Sessions { get; set; } = new List<Session>();
}