using Quad.Shared.DTO;
using Trivia.API.Enums;

namespace Quad.Shared;

public interface ITriviaService
{
    Task StartTrivia(int amount, Category category = Category.ANY, Trivia.API.Enums.Type type = Trivia.API.Enums.Type.ANY, Difficulty difficulty = Difficulty.ANY);
    Task<IEnumerable<QuestionDTO>> GetQuestions();
    Task<Guid?> CheckAnswer(Guid questionId, Guid answerId);
    Task<ResultDTO> GetResult();
    Task FinishTrivia();
}
