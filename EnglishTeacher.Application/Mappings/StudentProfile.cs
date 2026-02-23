using AutoMapper;
using EnglishTeacher.Application.DTOs.Students;
using EnglishTeacher.Domain.Entities;

namespace EnglishTeacher.Application.Mappings;

public class StudentProfile : Profile
{
    public StudentProfile()
    {
        // CREATE
        CreateMap<StudentCreateDto, Student>();

        // UPDATE
        CreateMap<StudentUpdateDto, Student>();

        // READ
        CreateMap<Student, StudentResponseDto>();
    }
}