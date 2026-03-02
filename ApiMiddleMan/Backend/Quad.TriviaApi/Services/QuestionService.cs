using Quad.Shared.Mappers;
using Quad.TriviaApi.Models;
using Trivia.API;

namespace Quad.TriviaApi.Services;

public class QuestionService : IQuestionService
{
    private readonly ITriviaCacheService _cacheService;
    private readonly ITriviaAPI _api;
    private readonly QuestionMapper _mapper;

    public QuestionService(ITriviaCacheService cacheService, ITriviaAPI api, QuestionMapper mapper)
    {
        _cacheService = cacheService;
        _api = api;
        _mapper = mapper;
    }

    public async Task<bool> IsAnswered(Guid questionId)
    {
        var session = await _cacheService.GetCacheData();
        if (session == null) return false;
        return session.Questions.Any(q => q.Guid == questionId && q.SelectedAnswer != null);
    }

    public async Task<Guid?> AnswerQuestion(Guid questionId, Guid answerId)
    {
        var session = await _cacheService.GetCacheData();
        if (session == null) return null;

        var question = session.Questions.FirstOrDefault(q => q.Guid == questionId);
        if (question == null) return null;

        question.SelectedAnswer = answerId;
        await _cacheService.StoreCacheData(session);
        return question.CorrectAnswer;
    }

    public async Task CreateQuestions(TriviaRequestDTO triviaRequest)
    {
        var result = await _api.RequestQuestions(triviaRequest);

        if (result == null) return;
        var questions = result.Questions.Select(q => _mapper.MapToQuestion(q));
        await _cacheService.StoreCacheData(new TriviaData()
        {
            Questions = questions.ToList()
        });
    }
}
