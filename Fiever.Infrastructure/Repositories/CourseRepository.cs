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
///     Represents the repository for managing course-related operations.
/// </summary>
public class CourseRepository : ICourseRepository
{
    private readonly string _connectionString;
    private readonly ApplicationDbContext _context;

    public CourseRepository(string connectionString, ApplicationDbContext context)
    {
        _connectionString = connectionString;
        _context = context;
    }

    public async Task AddCourseAsync(Course course)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();

            using (var command = new SqlCommand(
                       "INSERT INTO Courses (Id, Name, Code) VALUES (@Id, @Name, @Code)",
                       connection))
            {
                command.Parameters.AddWithValue("@Id", course.Id);
                command.Parameters.AddWithValue("@Name", course.Name);
                command.Parameters.AddWithValue("@Code", course.Code);

                await command.ExecuteNonQueryAsync();
            }
        }
    }

    public async Task<Course?> GetCourseByIdAsync(string courseId)
    {
        var course = await _context.Courses
            .Include(c => c.StudentCourses).ThenInclude(sc => sc.Student)
            .FirstOrDefaultAsync(c => c.Id == courseId);

        return course;
    }

    public async Task<List<Course>> GetCoursesAsync()
    {
        var courses = new List<Course>();

        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();

            using (var command = new SqlCommand("SELECT Id, Name, Code FROM Courses", connection))
            {
                var reader = await command.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                    courses.Add(new Course
                    {
                        Id = reader.GetString(0),
                        Name = reader.GetString(1),
                        Code = reader.GetString(2)
                    });
            }
        }

        return courses;
    }

    public async Task<bool> RemoveCourseAsync(string id)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();

            using (var command = new SqlCommand("DELETE FROM Courses WHERE Id = @Id", connection))
            {
                command.Parameters.AddWithValue("@Id", id);

                var affectedRows = await command.ExecuteNonQueryAsync();
                return affectedRows > 0;
            }
        }
    }

    public async Task UpdateCourseAsync(Course course)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();

            using (var command = new SqlCommand(
                       "UPDATE Courses SET Name = @Name, Code = @Code WHERE Id = @Id",
                       connection))
            {
                command.Parameters.AddWithValue("@Id", course.Id);
                command.Parameters.AddWithValue("@Name", course.Name);
                command.Parameters.AddWithValue("@Code", course.Code);

                await command.ExecuteNonQueryAsync();
            }
        }
    }

    public async Task<List<Course>> GetCoursesByStudentIdAsync(string studentId)
    {
        var courses = new List<Course>();

        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();

            using (var command = new SqlCommand(
                       @"SELECT c.Id, c.Name, c.Code
                  FROM Courses c
                  INNER JOIN StudentCourses sc ON c.Id = sc.CourseId
                  WHERE sc.StudentId = @StudentId",
                       connection))
            {
                command.Parameters.AddWithValue("@StudentId", studentId);
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                        courses.Add(new Course
                        {
                            Id = reader.GetString(0),
                            Name = reader.GetString(1),
                            Code = reader.GetString(2)
                        });
                }
            }
        }

        return courses;
    }
}