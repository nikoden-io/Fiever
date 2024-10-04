// -----------------------------------------------------------------------
// <copyright>
//     Copyright 2024, Nikoden.IO All rights reserved.
//     Author: Nicolas Denoël
// </copyright>
// -----------------------------------------------------------------------

using Fiever.Application.DTO.ApplicationDTO;

namespace Fiever.Application.Interfaces.ApplicationServices;

/// <summary>
///     Provides methods for managing and processing countries.
/// </summary>
public interface ICountryAppService
{
    /// <summary>
    ///     Create a country in database
    /// </summary>
    /// <param name="countryDto">The DTO that represent all data needed to create Country entity.</param>
    /// <returns>A task that returns an ApiResponse indicating the success or failure of adding process.</returns>
    Task<CountryDTO> AddCountryAsync(CreateCountryDTO countryDto);

    /// <summary>
    ///     Fetch a country in database
    /// </summary>
    /// <param name="id">ID of country to fetch data.</param>
    /// <returns>A task that returns an ApiResponse indicating the success or failure of creation process.</returns>
    Task<CountryDTO?> GetCountryByIdAsync(string id);

    /// <summary>
    ///     Fetch all countries from database
    /// </summary>
    /// <returns>A task that returns an ApiResponse indicating the success or failure of fetching.</returns>
    Task<List<CountryWithStudentsDTO>> GetCountriesAsync();

    /// <summary>
    ///     Remove a country from database
    /// </summary>
    /// <param name="id">ID of country to remove.</param>
    /// <returns>A task that returns an ApiResponse indicating the success or failure of removing process.</returns>
    Task<bool> RemoveCountryAsync(string id);

    /// <summary>
    ///     Update a country in database
    /// </summary>
    /// <param name="id">ID of country to update.</param>
    /// <param name="countryDto">The DTO that represent all data needed to update Country entity.</param>
    /// <returns>A task that returns an ApiResponse indicating the success or failure of update process.</returns>
    Task<CountryDTO?> UpdateCountryAsync(string id, CountryDTO countryDto);
}