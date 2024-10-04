// -----------------------------------------------------------------------
// <copyright>
//     Copyright 2024, Nikoden.IO All rights reserved.
//     Author: Nicolas Denoël
// </copyright>
// -----------------------------------------------------------------------

namespace Application.DTO;

/// <summary>
///     Represents a standardized API response with data and message.
/// </summary>
/// <typeparam name="T">The type of data contained in the response.</typeparam>
public class ApiResponse<T>
{
    /// <summary>
    ///     Gets or sets a value indicating whether the response indicates a successful operation.
    /// </summary>
    public bool Success { get; set; }

    /// <summary>
    ///     Gets or sets the data contained in the response.
    /// </summary>
    public T? Data { get; set; }

    /// <summary>
    ///     Gets or sets the message accompanying the response.
    /// </summary>
    public string Message { get; set; } = string.Empty;

    /// <summary>
    ///     Creates a successful response with the specified data and an optional message.
    /// </summary>
    /// <param name="data">The data to include in the response.</param>
    /// <param name="message">An optional message to accompany the response.</param>
    /// <returns>A successful <see cref="ApiResponse{T}" /> containing the specified data and message.</returns>
    public static ApiResponse<T> SuccessResponse(T? data, string message)
    {
        return new ApiResponse<T> { Success = true, Data = data, Message = message };
    }

    /// <summary>
    ///     Creates an error response with the specified message.
    /// </summary>
    /// <param name="message">The message describing the error.</param>
    /// <returns>An error <see cref="ApiResponse{T}" /> containing the specified message.</returns>
    public static ApiResponse<T> ErrorResponse(string message)
    {
        return new ApiResponse<T> { Success = false, Message = message };
    }
}