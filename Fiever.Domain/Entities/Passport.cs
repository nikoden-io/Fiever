namespace Fiever.Domain.Entities;

public class Passport
{
    public string Id { get; set; }
    public string Number { get; set; }

    // One-to-One relationship with Student
    public string StudentId { get; set; }
    public Student Student { get; set; }
}