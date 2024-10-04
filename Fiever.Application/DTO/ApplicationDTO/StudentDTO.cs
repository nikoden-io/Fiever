// -----------------------------------------------------------------------
// <copyright>
//     Copyright 2024, Nikoden.IO All rights reserved.
//     Author: Nicolas Denoël
// </copyright>
// -----------------------------------------------------------------------

using System.ComponentModel.DataAnnotations;

namespace Fiever.Application.DTO.ApplicationDTO;

public class StudentDTO
{
    public string? Id { get; set; }

    [Required] public string FirstName { get; set; } = string.Empty;

    [Required] public string LastName { get; set; } = string.Empty;

    [Required] public string Email { get; set; } = string.Empty;

    // One-to-Many Relationship with Country
    public string? CountryId { get; set; }
    public CountryDTO? Country { get; set; }

    // One-to-One Relationship with Passport
    public PassportDTO? Passport { get; set; }

    // One-to-Many Relationship with University
    public string? UniversityId { get; set; }
    public UniversityDTO? University { get; set; }

    // Many-to-Many Relationship with Courses
    public List<CourseDTO> Courses { get; set; } = new();
}

public class StudentsResponseDTO
{
    public List<StudentDTO> Students { get; set; }
}

public class StudentResponseDTO
{
    public StudentDTO Student { get; set; }
}

public class CreateStudentDTO
{
    [Required] public string FirstName { get; set; } = string.Empty;
    [Required] public string LastName { get; set; } = string.Empty;
    [Required] public string Email { get; set; } = string.Empty;
    public string? CountryId { get; set; }
    public string? UniversityId { get; set; }
    public string? PassportNumber { get; set; }
    public List<string>? CourseIds { get; set; } = new();
}