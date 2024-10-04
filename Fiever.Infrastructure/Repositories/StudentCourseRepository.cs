// -----------------------------------------------------------------------
// <copyright>
//     Copyright 2024, Nikoden.IO All rights reserved.
//     Author: Nicolas Denoël
// </copyright>
// -----------------------------------------------------------------------

using Fiever.Domain.IRepositories;
using Microsoft.Data.SqlClient;

namespace Fiever.Infrastructure.Repositories;

/// <summary>
///     Represents the repository for managing the many-to-many relationship between students and courses.
/// </summary>
public class StudentCourseRepository : IStudentCourseRepository
{
    private readonly string _connectionString;

    public StudentCourseRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    public async Task AddStudentToCourseAsync(string studentId, string courseId)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();

            using (var command = new SqlCommand(
                       "INSERT INTO StudentCourses (StudentId, CourseId) VALUES (@StudentId, @CourseId)",
                       connection))
            {
                command.Parameters.AddWithValue("@StudentId", studentId);
                command.Parameters.AddWithValue("@CourseId", courseId);

                await command.ExecuteNonQueryAsync();
            }
        }
    }

    public async Task<bool> IsStudentEnrolledInCourseAsync(string studentId, string courseId)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();

            using (var command = new SqlCommand(
                       "SELECT COUNT(1) FROM StudentCourses WHERE StudentId = @StudentId AND CourseId = @CourseId",
                       connection))
            {
                command.Parameters.AddWithValue("@StudentId", studentId);
                command.Parameters.AddWithValue("@CourseId", courseId);

                var result = (int)await command.ExecuteScalarAsync();
                return result > 0;
            }
        }
    }

    public async Task RemoveStudentFromCourseAsync(string studentId, string courseId)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();

            using (var command = new SqlCommand(
                       "DELETE FROM StudentCourses WHERE StudentId = @StudentId AND CourseId = @CourseId",
                       connection))
            {
                command.Parameters.AddWithValue("@StudentId", studentId);
                command.Parameters.AddWithValue("@CourseId", courseId);

                await command.ExecuteNonQueryAsync();
            }
        }
    }

    public async Task<List<string>> GetCoursesByStudentIdAsync(string studentId)
    {
        var courseIds = new List<string>();

        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();

            using (var command = new SqlCommand(
                       "SELECT CourseId FROM StudentCourses WHERE StudentId = @StudentId",
                       connection))
            {
                command.Parameters.AddWithValue("@StudentId", studentId);
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync()) courseIds.Add(reader.GetString(0));
                }
            }
        }

        return courseIds;
    }

    public async Task<List<string>> GetStudentsByCourseIdAsync(string courseId)
    {
        var studentIds = new List<string>();

        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();

            using (var command = new SqlCommand(
                       "SELECT StudentId FROM StudentCourses WHERE CourseId = @CourseId",
                       connection))
            {
                command.Parameters.AddWithValue("@CourseId", courseId);
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync()) studentIds.Add(reader.GetString(0));
                }
            }
        }

        return studentIds;
    }
}