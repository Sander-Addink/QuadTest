using Quad.API.Models;

namespace Quad.API;
public interface IQuadTriviaAPI
{
    Task StartTriviaSession(QuadRequest request);
    Task<IEnumerable<QuadQuestionResponse>> GetQuestions();
    Task<Guid?> AnswerQuestion(Guid questionId, Guid answerId);
    Task<QuadResultResponse?> GetResult();
    Task StopTriviaSession();
}
