// -----------------------------------------------------------------------
// <copyright>
//     Copyright 2024, Nikoden.IO All rights reserved.
//     Author: Nicolas Denoël
// </copyright>
// -----------------------------------------------------------------------

using System.ComponentModel.DataAnnotations;

namespace Fiever.Application.DTO.ApplicationDTO;

public class UniversityDTO
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
}

public class CreateUniversityDTO
{
    [Required] public string Name { get; set; } = string.Empty;
    [Required] public string Address { get; set; } = string.Empty;
}