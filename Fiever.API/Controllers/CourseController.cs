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
using GoldenBack.API.Constants;
using Microsoft.AspNetCore.Mvc;

namespace Fiever.API.Controllers;

/// <summary>
///     Manages HTTP requests for course related operations.
/// </summary>
[ApiController]
[ApiVersion("1.0")]
[Route("v{version:apiVersion}/course")]
public class CourseController : ControllerBase
{
    private readonly ICourseAppService _courseAppService;

    public CourseController(ICourseAppService courseAppService)
    {
        _courseAppService = courseAppService;
    }

    [HttpPost]
    public async Task<IActionResult> AddCourse([FromBody] CreateCourseDTO course)
    {
        if (!ModelState.IsValid)
            return BadRequest(ApiResponse<CreateCourseDTO>.ErrorResponse(Messages.WrongFormat));

        var result = await _courseAppService.AddCourseAsync(course);
        return Ok(ApiResponse<CourseDTO>.SuccessResponse(result, Messages.CreationSuccess));
    }

    [HttpGet]
    public async Task<IActionResult> GetCourses()
    {
        var courses = await _courseAppService.GetCoursesAsync();
        return Ok(ApiResponse<List<CourseDTO>>.SuccessResponse(courses, Messages.FetchSuccess));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCourseById(string id)
    {
        var course = await _courseAppService.GetCourseByIdAsync(id);
        if (course == null)
            return NotFound(ApiResponse<CourseDTO>.ErrorResponse(Messages.NotFound));
        return Ok(ApiResponse<CourseDTO>.SuccessResponse(course, Messages.FetchSuccess));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> RemoveCourse(string id)
    {
        var result = await _courseAppService.RemoveCourseAsync(id);
        if (!result)
            return NotFound(ApiResponse<string>.ErrorResponse(Messages.NotFound));

        return Ok(ApiResponse<string>.SuccessResponse(id, Messages.DeleteSuccess));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCourse(string id, [FromBody] CourseDTO courseDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ApiResponse<CourseDTO>.ErrorResponse(Messages.WrongFormat));

        var updatedCourse = await _courseAppService.UpdateCourseAsync(id, courseDto);
        if (updatedCourse == null)
            return NotFound(ApiResponse<CourseDTO>.ErrorResponse(Messages.NotFound));

        return Ok(ApiResponse<CourseDTO>.SuccessResponse(updatedCourse, Messages.UpdateSuccess));
    }
}