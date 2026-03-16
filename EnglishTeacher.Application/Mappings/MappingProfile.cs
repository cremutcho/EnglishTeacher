using AutoMapper;
using EnglishTeacher.Domain.Entities;
using EnglishTeacher.Application.DTOs.Exercises;

namespace EnglishTeacher.Application.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Exercise, ExerciseResponseDto>();
    }
}