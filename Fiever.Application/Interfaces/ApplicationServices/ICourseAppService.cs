using Fiever.Application.DTO.ApplicationDTO;

namespace Fiever.Application.Interfaces.ApplicationServices;

/// <summary>
///     Represents the application service for managing course-related operations.
/// </summary>
public interface ICourseAppService
{
    /// <summary>
    ///     Adds a new course to the system.
    /// </summary>
    /// <param name="courseDto">The data transfer object containing the details of the course to add.</param>
    /// <returns>The added course as a data transfer object.</returns>
    Task<CourseDTO> AddCourseAsync(CreateCourseDTO courseDto);

    /// <summary>
    ///     Retrieves a course by its identifier.
    /// </summary>
    /// <param name="id">The identifier of the course.</param>
    /// <returns>The course as a data transfer object if found, otherwise null.</returns>
    Task<CourseDTO?> GetCourseByIdAsync(string id);

    /// <summary>
    ///     Retrieves all courses in the system.
    /// </summary>
    /// <returns>A list of all courses as data transfer objects.</returns>
    Task<List<CourseDTO>> GetCoursesAsync();

    /// <summary>
    ///     Removes a course from the system by its identifier.
    /// </summary>
    /// <param name="id">The identifier of the course to remove.</param>
    /// <returns>True if the course was removed, otherwise false.</returns>
    Task<bool> RemoveCourseAsync(string id);

    /// <summary>
    ///     Updates an existing course in the system.
    /// </summary>
    /// <param name="id">The identifier of the course to update.</param>
    /// <param name="courseDto">The data transfer object containing the updated details of the course.</param>
    /// <returns>The updated course as a data transfer object if the update was successful, otherwise null.</returns>
    Task<CourseDTO?> UpdateCourseAsync(string id, CourseDTO courseDto);
}