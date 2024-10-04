using Application.DTO;
using Asp.Versioning;
using Fiever.Application.DTO.ApplicationDTO;
using Fiever.Application.Interfaces.ApplicationServices;
using Fiever.Domain.IRepositories;
using GoldenBack.API.Constants;
using Microsoft.AspNetCore.Mvc;

namespace Fiever.API.Controllers;

/// <summary>
///     Manages HTTP requests for country related operations.
/// </summary>
[ApiController]
[ApiVersion("1.0")]
[Route("v{version:apiVersion}/country")]
public class CountryController : ControllerBase
{
    private readonly ICountryAppService _countryAppService;

    public CountryController(ICountryAppService countryAppService, ICountryRepository countryRepository)
    {
        _countryAppService = countryAppService;
    }

    [HttpPost]
    public async Task<IActionResult> AddCountry([FromBody] CreateCountryDTO country)
    {
        if (!ModelState.IsValid)
            return BadRequest(ApiResponse<CreateStudentDTO>.ErrorResponse(Messages.WrongFormat));

        var result = await _countryAppService.AddCountryAsync(country);
        return Ok(ApiResponse<CountryDTO>.SuccessResponse(result, Messages.CreationSuccess));
    }

    [HttpGet]
    public async Task<IActionResult> GetCountries()
    {
        var countries = await _countryAppService.GetCountriesAsync();
        return Ok(ApiResponse<List<CountryWithStudentsDTO>>.SuccessResponse(countries,
            Messages.FetchSuccess));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCountryById(string id)
    {
        var country = await _countryAppService.GetCountryByIdAsync(id);
        if (country == null)
            return NotFound(ApiResponse<CountryDTO>.ErrorResponse(Messages.NotFound));
        return Ok(ApiResponse<CountryDTO>.SuccessResponse(country, Messages.FetchSuccess));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> RemoveCountry(string id)
    {
        var result = await _countryAppService.RemoveCountryAsync(id);
        if (!result)
            return NotFound(ApiResponse<string>.ErrorResponse(Messages.NotFound));

        return Ok(ApiResponse<string>.SuccessResponse(id, Messages.DeleteSuccess));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCountry(string id, [FromBody] CountryDTO countryDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ApiResponse<CountryDTO>.ErrorResponse(Messages.WrongFormat));

        var updatedCountry = await _countryAppService.UpdateCountryAsync(id, countryDto);
        if (updatedCountry == null)
            return NotFound(ApiResponse<CountryDTO>.ErrorResponse(Messages.NotFound));

        return Ok(ApiResponse<CountryDTO>.SuccessResponse(updatedCountry, Messages.UpdateSuccess));
    }
}