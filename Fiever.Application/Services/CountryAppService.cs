// -----------------------------------------------------------------------
// <copyright>
//     Copyright 2024, Nikoden.IO All rights reserved.
//     Author: Nicolas Denoël
// </copyright>
// -----------------------------------------------------------------------

using AutoMapper;
using Fiever.Application.DTO.ApplicationDTO;
using Fiever.Application.Interfaces.ApplicationServices;
using Fiever.Domain.Entities;
using Fiever.Domain.IRepositories;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;

namespace Application.Services.AppServices;

public class CountryAppService : ICountryAppService
{
    private readonly ICountryRepository _countryRepository;
    private readonly ILogger<CountryAppService> _logger;
    private readonly IMapper _mapper;

    public CountryAppService(ICountryRepository countryRepository, IMapper mapper,
        ILogger<CountryAppService> logger)
    {
        _countryRepository = countryRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<CountryDTO> AddCountryAsync(CreateCountryDTO countryDto)
    {
        var country = new Country
        {
            Id = ObjectId.GenerateNewId().ToString(),
            Name = countryDto.Name,
            Code = countryDto.Code
        };

        await _countryRepository.AddCountryAsync(country);
        return _mapper.Map<CountryDTO>(country);
    }

    public async Task<CountryDTO?> GetCountryByIdAsync(string id)
    {
        var country = await _countryRepository.GetCountryByIdAsync(id);
        return country != null ? _mapper.Map<CountryDTO>(country) : null;
    }

    public async Task<List<CountryWithStudentsDTO>> GetCountriesAsync()
    {
        var countries = await _countryRepository.GetCountriesAsync();
        return _mapper.Map<List<CountryWithStudentsDTO>>(countries);
    }

    public async Task<bool> RemoveCountryAsync(string id)
    {
        return await _countryRepository.RemoveCountryAsync(id);
    }

    public async Task<CountryDTO?> UpdateCountryAsync(string id, CountryDTO countryDto)
    {
        var existingCountry = await _countryRepository.GetCountryByIdAsync(id);
        if (existingCountry == null)
            return null;

        existingCountry.Name = countryDto.Name;
        existingCountry.Code = countryDto.Code;

        await _countryRepository.UpdateCountryAsync(existingCountry);
        return _mapper.Map<CountryDTO>(existingCountry);
    }
}