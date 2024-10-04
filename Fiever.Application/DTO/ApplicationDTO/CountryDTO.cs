// -----------------------------------------------------------------------
// <copyright>
//     Copyright 2024, Nikoden.IO All rights reserved.
//     Author: Nicolas Denoël
// </copyright>
// -----------------------------------------------------------------------

using System.ComponentModel.DataAnnotations;

namespace Fiever.Application.DTO.ApplicationDTO;

public class CountryDTO
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Code { get; set; }
}

public class CountryWithStudentsDTO
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Code { get; set; }
    public List<StudentDTO> Students { get; set; } = new();
}

public class CreateCountryDTO
{
    [Required] public string Name { get; set; } = string.Empty;
    [Required] public string Code { get; set; } = string.Empty;
}