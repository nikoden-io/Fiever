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

namespace Fiever.Application.Services;

public class PassportAppService : IPassportAppService
{
    private readonly ILogger<PassportAppService> _logger;
    private readonly IMapper _mapper;
    private readonly IPassportRepository _passportRepository;

    public PassportAppService(IPassportRepository passportRepository, IMapper mapper,
        ILogger<PassportAppService> logger)
    {
        _passportRepository = passportRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<PassportDTO> AddPassportAsync(CreatePassportDTO passportDto)
    {
        var passport = new Passport
        {
            Id = ObjectId.GenerateNewId().ToString(),
            Number = passportDto.Number,
            StudentId = passportDto.StudentId
        };

        await _passportRepository.AddPassportAsync(passport);
        return _mapper.Map<PassportDTO>(passport);
    }

    public async Task<PassportDTO?> GetPassportByIdAsync(string id)
    {
        var passport = await _passportRepository.GetPassportByIdAsync(id);
        return passport != null ? _mapper.Map<PassportDTO>(passport) : null;
    }

    public async Task<PassportDTO?> GetPassportByStudentIdAsync(string studentId)
    {
        var passport = await _passportRepository.GetPassportByStudentIdAsync(studentId);
        return passport != null ? _mapper.Map<PassportDTO>(passport) : null;
    }

    public async Task<List<PassportDTO>> GetPassportsAsync()
    {
        var passports = await _passportRepository.GetPassportsAsync();
        return _mapper.Map<List<PassportDTO>>(passports);
    }

    public async Task<bool> RemovePassportAsync(string id)
    {
        return await _passportRepository.RemovePassportAsync(id);
    }

    public async Task<PassportDTO?> UpdatePassportAsync(string id, PassportDTO passportDto)
    {
        var existingPassport = await _passportRepository.GetPassportByIdAsync(id);
        if (existingPassport == null)
            return null;

        existingPassport.Number = passportDto.Number;
        existingPassport.StudentId = passportDto.StudentId;

        await _passportRepository.UpdatePassportAsync(existingPassport);
        return _mapper.Map<PassportDTO>(existingPassport);
    }
}