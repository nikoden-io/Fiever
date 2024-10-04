// -----------------------------------------------------------------------
// <copyright>
//     Copyright 2024, Nikoden.IO All rights reserved.
//     Author: Nicolas Denoël
// </copyright>
// -----------------------------------------------------------------------

using Fiever.Domain.Entities;
using Fiever.Domain.IRepositories;
using Fiever.Infrastructure.DbContext;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Fiever.Infrastructure.Repositories;

/// <summary>
///     Represents the repository for managing university-related operations.
/// </summary>
public class UniversityRepository : IUniversityRepository
{
    private readonly string _connectionString;
    private readonly ApplicationDbContext _context;

    public UniversityRepository(string connectionString, ApplicationDbContext context)
    {
        _connectionString = connectionString;
        _context = context;
    }

    public async Task AddUniversityAsync(University university)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();

            using (var command = new SqlCommand(
                       "INSERT INTO Universities (Id, Name, Address) VALUES (@Id, @Name, @Address)",
                       connection))
            {
                command.Parameters.AddWithValue("@Id", university.Id);
                command.Parameters.AddWithValue("@Name", university.Name);
                command.Parameters.AddWithValue("@Address", university.Address);

                await command.ExecuteNonQueryAsync();
            }
        }
    }

    public async Task<University?> GetUniversityByIdAsync(string universityId)
    {
        var university = await _context.Universities
            .Include(u => u.Students)
            .FirstOrDefaultAsync(u => u.Id == universityId);

        return university;
    }

    public async Task<List<University>> GetUniversitiesAsync()
    {
        var universities = new List<University>();

        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();

            using (var command = new SqlCommand("SELECT Id, Name, Address FROM Universities", connection))
            {
                var reader = await command.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                    universities.Add(new University
                    {
                        Id = reader.GetString(0),
                        Name = reader.GetString(1),
                        Address = reader.GetString(2)
                    });
            }
        }

        return universities;
    }

    public async Task<bool> RemoveUniversityAsync(string id)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();

            using (var command = new SqlCommand("DELETE FROM Universities WHERE Id = @Id", connection))
            {
                command.Parameters.AddWithValue("@Id", id);

                var affectedRows = await command.ExecuteNonQueryAsync();
                return affectedRows > 0;
            }
        }
    }

    public async Task UpdateUniversityAsync(University university)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();

            using (var command = new SqlCommand(
                       "UPDATE Universities SET Name = @Name, Address = @Address WHERE Id = @Id",
                       connection))
            {
                command.Parameters.AddWithValue("@Id", university.Id);
                command.Parameters.AddWithValue("@Name", university.Name);
                command.Parameters.AddWithValue("@Address", university.Address);

                await command.ExecuteNonQueryAsync();
            }
        }
    }
}