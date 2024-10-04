// -----------------------------------------------------------------------
// <copyright>
//     Copyright 2024, Nikoden.IO All rights reserved.
//     Author: Nicolas Denoël
// </copyright>
// -----------------------------------------------------------------------

using Fiever.Domain.Entities;

namespace Fiever.Domain.IRepositories;

public interface IUniversityRepository
{
    Task AddUniversityAsync(University university);
    Task<University?> GetUniversityByIdAsync(string universityId);
    Task<List<University>> GetUniversitiesAsync();
    Task<bool> RemoveUniversityAsync(string id);
    Task UpdateUniversityAsync(University university);
}