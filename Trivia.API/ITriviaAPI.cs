using Trivia.API.Models;

namespace Trivia.API;
public interface ITriviaAPI
{
    public Task<TriviaResponse?> RequestQuestions(TriviaRequest request);
}
