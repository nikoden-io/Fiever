using Fiever.Application.Interfaces.ApplicationServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Fiever.UI.Models;

public class DeleteStudentModel : PageModel
{
    private readonly IStudentAppService _studentAppService;

    public DeleteStudentModel(IStudentAppService studentAppService)
    {
        _studentAppService = studentAppService;
    }

    public string StudentId { get; set; }

    public async Task<IActionResult> OnGetAsync(string id)
    {
        StudentId = id;
        var student = await _studentAppService.GetStudentByIdAsync(id);
        if (student == null) return NotFound();

        return Page();
    }

    public async Task<IActionResult> OnPostAsync(string id)
    {
        var result = await _studentAppService.RemoveStudentAsync(id);
        if (!result) return NotFound();

        return RedirectToPage("/Students/Index");
    }
}