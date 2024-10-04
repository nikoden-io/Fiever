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
///     Manages HTTP requests for passport related operations.
/// </summary>
[ApiController]
[ApiVersion("1.0")]
[Route("v{version:apiVersion}/passport")]
public class PassportController : ControllerBase
{
    private readonly IPassportAppService _passportAppService;

    public PassportController(IPassportAppService passportAppService)
    {
        _passportAppService = passportAppService;
    }

    [HttpPost]
    public async Task<IActionResult> AddPassport([FromBody] CreatePassportDTO passport)
    {
        if (!ModelState.IsValid)
            return BadRequest(ApiResponse<CreatePassportDTO>.ErrorResponse(Messages.WrongFormat));

        var result = await _passportAppService.AddPassportAsync(passport);
        return Ok(ApiResponse<PassportDTO>.SuccessResponse(result, Messages.CreationSuccess));
    }

    [HttpGet("student/{studentId}")]
    public async Task<IActionResult> GetPassportByStudentId(string studentId)
    {
        var passport = await _passportAppService.GetPassportByStudentIdAsync(studentId);
        if (passport == null)
            return NotFound(ApiResponse<PassportDTO>.ErrorResponse(Messages.NotFound));
        return Ok(ApiResponse<PassportDTO>.SuccessResponse(passport, Messages.FetchSuccess));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> RemovePassport(string id)
    {
        var result = await _passportAppService.RemovePassportAsync(id);
        if (!result)
            return NotFound(ApiResponse<string>.ErrorResponse(Messages.NotFound));

        return Ok(ApiResponse<string>.SuccessResponse(id, Messages.DeleteSuccess));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdatePassport(string id, [FromBody] PassportDTO passportDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ApiResponse<PassportDTO>.ErrorResponse(Messages.WrongFormat));

        var updatedPassport = await _passportAppService.UpdatePassportAsync(id, passportDto);
        if (updatedPassport == null)
            return NotFound(ApiResponse<PassportDTO>.ErrorResponse(Messages.NotFound));

        return Ok(ApiResponse<PassportDTO>.SuccessResponse(updatedPassport, Messages.UpdateSuccess));
    }
}