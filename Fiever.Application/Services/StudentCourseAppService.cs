// -----------------------------------------------------------------------
// <copyright>
//     Copyright 2024, Nikoden.IO All rights reserved.
//     Author: Nicolas Denoël
// </copyright>
// -----------------------------------------------------------------------

using AutoMapper;
using Fiever.Application.Interfaces.ApplicationServices;
using Fiever.Domain.IRepositories;
using Microsoft.Extensions.Logging;

namespace Application.Services.AppServices;

public class StudentCourseAppService : IStudentCourseAppService
{
    private readonly ILogger<StudentCourseAppService> _logger;
    private readonly IMapper _mapper;
    private readonly IStudentCourseRepository _studentCourseRepository;

    public StudentCourseAppService(IStudentCourseRepository studentCourseRepository, IMapper mapper,
        ILogger<StudentCourseAppService> logger)
    {
        _studentCourseRepository = studentCourseRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task AddStudentToCourseAsync(string studentId, string courseId)
    {
        if (await _studentCourseRepository.IsStudentEnrolledInCourseAsync(studentId, courseId))
            throw new Exception("Student is already enrolled in the course");
        await _studentCourseRepository.AddStudentToCourseAsync(studentId, courseId);
    }

    public async Task RemoveStudentFromCourseAsync(string studentId, string courseId)
    {
        if (!await _studentCourseRepository.IsStudentEnrolledInCourseAsync(studentId, courseId))
            throw new Exception("Student is not enrolled in the course");
        await _studentCourseRepository.RemoveStudentFromCourseAsync(studentId, courseId);
    }

    public async Task<List<string>> GetCoursesByStudentIdAsync(string studentId)
    {
        return await _studentCourseRepository.GetCoursesByStudentIdAsync(studentId);
    }

    public async Task<List<string>> GetStudentsByCourseIdAsync(string courseId)
    {
        return await _studentCourseRepository.GetStudentsByCourseIdAsync(courseId);
    }
}