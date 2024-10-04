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
///     Manages HTTP requests for university related operations.
/// </summary>
[ApiController]
[ApiVersion("1.0")]
[Route("v{version:apiVersion}/university")]
public class UniversityController : ControllerBase
{
    private readonly IUniversityAppService _universityAppService;

    public UniversityController(IUniversityAppService universityAppService)
    {
        _universityAppService = universityAppService;
    }

    [HttpPost]
    public async Task<IActionResult> AddUniversity([FromBody] CreateUniversityDTO university)
    {
        if (!ModelState.IsValid)
            return BadRequest(ApiResponse<CreateUniversityDTO>.ErrorResponse(Messages.WrongFormat));

        var result = await _universityAppService.AddUniversityAsync(university);
        return Ok(ApiResponse<UniversityDTO>.SuccessResponse(result, Messages.CreationSuccess));
    }

    [HttpGet]
    public async Task<IActionResult> GetUniversities()
    {
        var universities = await _universityAppService.GetUniversitiesAsync();
        return Ok(ApiResponse<List<UniversityDTO>>.SuccessResponse(universities, Messages.FetchSuccess));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetUniversityById(string id)
    {
        var university = await _universityAppService.GetUniversityByIdAsync(id);
        if (university == null)
            return NotFound(ApiResponse<UniversityDTO>.ErrorResponse(Messages.NotFound));
        return Ok(ApiResponse<UniversityDTO>.SuccessResponse(university, Messages.FetchSuccess));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> RemoveUniversity(string id)
    {
        var result = await _universityAppService.RemoveUniversityAsync(id);
        if (!result)
            return NotFound(ApiResponse<string>.ErrorResponse(Messages.NotFound));

        return Ok(ApiResponse<string>.SuccessResponse(id, Messages.DeleteSuccess));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUniversity(string id, [FromBody] UniversityDTO universityDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ApiResponse<UniversityDTO>.ErrorResponse(Messages.WrongFormat));

        var updatedUniversity = await _universityAppService.UpdateUniversityAsync(id, universityDto);
        if (updatedUniversity == null)
            return NotFound(ApiResponse<UniversityDTO>.ErrorResponse(Messages.NotFound));

        return Ok(ApiResponse<UniversityDTO>.SuccessResponse(updatedUniversity, Messages.UpdateSuccess));
    }
}