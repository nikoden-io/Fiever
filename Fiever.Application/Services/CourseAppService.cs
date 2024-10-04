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

public class CourseAppService : ICourseAppService
{
    private readonly ICourseRepository _courseRepository;
    private readonly ILogger<CourseAppService> _logger;
    private readonly IMapper _mapper;

    public CourseAppService(ICourseRepository courseRepository, IMapper mapper, ILogger<CourseAppService> logger)
    {
        _courseRepository = courseRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<CourseDTO> AddCourseAsync(CreateCourseDTO courseDto)
    {
        var course = new Course
        {
            Id = ObjectId.GenerateNewId().ToString(),
            Name = courseDto.Name,
            Code = courseDto.Code
        };

        await _courseRepository.AddCourseAsync(course);
        return _mapper.Map<CourseDTO>(course);
    }

    public async Task<CourseDTO?> GetCourseByIdAsync(string id)
    {
        var course = await _courseRepository.GetCourseByIdAsync(id);
        return course != null ? _mapper.Map<CourseDTO>(course) : null;
    }

    public async Task<List<CourseDTO>> GetCoursesAsync()
    {
        var courses = await _courseRepository.GetCoursesAsync();
        return _mapper.Map<List<CourseDTO>>(courses);
    }

    public async Task<bool> RemoveCourseAsync(string id)
    {
        return await _courseRepository.RemoveCourseAsync(id);
    }

    public async Task<CourseDTO?> UpdateCourseAsync(string id, CourseDTO courseDto)
    {
        var existingCourse = await _courseRepository.GetCourseByIdAsync(id);
        if (existingCourse == null)
            return null;

        existingCourse.Name = courseDto.Name;
        existingCourse.Code = courseDto.Code;

        await _courseRepository.UpdateCourseAsync(existingCourse);
        return _mapper.Map<CourseDTO>(existingCourse);
    }
}