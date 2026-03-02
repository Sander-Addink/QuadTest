using Quad.Shared;
using Quad.Shared.DTO;
using Trivia.API.Enums;

namespace Quad.API.Implementation;

internal class ApiTriviaService : ITriviaService
{
    private readonly IQuadTriviaAPI _api;

    public ApiTriviaService(IQuadTriviaAPI api)
    {
        _api = api;
    }

    public async Task<Guid?> CheckAnswer(Guid questionId, Guid answerId)
    {
        return await _api.AnswerQuestion(questionId, answerId);
    }

    public async Task FinishTrivia()
    {
        await _api.StopTriviaSession();
    }

    public async Task<IEnumerable<QuestionDTO>> GetQuestions()
    {
        return await _api.GetQuestions();
    }

    public async Task<ResultDTO> GetResult()
    {
        return (await _api.GetResult())!;
    }

    public async Task StartTrivia(int amount, Category category = Category.ANY, Trivia.API.Enums.Type type = Trivia.API.Enums.Type.ANY, Difficulty difficulty = Difficulty.ANY)
    {
        await _api.StartTriviaSession(new Models.QuadRequest()
        {
            NumberOfQuestions = amount,
            Category = category,
            Type = type,
            Difficulty = difficulty
        });
    }
}
