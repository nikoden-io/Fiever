// -----------------------------------------------------------------------
// <copyright>
//     Copyright 2024, Nikoden.IO All rights reserved.
//     Author: Nicolas Denoël
// </copyright>
// -----------------------------------------------------------------------

using Fiever.Application.DTO.ApplicationDTO;
using Fiever.Application.Interfaces.ApplicationServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Fiever.UI.Models;

public class EnrollStudentModel : PageModel
{
    private readonly ICourseAppService _courseAppService;
    private readonly IStudentAppService _studentAppService;
    private readonly IStudentCourseAppService _studentCourseAppService;
    private readonly IUniversityAppService _universityAppService;

    public EnrollStudentModel(
        IStudentAppService studentAppService,
        ICourseAppService courseAppService,
        IUniversityAppService universityAppService,
        IStudentCourseAppService studentCourseAppService)
    {
        _studentAppService = studentAppService;
        _courseAppService = courseAppService;
        _universityAppService = universityAppService;
        _studentCourseAppService = studentCourseAppService;
    }

    [BindProperty] public string StudentId { get; set; } = string.Empty;

    [BindProperty] public string UniversityId { get; set; } = string.Empty;

    [BindProperty] public List<string> CourseIds { get; set; } = new();

    public List<StudentDTO> Students { get; set; } = new();
    public List<CourseDTO> Courses { get; set; } = new();
    public List<UniversityDTO> Universities { get; set; } = new();

    public async Task OnGetAsync()
    {
        Students = await _studentAppService.GetStudentsAsync();
        Courses = await _courseAppService.GetCoursesAsync();
        Universities = await _universityAppService.GetUniversitiesAsync();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            await OnGetAsync(); // Reload the dropdown values
            return Page();
        }

        foreach (var courseId in CourseIds) await _studentCourseAppService.AddStudentToCourseAsync(StudentId, courseId);

        return RedirectToPage("/Relationships/Success");
    }
}