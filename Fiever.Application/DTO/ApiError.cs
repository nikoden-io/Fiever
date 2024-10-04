// -----------------------------------------------------------------------
// <copyright>
//     Copyright 2024, Nikoden.IO All rights reserved.
//     Author: Nicolas Denoël
// </copyright>
// -----------------------------------------------------------------------

namespace Application.DTO;

/// <summary>
///     Represents an error detail in an API response.
/// </summary>
public class ApiError
{
    /// <summary>
    ///     Gets or sets the error code.
    /// </summary>
    public int Code { get; set; } = 500;

    /// <summary>
    ///     Gets or sets the error type.
    /// </summary>
    public string Type { get; set; } = "Unknown type error";

    /// <summary>
    ///     Gets or sets the error description.
    /// </summary>
    public string Description { get; set; } = "No description available";
}