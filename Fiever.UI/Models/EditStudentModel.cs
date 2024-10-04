using System.Text;
using System.Text.Json;
using Fiever.Application.DTO.ApplicationDTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Fiever.UI.Models;

public class EditStudentModel : PageModel
{
    private readonly HttpClient _httpClient;

    public EditStudentModel(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient("FieverApiClient");
    }

    [BindProperty] public StudentDTO Student { get; set; } = new();

    public async Task<IActionResult> OnGetAsync(string id)
    {
        var response = await _httpClient.GetAsync($"http://localhost:5166/v1.0/student/{id}");

        if (!response.IsSuccessStatusCode) return NotFound();

        var jsonContent = await response.Content.ReadAsStringAsync();
        var apiResponse = JsonSerializer.Deserialize<ApiResponse<StudentDTO>>(jsonContent, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        if (apiResponse?.Success == true && apiResponse.Data != null)
            Student = apiResponse.Data;
        else
            return NotFound();

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        Console.WriteLine($"Updating student with ID: {Student.Id}");
        if (string.IsNullOrEmpty(Student.Id))
        {
            ModelState.AddModelError(string.Empty, "Student ID is missing.");
            return Page();
        }

        if (!ModelState.IsValid) return Page();

        var jsonContent = new StringContent(JsonSerializer.Serialize(Student), Encoding.UTF8, "application/json");
        var response = await _httpClient.PutAsync($"http://localhost:5166/v1.0/student/{Student.Id}", jsonContent);

        if (response.IsSuccessStatusCode) return RedirectToPage("/Students/Index");

        ModelState.AddModelError(string.Empty, "An error occurred while updating the student.");
        return Page();
    }
}