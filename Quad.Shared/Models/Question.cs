namespace Quad.Shared.Models;

public class Question
{
    public Guid Guid { get; set; } = Guid.NewGuid();
    public string QuestionText { get; set; } = default!;
    public Dictionary<Guid, string> Answers { get; set; } = new();
    public Guid? CorrectAnswer { get; set; }
    public Guid? SelectedAnswer { get; set; }
}
