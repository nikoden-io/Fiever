// -----------------------------------------------------------------------
// <copyright>
//     Copyright 2024, Nikoden.IO All rights reserved.
//     Author: Nicolas Denoël
// </copyright>
// -----------------------------------------------------------------------

using Fiever.Application.DTO.ApplicationDTO;

namespace Fiever.Application.Interfaces.ApplicationServices;

/// <summary>
///     Provides methods for managing and processing students.
/// </summary>
public interface IStudentAppService
{
    /// <summary>
    ///     Create a student in database
    /// </summary>
    /// <param name="studentDto">The DTO that represent all data needed to create Student entity.</param>
    /// <returns>A task that returns an ApiResponse indicating the success or failure of adding process.</returns>
    Task<StudentDTO> AddStudentAsync(CreateStudentDTO studentDto);

    /// <summary>
    ///     Fetch a student in database
    /// </summary>
    /// <param name="id">ID of student to fetch data.</param>
    /// <returns>A task that returns an ApiResponse indicating the success or failure of creation process.</returns>
    Task<StudentDTO?> GetStudentByIdAsync(string id);

    /// <summary>
    ///     Fetch all students from database
    /// </summary>
    /// <returns>A task that returns an ApiResponse indicating the success or failure of fetching.</returns>
    Task<List<StudentDTO>> GetStudentsAsync();

    /// <summary>
    ///     Remove a student from database
    /// </summary>
    /// <param name="id">ID of student to remove.</param>
    /// <returns>A task that returns an ApiResponse indicating the success or failure of removing process.</returns>
    Task<bool> RemoveStudentAsync(string id);

    /// <summary>
    ///     Update a student in database
    /// </summary>
    /// <param name="id">ID of student to update.</param>
    /// <param name="studentDto">The DTO that represent all data needed to update Student entity.</param>
    /// <returns>A task that returns an ApiResponse indicating the success or failure of update process.</returns>
    Task<StudentDTO?> UpdateStudentAsync(string id, StudentDTO studentDto);
}