// -----------------------------------------------------------------------
// <copyright>
//     Copyright 2024, Nikoden.IO All rights reserved.
//     Author: Nicolas Denoël
// </copyright>
// -----------------------------------------------------------------------

using System.Text;
using System.Text.Json;
using Fiever.Application.DTO.ApplicationDTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Fiever.UI.Models;

public class CreateStudentModel : PageModel
{
    private readonly HttpClient _httpClient;

    public CreateStudentModel(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient("FieverApiClient");
    }

    [BindProperty] public CreateStudentDTO Student { get; set; } = new();

    public void OnGet()
    {
        // Initialization, if needed
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            Console.WriteLine("Model Invalid in CreateStudentModel - OnPostAsync");
            return Page();
        }

        // Log the bound values of Student
        Console.WriteLine($"FirstName: {Student.FirstName}");
        Console.WriteLine($"LastName: {Student.LastName}");
        Console.WriteLine($"Email: {Student.Email}");

        var jsonContent = new StringContent(JsonSerializer.Serialize(Student), Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync("http:localhost:5166/v1.0/student/adonet", jsonContent);

        if (!response.IsSuccessStatusCode)
        {
            var responseBody = await response.Content.ReadAsStringAsync();
            Console.WriteLine("Error response: " + responseBody);
            ModelState.AddModelError(string.Empty, "An error occurred while creating the student: " + responseBody);
            return Page();
        }

        if (response.IsSuccessStatusCode)
            return RedirectToPage("/Students/Index");

        // Handle errors if needed
        ModelState.AddModelError(string.Empty, "An error occurred while creating the student.");
        return Page();
    }
}