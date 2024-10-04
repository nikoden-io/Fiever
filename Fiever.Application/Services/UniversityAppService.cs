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

public class UniversityAppService : IUniversityAppService
{
    private readonly ILogger<UniversityAppService> _logger;
    private readonly IMapper _mapper;
    private readonly IUniversityRepository _universityRepository;

    public UniversityAppService(IUniversityRepository universityRepository, IMapper mapper,
        ILogger<UniversityAppService> logger)
    {
        _universityRepository = universityRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<UniversityDTO> AddUniversityAsync(CreateUniversityDTO universityDto)
    {
        var university = new University
        {
            Id = ObjectId.GenerateNewId().ToString(),
            Name = universityDto.Name,
            Address = universityDto.Address
        };

        await _universityRepository.AddUniversityAsync(university);
        return _mapper.Map<UniversityDTO>(university);
    }

    public async Task<UniversityDTO?> GetUniversityByIdAsync(string id)
    {
        var university = await _universityRepository.GetUniversityByIdAsync(id);
        return university != null ? _mapper.Map<UniversityDTO>(university) : null;
    }

    public async Task<List<UniversityDTO>> GetUniversitiesAsync()
    {
        var universities = await _universityRepository.GetUniversitiesAsync();
        return _mapper.Map<List<UniversityDTO>>(universities);
    }

    public async Task<bool> RemoveUniversityAsync(string id)
    {
        return await _universityRepository.RemoveUniversityAsync(id);
    }

    public async Task<UniversityDTO?> UpdateUniversityAsync(string id, UniversityDTO universityDto)
    {
        var existingUniversity = await _universityRepository.GetUniversityByIdAsync(id);
        if (existingUniversity == null)
            return null;

        existingUniversity.Name = universityDto.Name;
        existingUniversity.Address = universityDto.Address;

        await _universityRepository.UpdateUniversityAsync(existingUniversity);
        return _mapper.Map<UniversityDTO>(existingUniversity);
    }
}