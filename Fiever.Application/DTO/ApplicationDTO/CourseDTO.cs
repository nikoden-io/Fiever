// -----------------------------------------------------------------------
// <copyright>
//     Copyright 2024, Nikoden.IO All rights reserved.
//     Author: Nicolas Denoël
// </copyright>
// -----------------------------------------------------------------------

using System.ComponentModel.DataAnnotations;

namespace Fiever.Application.DTO.ApplicationDTO;

public class CourseDTO
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Code { get; set; }
}

public class CreateCourseDTO
{
    [Required] public string Name { get; set; } = string.Empty;
    [Required] public string Code { get; set; } = string.Empty;
}