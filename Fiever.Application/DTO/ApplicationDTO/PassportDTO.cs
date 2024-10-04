// -----------------------------------------------------------------------
// <copyright>
//     Copyright 2024, Nikoden.IO All rights reserved.
//     Author: Nicolas Denoël
// </copyright>
// -----------------------------------------------------------------------

using System.ComponentModel.DataAnnotations;

namespace Fiever.Application.DTO.ApplicationDTO;

public class PassportDTO
{
    public string Id { get; set; }
    public string Number { get; set; }
    public string StudentId { get; set; }
}

public class CreatePassportDTO
{
    [Required] public string Number { get; set; } = string.Empty;
    [Required] public string StudentId { get; set; } = string.Empty;
}