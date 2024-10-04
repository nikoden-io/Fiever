using Fiever.Application.DTO.ApplicationDTO;

namespace Fiever.Application.Interfaces.ApplicationServices;

/// <summary>
///     Represents the application service for managing passport-related operations.
/// </summary>
public interface IPassportAppService
{
    /// <summary>
    ///     Adds a new passport to the system.
    /// </summary>
    /// <param name="passportDto">The data transfer object containing the details of the passport to add.</param>
    /// <returns>The added passport as a data transfer object.</returns>
    Task<PassportDTO> AddPassportAsync(CreatePassportDTO passportDto);

    /// <summary>
    ///     Retrieves a passport by its identifier.
    /// </summary>
    /// <param name="id">The identifier of the passport.</param>
    /// <returns>The passport as a data transfer object if found, otherwise null.</returns>
    Task<PassportDTO?> GetPassportByIdAsync(string id);

    /// <summary>
    ///     Retrieves a passport by its identifier.
    /// </summary>
    /// <param name="studentId">The identifier of the student to fetch passport.</param>
    /// <returns>The passport as a data transfer object if found, otherwise null.</returns>
    Task<PassportDTO?> GetPassportByStudentIdAsync(string studentId);

    /// <summary>
    ///     Retrieves all passports in the system.
    /// </summary>
    /// <returns>A list of all passports as data transfer objects.</returns>
    Task<List<PassportDTO>> GetPassportsAsync();

    /// <summary>
    ///     Removes a passport from the system by its identifier.
    /// </summary>
    /// <param name="id">The identifier of the passport to remove.</param>
    /// <returns>True if the passport was removed, otherwise false.</returns>
    Task<bool> RemovePassportAsync(string id);

    /// <summary>
    ///     Updates an existing passport in the system.
    /// </summary>
    /// <param name="id">The identifier of the passport to update.</param>
    /// <param name="passportDto">The data transfer object containing the updated details of the passport.</param>
    /// <returns>The updated passport as a data transfer object if the update was successful, otherwise null.</returns>
    Task<PassportDTO?> UpdatePassportAsync(string id, PassportDTO passportDto);
}