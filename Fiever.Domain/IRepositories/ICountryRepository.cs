// -----------------------------------------------------------------------
// <copyright>
//     Copyright 2024, Nikoden.IO All rights reserved.
//     Author: Nicolas Denoël
// </copyright>
// -----------------------------------------------------------------------

using Fiever.Domain.Entities;

namespace Fiever.Domain.IRepositories;

public interface ICountryRepository
{
    Task AddCountryAsync(Country country);
    Task<Country?> GetCountryByIdAsync(string id);
    Task<List<Country>> GetCountriesAsync();
    Task<bool> RemoveCountryAsync(string id);
    Task UpdateCountryAsync(Country country);
}