using Dixi.Domain.Entities;
using Dixi.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Dixi.Infrastructure.Persistence;

public static class DixiDbSeeder
{
    public static async Task SeedAsync(DixiDbContext db)
    {
        if (await db.Users.AnyAsync()) return; // already seeded

        var admin = new AppUser
        {
            FullName = "Mohammed Admin",
            Email = "admin@dixi.dev",
            PhoneNumber = "01000000001",
            PasswordHash = BCrypt.Net.BCrypt.HashPassword("Admin@123"),
            Role = UserRole.Admin
        };

        var instructor = new AppUser
        {
            FullName = "Sara Ahmed",
            Email = "sara.instructor@dixi.dev",
            PhoneNumber = "01000000002",
            PasswordHash = BCrypt.Net.BCrypt.HashPassword("Instructor@123"),
            Role = UserRole.Instructor,
            Title = "Frontend Lead",
            Bio = "Angular & UI specialist with 6+ years teaching web development."
        };

        var student = new AppUser
        {
            FullName = "Omar Student",
            Email = "omar.student@dixi.dev",
            PhoneNumber = "01000000003",
            PasswordHash = BCrypt.Net.BCrypt.HashPassword("Student@123"),
            Role = UserRole.Student
        };

        db.Users.AddRange(admin, instructor, student);
        await db.SaveChangesAsync();

        var courses = new List<Course>
        {
            new()
            {
                Title = "Frontend with Angular",
                Description = "Build modern, reactive UIs with Angular 20, signals, and standalone components.",
                Price = 149.99m,
                DurationWeeks = 8,
                Level = "Beginner",
                InstructorId = instructor.Id
            },
            new()
            {
                Title = "Backend with .NET",
                Description = "APIs, EF Core, and Clean Architecture using ASP.NET Core.",
                Price = 179.99m,
                DurationWeeks = 10,
                Level = "Intermediate",
                InstructorId = instructor.Id
            },
            new()
            {
                Title = "Full-Stack Path",
                Description = "Ship complete production apps end-to-end, frontend to backend to deployment.",
                Price = 299.99m,
                DurationWeeks = 16,
                Level = "Advanced",
                InstructorId = instructor.Id
            }
        };

        db.Courses.AddRange(courses);
        await db.SaveChangesAsync();

        var enrollment = new Enrollment
        {
            StudentId = student.Id,
            CourseId = courses[0].Id
        };
        db.Enrollments.Add(enrollment);
        await db.SaveChangesAsync();
    }
}