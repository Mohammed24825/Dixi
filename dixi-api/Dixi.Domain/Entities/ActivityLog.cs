namespace Dixi.Domain.Entities;

public class ActivityLog
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid UserId { get; set; }
    public AppUser User { get; set; } = null!;
    public string Action { get; set; } = string.Empty; // e.g. "Login", "Enrolled in course", "Grade updated"
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
}