namespace Questionnaire.Models;

public class Question
{
    public Guid Guid { get; set; } = Guid.NewGuid();
    public string QuestionText { get; set; } = default!;
    public Dictionary<Guid, string> Answers { get; set; } = new();
    public Guid CorrectAnswer { get; set; } = default!;
    public Guid? SelectedAnswer { get; set; }
    public bool Answered { get; set; } = false;
}
