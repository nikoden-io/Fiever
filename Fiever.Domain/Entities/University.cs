namespace Fiever.Domain.Entities;

public class University
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }

    // One-to-Many relationship with Students
    public ICollection<Student> Students { get; set; } = new List<Student>();
}