using Fiever.Application.DTO.ApplicationDTO;

namespace Fiever.Application.Interfaces.ApplicationServices;

/// <summary>
///     Represents the application service for managing university-related operations.
/// </summary>
public interface IUniversityAppService
{
    /// <summary>
    ///     Adds a new university to the system.
    /// </summary>
    /// <param name="universityDto">The data transfer object containing the details of the university to add.</param>
    /// <returns>The added university as a data transfer object.</returns>
    Task<UniversityDTO> AddUniversityAsync(CreateUniversityDTO universityDto);

    /// <summary>
    ///     Retrieves a university by its identifier.
    /// </summary>
    /// <param name="id">The identifier of the university.</param>
    /// <returns>The university as a data transfer object if found, otherwise null.</returns>
    Task<UniversityDTO?> GetUniversityByIdAsync(string id);

    /// <summary>
    ///     Retrieves all universities in the system.
    /// </summary>
    /// <returns>A list of all universities as data transfer objects.</returns>
    Task<List<UniversityDTO>> GetUniversitiesAsync();

    /// <summary>
    ///     Removes a university from the system by its identifier.
    /// </summary>
    /// <param name="id">The identifier of the university to remove.</param>
    /// <returns>True if the university was removed, otherwise false.</returns>
    Task<bool> RemoveUniversityAsync(string id);

    /// <summary>
    ///     Updates an existing university in the system.
    /// </summary>
    /// <param name="id">The identifier of the university to update.</param>
    /// <param name="universityDto">The data transfer object containing the updated details of the university.</param>
    /// <returns>The updated university as a data transfer object if the update was successful, otherwise null.</returns>
    Task<UniversityDTO?> UpdateUniversityAsync(string id, UniversityDTO universityDto);
}