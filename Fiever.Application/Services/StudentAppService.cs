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

public class StudentAppService : IStudentAppService
{
    private readonly ICountryRepository _countryRepository;
    private readonly ILogger<StudentAppService> _logger;
    private readonly IMapper _mapper;
    private readonly IStudentRepository _studentRepository;

    public StudentAppService(IStudentRepository studentRepository, ICountryRepository countryRepository, IMapper mapper,
        ILogger<StudentAppService> logger)
    {
        _studentRepository = studentRepository;
        _countryRepository = countryRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<StudentDTO> AddStudentAsync(CreateStudentDTO studentDto)
    {
        Country? country = null;
        if (!string.IsNullOrEmpty(studentDto.CountryId))
        {
            country = await _countryRepository.GetCountryByIdAsync(studentDto.CountryId);
            if (country == null) throw new Exception("Country not found");
        }

        var student = new Student
        {
            Id = ObjectId.GenerateNewId().ToString(),
            FirstName = studentDto.FirstName,
            LastName = studentDto.LastName,
            Email = studentDto.Email,
            CountryId = studentDto.CountryId,
            Country = country
        };

        await _studentRepository.AddStudentAsync(student);
        return _mapper.Map<StudentDTO>(student);
    }

    public async Task<StudentDTO?> GetStudentByIdAsync(string id)
    {
        var student = await _studentRepository.GetStudentByIdAsync(id);
        return student != null ? _mapper.Map<StudentDTO>(student) : null;
    }

    public async Task<List<StudentDTO>> GetStudentsAsync()
    {
        var students = await _studentRepository.GetStudentsAsync();
        return _mapper.Map<List<StudentDTO>>(students);
    }

    public async Task<bool> RemoveStudentAsync(string id)
    {
        return await _studentRepository.RemoveStudentAsync(id);
    }

    public async Task<StudentDTO?> UpdateStudentAsync(string id, StudentDTO studentDto)
    {
        var existingStudent = await _studentRepository.GetStudentByIdAsync(id);
        if (existingStudent == null)
            return null;

        existingStudent.FirstName = studentDto.FirstName;
        existingStudent.LastName = studentDto.LastName;
        existingStudent.Email = studentDto.Email;

        if (!string.IsNullOrEmpty(studentDto.CountryId))
        {
            var country = await _countryRepository.GetCountryByIdAsync(studentDto.CountryId);
            if (country == null) throw new Exception("Country not found");

            existingStudent.CountryId = studentDto.CountryId;
            existingStudent.Country = country;
        }
        else
        {
            existingStudent.CountryId = null;
            existingStudent.Country = null;
        }

        await _studentRepository.UpdateStudentAsync(existingStudent);
        return _mapper.Map<StudentDTO>(existingStudent);
    }
}