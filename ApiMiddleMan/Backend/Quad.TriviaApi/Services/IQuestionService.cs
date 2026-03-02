using Quad.TriviaApi.Models;

namespace Quad.TriviaApi.Services;

public interface IQuestionService
{
    Task<bool> IsAnswered(Guid questionId);
    Task CreateQuestions(TriviaRequestDTO triviaRequest);
    Task<Guid?> AnswerQuestion(Guid questionId, Guid answerId);
}
