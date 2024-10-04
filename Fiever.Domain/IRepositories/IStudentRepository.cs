// -----------------------------------------------------------------------
// <copyright>
//     Copyright 2024, Nikoden.IO All rights reserved.
//     Author: Nicolas Denoël
// </copyright>
// -----------------------------------------------------------------------

using Fiever.Domain.Entities;

namespace Fiever.Domain.IRepositories;

public interface IStudentRepository
{
    Task AddStudentAsync(Student student);
    Task<Student?> GetStudentByIdAsync(string id);
    Task<List<Student>> GetStudentsAsync();
    Task<bool> RemoveStudentAsync(string id);
    Task UpdateStudentAsync(Student student);
}