using System.Net;
using System.Text.Json.Serialization;

namespace Trivia.API.Models;

public class TriviaResponse
{
    [JsonPropertyName("response_code")]
    public int ResponseCode { get; set; }
    [JsonPropertyName("results")]
    public List<TriviaQuestion> Questions { get; set; } = new List<TriviaQuestion>();
}

public class TriviaQuestion
{
    private string _question = default!;
    [JsonPropertyName("type")]
    public string Type
    {
        get => _question;
        set => _question = WebUtility.HtmlDecode(value);
    }

    private string _difficulty = default!;
    [JsonPropertyName("difficulty")]
    public string Difficulty
    {
        get => _difficulty;
        set => _difficulty = WebUtility.HtmlDecode(value);
    }

    private string _category = default!;
    [JsonPropertyName("category")]
    public string Category
    {
        get => _category;
        set => _category = WebUtility.HtmlDecode(value);
    }

    private string _questionText = default!;
    [JsonPropertyName("question")]
    public string QuestionText
    {
        get => _questionText;
        set => _questionText = WebUtility.HtmlDecode(value);
    }

    private string _correctAnswer = default!;
    [JsonPropertyName("correct_answer")]
    public string CorrectAnswer
    {
        get => _correctAnswer;
        set => _correctAnswer = WebUtility.HtmlDecode(value);
    }

    private List<string> _incorrectAnswers = new List<string>();
    [JsonPropertyName("incorrect_answers")]
    public List<string> WrongAnswers
    {
        get => _incorrectAnswers;
        set => _incorrectAnswers = value.Select(s => WebUtility.HtmlDecode(s)).ToList();
    }

    public List<string> AllAnswers => WrongAnswers.Append(CorrectAnswer).OrderBy(x => Guid.NewGuid()).ToList();
}
