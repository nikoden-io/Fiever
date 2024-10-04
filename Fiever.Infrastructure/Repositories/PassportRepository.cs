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

namespace Fiever.Infrastructure.Repositories;

/// <summary>
///     Represents the repository for managing passport-related operations.
/// </summary>
public class PassportRepository : IPassportRepository
{
    private readonly string _connectionString;
    private readonly ApplicationDbContext _context;

    public PassportRepository(string connectionString, ApplicationDbContext context)
    {
        _connectionString = connectionString;
        _context = context;
    }

    public async Task AddPassportAsync(Passport passport)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();

            using (var command = new SqlCommand(
                       "INSERT INTO Passports (Id, Number, StudentId) VALUES (@Id, @Number, @StudentId)",
                       connection))
            {
                command.Parameters.AddWithValue("@Id", passport.Id);
                command.Parameters.AddWithValue("@Number", passport.Number);
                command.Parameters.AddWithValue("@StudentId", passport.StudentId);

                await command.ExecuteNonQueryAsync();
            }
        }
    }

    public async Task<Passport?> GetPassportByIdAsync(string passportId)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();

            using (var command =
                   new SqlCommand("SELECT Id, Number, StudentId FROM Passports WHERE Id = @Id", connection))
            {
                command.Parameters.AddWithValue("@Id", passportId);
                using (var reader = await command.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                        return new Passport
                        {
                            Id = reader.GetString(0),
                            Number = reader.GetString(1),
                            StudentId = reader.GetString(2)
                        };
                }
            }
        }

        return null;
    }

    public async Task<Passport?> GetPassportByStudentIdAsync(string studentId)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();

            using (var command =
                   new SqlCommand("SELECT Id, Number, StudentId FROM Passports WHERE StudentId = @StudentId",
                       connection))
            {
                command.Parameters.AddWithValue("@StudentId", studentId);
                using (var reader = await command.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                        return new Passport
                        {
                            Id = reader.GetString(0),
                            Number = reader.GetString(1),
                            StudentId = reader.GetString(2)
                        };
                }
            }
        }

        return null;
    }

    public async Task<List<Passport>> GetPassportsAsync()
    {
        var passports = new List<Passport>();

        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();

            using (var command = new SqlCommand("SELECT Id, Number, StudentId FROM Passports", connection))
            {
                var reader = await command.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                    passports.Add(new Passport
                    {
                        Id = reader.GetString(0),
                        Number = reader.GetString(1),
                        StudentId = reader.GetString(2)
                    });
            }
        }

        return passports;
    }

    public async Task<bool> RemovePassportAsync(string id)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();

            using (var command = new SqlCommand("DELETE FROM Passports WHERE Id = @Id", connection))
            {
                command.Parameters.AddWithValue("@Id", id);

                var affectedRows = await command.ExecuteNonQueryAsync();
                return affectedRows > 0;
            }
        }
    }

    public async Task UpdatePassportAsync(Passport passport)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();

            using (var command = new SqlCommand(
                       "UPDATE Passports SET Number = @Number, StudentId = @StudentId WHERE Id = @Id",
                       connection))
            {
                command.Parameters.AddWithValue("@Id", passport.Id);
                command.Parameters.AddWithValue("@Number", passport.Number);
                command.Parameters.AddWithValue("@StudentId", passport.StudentId);

                await command.ExecuteNonQueryAsync();
            }
        }
    }
}