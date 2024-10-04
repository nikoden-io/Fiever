// -----------------------------------------------------------------------
// <copyright>
//     Copyright 2024, Nikoden.IO All rights reserved.
//     Author: Nicolas Denoël
// </copyright>
// -----------------------------------------------------------------------

namespace Fiever.Domain.Entities;

public class Student
{
    public string Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string? Email { get; set; }

    public string? CountryId { get; set; }
    public Country? Country { get; set; } // Navigation property to Country (one-to-many)

    // One-to-One relationship with Passport
    public Passport? Passport { get; set; }

    // One-to-Many relationship with University
    public string? UniversityId { get; set; }
    public University? University { get; set; }

    // Many-to-Many relationship with Courses
    public ICollection<StudentCourse> StudentCourses { get; set; } = new List<StudentCourse>();
}