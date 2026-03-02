namespace Quad.Shared.DTO;

public class QuestionDTO
{
    public Guid QuestionId { get; set; } = default!;
    public string Question { get; set; } = default!;
    public Dictionary<Guid, string> Answers { get; set; } = new Dictionary<Guid, string>();
    public Guid? Answered { get; set; }
    public Guid? Correct { get; set; }
}
