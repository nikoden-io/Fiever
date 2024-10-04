// -----------------------------------------------------------------------
// <copyright>
//     Copyright 2024, Nikoden.IO All rights reserved.
//     Author: Nicolas Denoël
// </copyright>
// -----------------------------------------------------------------------

namespace Fiever.Application.Interfaces.ApplicationServices;

/// <summary>
///     Represents the application service for managing the many-to-many relationship between students and courses.
/// </summary>
public interface IStudentCourseAppService
{
    /// <summary>
    ///     Adds a student to a course in the many-to-many relationship.
    /// </summary>
    /// <param name="studentId">The identifier of the student.</param>
    /// <param name="courseId">The identifier of the course.</param>
    Task AddStudentToCourseAsync(string studentId, string courseId);

    /// <summary>
    ///     Removes a student from a course in the many-to-many relationship.
    /// </summary>
    /// <param name="studentId">The identifier of the student.</param>
    /// <param name="courseId">The identifier of the course.</param>
    Task RemoveStudentFromCourseAsync(string studentId, string courseId);

    /// <summary>
    ///     Retrieves a list of course IDs associated with a specific student.
    /// </summary>
    /// <param name="studentId">The identifier of the student.</param>
    /// <returns>A list of course IDs associated with the student.</returns>
    Task<List<string>> GetCoursesByStudentIdAsync(string studentId);

    /// <summary>
    ///     Retrieves a list of student IDs associated with a specific course.
    /// </summary>
    /// <param name="courseId">The identifier of the course.</param>
    /// <returns>A list of student IDs associated with the course.</returns>
    Task<List<string>> GetStudentsByCourseIdAsync(string courseId);
}