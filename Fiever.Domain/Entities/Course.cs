namespace Fiever.Domain.Entities;

public class Course
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Code { get; set; }

    // Many-to-Many relationship with Students
    public ICollection<StudentCourse> StudentCourses { get; set; } = new List<StudentCourse>();
}