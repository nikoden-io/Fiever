using System.Text.Json;
using Fiever.Application.DTO.ApplicationDTO;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Fiever.UI.Models;

public class StudentsIndexModel : PageModel
{
    private readonly HttpClient _httpClient;

    public StudentsIndexModel(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public List<StudentDTO> Students { get; set; } = new();

    public async Task OnGetAsync()
    {
        var response = await _httpClient.GetAsync("http://localhost:5166/v1.0/student/adonet");

        if (response.IsSuccessStatusCode)
        {
            var jsonContent = await response.Content.ReadAsStringAsync();
            var apiResponse = JsonSerializer.Deserialize<ApiResponse<List<StudentDTO>>>(jsonContent,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

            if (apiResponse?.Success == true && apiResponse.Data != null) Students = apiResponse.Data;
        }
        else
        {
            Console.WriteLine($"Failed to get response from API. Status Code: {response.StatusCode}");
        }
    }
}