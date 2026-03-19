using AutoMapper;
using EnglishTeacher.Domain.Entities;
using EnglishTeacher.Application.DTOs.Students;
using EnglishTeacher.Application.DTOs.Teachers;
using EnglishTeacher.Application.DTOs.Lessons;
using EnglishTeacher.Application.DTOs.Exercises;

namespace EnglishTeacher.Application.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // 🔹 Students
        CreateMap<Student, StudentResponseDto>();
        CreateMap<StudentCreateDto, Student>();
        CreateMap<StudentUpdateDto, Student>();

        // 🔹 Teachers
        CreateMap<Teacher, TeacherResponseDto>();
        CreateMap<TeacherCreateDto, Teacher>();
        CreateMap<TeacherUpdateDto, Teacher>();

        // 🔹 Lessons
        CreateMap<Lesson, LessonResponseDto>();
        CreateMap<CreateLessonDto, Lesson>();
        CreateMap<UpdateLessonDto, Lesson>();

        // 🔹 Exercises (🔥 CORRIGIDO)
        CreateMap<Exercise, ExerciseResponseDto>()
            .ForMember(dest => dest.Type,
                opt => opt.MapFrom(src => src.Type.ToString()))
            .ForMember(dest => dest.Difficulty,
                opt => opt.MapFrom(src => src.Difficulty.ToString()));

        CreateMap<CreateExerciseDto, Exercise>();
    }
}