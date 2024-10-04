namespace Fiever.Domain.Entities;

// Associative entity for many-to-many relationship between Student and Course
public class StudentCourse
{
    public string StudentId { get; set; }
    public Student Student { get; set; }

    public string CourseId { get; set; }
    public Course Course { get; set; }
}