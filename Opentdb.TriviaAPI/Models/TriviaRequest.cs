using Trivia.API.Enums;

namespace Trivia.API.Models;

public class TriviaRequest
{
    public int NumberOfQuestions { get; set; }
    public Category Category { get; set; }
    public Difficulty Difficulty { get; set; }
    public Enums.Type Type { get; set; }
}


