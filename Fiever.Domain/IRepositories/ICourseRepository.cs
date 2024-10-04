// -----------------------------------------------------------------------
// <copyright>
//     Copyright 2024, Nikoden.IO All rights reserved.
//     Author: Nicolas Denoël
// </copyright>
// -----------------------------------------------------------------------

using Fiever.Domain.Entities;

namespace Fiever.Domain.IRepositories;

public interface ICourseRepository
{
    /// <summary>
    ///     Adds a new course to the repository.
    /// </summary>
    /// <param name="course">The course entity to add.</param>
    Task AddCourseAsync(Course course);

    /// <summary>
    ///     Retrieves a course by its identifier.
    /// </summary>
    /// <param name="courseId">The identifier of the course.</param>
    /// <returns>The course entity if found, otherwise null.</returns>
    Task<Course?> GetCourseByIdAsync(string courseId);

    /// <summary>
    ///     Retrieves all courses from the repository.
    /// </summary>
    /// <returns>A list of all courses.</returns>
    Task<List<Course>> GetCoursesAsync();

    /// <summary>
    ///     Removes a course from the repository by its identifier.
    /// </summary>
    /// <param name="id">The identifier of the course to remove.</param>
    /// <returns>True if the course was removed, otherwise false.</returns>
    Task<bool> RemoveCourseAsync(string id);

    /// <summary>
    ///     Updates an existing course in the repository.
    /// </summary>
    /// <param name="course">The course entity with updated information.</param>
    Task UpdateCourseAsync(Course course);

    /// <summary>
    ///     Retrieves a list of courses associated with a specific student.
    /// </summary>
    /// <param name="studentId">The identifier of the student.</param>
    /// <returns>A list of courses associated with the student.</returns>
    Task<List<Course>> GetCoursesByStudentIdAsync(string studentId);
}