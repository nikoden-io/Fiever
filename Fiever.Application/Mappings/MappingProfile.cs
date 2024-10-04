// -----------------------------------------------------------------------
// <copyright>
//     Copyright 2024, Nikoden.IO All rights reserved.
//     Author: Nicolas Denoël
// </copyright>
// -----------------------------------------------------------------------

using AutoMapper;
using Fiever.Application.DTO.ApplicationDTO;
using Fiever.Domain.Entities;

namespace Fiever.Application.Mappings;

/// <summary>
///     Defines the AutoMapper profile for mapping between domain entities and DTOs.
/// </summary>
public class MappingProfile : Profile
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="MappingProfile" /> class.
    /// </summary>
    public MappingProfile()
    {
        CreateMap<Country, CountryDTO>();
        CreateMap<Country, CountryWithStudentsDTO>()
            .ForMember(dest => dest.Students, opt => opt.MapFrom(src => src.Students));
        CreateMap<Student, StudentDTO>();
    }
}