public class AnswerExerciseDto
{
    public Guid SessionId { get; set; }
    public Guid ExerciseId { get; set; }
    public string Answer { get; set; } = string.Empty;
}