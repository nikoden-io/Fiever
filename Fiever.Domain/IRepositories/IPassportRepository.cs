// -----------------------------------------------------------------------
// <copyright>
//     Copyright 2024, Nikoden.IO All rights reserved.
//     Author: Nicolas Denoël
// </copyright>
// -----------------------------------------------------------------------

using Fiever.Domain.Entities;

namespace Fiever.Domain.IRepositories;

public interface IPassportRepository
{
    /// <summary>
    ///     Adds a new passport to the repository.
    /// </summary>
    /// <param name="passport">The passport entity to add.</param>
    Task AddPassportAsync(Passport passport);

    /// <summary>
    ///     Retrieves a passport by its identifier.
    /// </summary>
    /// <param name="passportId">The identifier of the passport.</param>
    /// <returns>The passport entity if found, otherwise null.</returns>
    Task<Passport?> GetPassportByIdAsync(string passportId);

    /// <summary>
    ///     Retrieves a passport by student identifier.
    /// </summary>
    /// <param name="studentId">The identifier of the student to fetch passport.</param>
    /// <returns>The passport entity if found, otherwise null.</returns>
    Task<Passport?> GetPassportByStudentIdAsync(string studentId);

    /// <summary>
    ///     Retrieves all passports from the repository.
    /// </summary>
    /// <returns>A list of all passports.</returns>
    Task<List<Passport>> GetPassportsAsync();

    /// <summary>
    ///     Removes a passport from the repository by its identifier.
    /// </summary>
    /// <param name="id">The identifier of the passport to remove.</param>
    /// <returns>True if the passport was removed, otherwise false.</returns>
    Task<bool> RemovePassportAsync(string id);

    /// <summary>
    ///     Updates an existing passport in the repository.
    /// </summary>
    /// <param name="passport">The passport entity with updated information.</param>
    Task UpdatePassportAsync(Passport passport);
}