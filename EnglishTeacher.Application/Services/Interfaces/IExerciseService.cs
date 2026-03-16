using EnglishTeacher.Application.DTOs.Exercises;

public interface IExerciseService
{
    Task<ExerciseResponseDto> CreateAsync(CreateExerciseDto dto);

    Task<ExerciseResponseDto?> GetByIdAsync(Guid id);

    Task<IEnumerable<ExerciseResponseDto>> GetByLessonAsync(Guid lessonId);

    Task DeleteAsync(Guid id);
}