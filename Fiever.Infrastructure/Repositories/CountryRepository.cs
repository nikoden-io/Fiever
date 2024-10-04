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

public class CountryRepository : ICountryRepository
{
    private readonly string _connectionString;
    private readonly ApplicationDbContext _context;


    public CountryRepository(string connectionString, ApplicationDbContext context)
    {
        _connectionString = connectionString;
        _context = context;
    }


    // ADO way
    public async Task AddCountryAsync(Country country)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();

            using (var command = new SqlCommand(
                       "INSERT INTO Countries (Id, Name, Code) VALUES (@Id, @Name, @Code)",
                       connection))
            {
                command.Parameters.AddWithValue("@Id", country.Id);
                command.Parameters.AddWithValue("@Name", country.Name);
                command.Parameters.AddWithValue("@Code", country.Code);

                await command.ExecuteNonQueryAsync();
            }
        }
    }

    // EF Core way
    public async Task<Country?> GetCountryByIdAsync(string countryId)
    {
        var country = await _context.Countries
            .Include(c => c.Students)
            .FirstOrDefaultAsync(c => c.Id == countryId);

        return country;
    }

    public async Task<List<Country>> GetCountriesAsync()
    {
        var countries = new List<Country>();

        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();

            // Step 1: Fetch all countries
            using (var command = new SqlCommand("SELECT Id, Name, Code FROM Countries", connection))
            {
                var reader = await command.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    var country = new Country
                    {
                        Id = reader.GetString(0),
                        Name = reader.GetString(1),
                        Code = reader.GetString(2),
                        Students = new List<Student>()
                    };
                    countries.Add(country);
                }

                await reader.CloseAsync();
            }

            // Step 2: Fetch students for each country
            foreach (var country in countries)
                using (var studentCommand =
                       new SqlCommand(
                           "SELECT Id, FirstName, LastName, Email, CountryId FROM Students WHERE CountryId = @CountryId",
                           connection))
                {
                    studentCommand.Parameters.AddWithValue("@CountryId", country.Id);

                    using (var studentReader = await studentCommand.ExecuteReaderAsync())
                    {
                        while (await studentReader.ReadAsync())
                        {
                            var student = new Student
                            {
                                Id = studentReader.GetString(0),
                                FirstName = studentReader.GetString(1),
                                LastName = studentReader.GetString(2),
                                Email = studentReader.IsDBNull(3) ? null : studentReader.GetString(3),
                                CountryId = studentReader.GetString(4)
                            };
                            country.Students.Add(student);
                        }
                    }
                }
        }

        return countries;
    }

    // ADO way
    public async Task<bool> RemoveCountryAsync(string id)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();

            using (var command = new SqlCommand("DELETE FROM Countries WHERE Id = @Id", connection))
            {
                command.Parameters.AddWithValue("@Id", id);

                var affectedRows = await command.ExecuteNonQueryAsync();
                return affectedRows > 0;
            }
        }
    }

    // ADO way
    public async Task UpdateCountryAsync(Country country)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();

            using (var command = new SqlCommand(
                       "UPDATE Countries SET Name = @Name, Code = @Code WHERE Id = @Id",
                       connection))
            {
                command.Parameters.AddWithValue("@Id", country.Id);
                command.Parameters.AddWithValue("@Name", country.Name);
                command.Parameters.AddWithValue("@Code", country.Code);

                await command.ExecuteNonQueryAsync();
            }
        }
    }
}