// -----------------------------------------------------------------------
// <copyright>
//     Copyright 2024, Nikoden.IO All rights reserved.
//     Author: Nicolas Denoël
// </copyright>
// -----------------------------------------------------------------------


using Application.DTO;
using Asp.Versioning;
using Fiever.Application.DTO.ApplicationDTO;
using Fiever.Application.Interfaces.ApplicationServices;
using Fiever.Domain.Entities;
using Fiever.Domain.IRepositories;
using GoldenBack.API.Constants;
using Microsoft.AspNetCore.Mvc;

namespace GoldenBack.API.Controllers;

/// <summary>
///     Manages HTTP requests for student related operations.
/// </summary>
[ApiController]
[ApiVersion("1.0")]
[Route("v{version:apiVersion}/student")]
public class StudentController : ControllerBase
{
    private readonly ICountryRepository _countryRepository;
    private readonly IStudentAppService _studentAppService;
    private readonly IStudentRepository _studentRepository;

    public StudentController(IStudentAppService studentAppService, IStudentRepository studentRepository,
        ICountryRepository countryRepository)
    {
        _studentAppService = studentAppService;
        _studentRepository = studentRepository;
        _countryRepository = countryRepository;
    }

    // Create a new student (EF Core)
    [HttpPost("efcore")]
    public async Task<IActionResult> AddStudentEFCore([FromBody] CreateStudentDTO studentDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ApiResponse<CreateStudentDTO>.ErrorResponse("Invalid input data"));

        var result = await _studentAppService.AddStudentAsync(studentDto);
        return Ok(ApiResponse<StudentDTO>.SuccessResponse(result, "Student created successfully"));
    }

    // Create a new student (ADO.NET)
    [HttpPost("adonet")]
    public async Task<IActionResult> AddStudentADO([FromBody] CreateStudentDTO studentDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ApiResponse<CreateStudentDTO>.ErrorResponse(Messages.WrongFormat));

        var student = new Student
        {
            Id = Guid.NewGuid().ToString(),
            FirstName = studentDto.FirstName,
            LastName = studentDto.LastName,
            Email = studentDto.Email,
            CountryId = studentDto.CountryId
        };

        await _studentRepository.AddStudentAsync(student);

        var country = await _countryRepository.GetCountryByIdAsync(student.CountryId);
        student.Country = country;

        return Ok(ApiResponse<Student>.SuccessResponse(student, Messages.CreationSuccess));
    }

    [HttpGet]
    public async Task<IActionResult> GetStudents()
    {
        var students = await _studentAppService.GetStudentsAsync();
        return Ok(ApiResponse<List<StudentDTO>>.SuccessResponse(students,
            Messages.FetchSuccess));
    }

    // Get all students (EF Core)
    [HttpGet("efcore")]
    public async Task<IActionResult> GetStudentsEFCore()
    {
        var result = await _studentAppService.GetStudentsAsync();
        return Ok(ApiResponse<List<StudentDTO>>.SuccessResponse(result, Messages.FetchSuccess));
    }

    // Get all students (ADO.NET)
    [HttpGet("adonet")]
    public async Task<IActionResult> GetStudentsADO()
    {
        var result = await _studentRepository.GetStudentsAsync();
        return Ok(ApiResponse<List<Student>>.SuccessResponse(result, Messages.FetchSuccess));
    }

    // Get student by ID
    [HttpGet("{id}")]
    public async Task<IActionResult> GetStudentById(string id)
    {
        var student = await _studentAppService.GetStudentByIdAsync(id);
        if (student == null)
            return NotFound(ApiResponse<StudentDTO>.ErrorResponse(Messages.NotFound));

        return Ok(ApiResponse<StudentDTO>.SuccessResponse(student, Messages.FetchSuccess));
    }

    // Update student by ID
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateStudent(string id, [FromBody] StudentDTO studentDto)
    {
        if (!ModelState.IsValid)
        {
            Console.WriteLine("BAD REQUEST REACHED ON UPDATE");
            return BadRequest(ApiResponse<StudentDTO>.ErrorResponse(Messages.WrongFormat));
        }

        var updatedStudent = await _studentAppService.UpdateStudentAsync(id, studentDto);
        if (updatedStudent == null)
            return NotFound(ApiResponse<StudentDTO>.ErrorResponse(Messages.NotFound));

        return Ok(ApiResponse<StudentDTO>.SuccessResponse(updatedStudent, Messages.UpdateSuccess));
    }

    // Delete student by ID
    [HttpDelete("{id}")]
    public async Task<IActionResult> RemoveStudent(string id)
    {
        var result = await _studentAppService.RemoveStudentAsync(id);
        if (!result)
            return NotFound(ApiResponse<string>.ErrorResponse(Messages.NotFound));

        return Ok(ApiResponse<string>.SuccessResponse(id, Messages.DeleteSuccess));
    }
}