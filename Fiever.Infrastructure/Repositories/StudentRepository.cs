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
///     Represents the repository for managing student-related operations.
/// </summary>
public class StudentRepository : IStudentRepository
{
    private readonly string _connectionString;
    private readonly ApplicationDbContext _context;

    public StudentRepository(string connectionString, ApplicationDbContext context)
    {
        _connectionString = connectionString;
        _context = context;
    }

    public async Task AddStudentAsync(Student student)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();

            using (var command = new SqlCommand(
                       "INSERT INTO Students (Id, FirstName, LastName, Email, CountryId) VALUES (@Id, @FirstName, @LastName, @Email, @CountryId)",
                       connection))
            {
                command.Parameters.AddWithValue("@Id", student.Id);
                command.Parameters.AddWithValue("@FirstName", student.FirstName);
                command.Parameters.AddWithValue("@LastName", student.LastName);
                command.Parameters.AddWithValue("@Email", student.Email ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@CountryId", student.CountryId ?? (object)DBNull.Value);

                await command.ExecuteNonQueryAsync();
            }
        }
    }

    public async Task<Student?> GetStudentByIdAsync(string id)
    {
        var student = await _context.Students
            .Include(c => c.Country)
            .Include(s => s.Passport)
            .Include(s => s.University)
            .Include(s => s.StudentCourses).ThenInclude(sc => sc.Course)
            .FirstOrDefaultAsync(c => c.Id == id);

        return student;
    }

    public async Task<List<Student>> GetStudentsAsync()
    {
        var users = new List<Student>();

        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();

            using (var command =
                   new SqlCommand("SELECT Id, FirstName, LastName, Email, CountryId FROM Students", connection))
            {
                var reader = await command.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                    users.Add(new Student
                    {
                        Id = reader.GetString(0),
                        FirstName = reader.GetString(1),
                        LastName = reader.GetString(2),
                        Email = reader.GetString(3),
                        CountryId = reader.IsDBNull(4) ? null : reader.GetString(4)
                    });
            }
        }

        return users;
    }

    public async Task<bool> RemoveStudentAsync(string id)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();

            using (var command = new SqlCommand("DELETE FROM Students WHERE Id = @Id", connection))
            {
                command.Parameters.AddWithValue("@Id", id);

                var affectedRows = await command.ExecuteNonQueryAsync();
                return affectedRows > 0;
            }
        }
    }

    public async Task UpdateStudentAsync(Student student)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();

            using (var command = new SqlCommand(
                       "UPDATE Students SET FirstName = @FirstName, LastName = @LastName, Email = @Email, CountryId = @CountryId WHERE Id = @Id",
                       connection))
            {
                command.Parameters.AddWithValue("@Id", student.Id);
                command.Parameters.AddWithValue("@FirstName", student.FirstName);
                command.Parameters.AddWithValue("@LastName", student.LastName);
                command.Parameters.AddWithValue("@Email", student.Email ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@CountryId", student.CountryId ?? (object)DBNull.Value);

                await command.ExecuteNonQueryAsync();
            }
        }
    }
}