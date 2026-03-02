using Quad.Shared.Models;

namespace Quad.TriviaApi.Models;

public class TriviaData
{
    public List<Question> Questions { get; set; } = new();
}
