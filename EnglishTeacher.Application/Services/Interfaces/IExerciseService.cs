using EnglishTeacher.Application.DTOs.Exercises;

public interface IExerciseService
{
    Task<ExerciseResponseDto> CreateAsync(CreateExerciseDto dto);
    Task<ExerciseResponseDto?> GetByIdAsync(Guid id);
    Task<IEnumerable<ExerciseResponseDto>> GetByLessonAsync(Guid lessonId);
    Task DeleteAsync(Guid id);

    // NOVO: submeter resposta do aluno e receber correção imediata
    Task<bool> SubmitAnswerAsync(Guid exerciseId, Guid studentId, string studentAnswer);
}