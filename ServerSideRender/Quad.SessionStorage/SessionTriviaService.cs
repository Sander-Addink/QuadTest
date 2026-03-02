using Quad.Shared.Mappers;
using Quad.Shared;
using Quad.Shared.DTO;
using Trivia.API;
using Trivia.API.Enums;
using Trivia.API.Models;

namespace Quad.SessionStorage;

internal class SessionTriviaService : ITriviaService
{
    private readonly ITriviaAPI _api;
    private readonly QuestionSessionState _state;
    private readonly QuestionMapper _mapper;

    private bool IsRequestingQuestions { get; set; } = false;

    public SessionTriviaService(ITriviaAPI api, QuestionSessionState state, QuestionMapper mapper)
    {
        _api = api;
        _state = state;
        _mapper = mapper;
    }

    public async Task<Guid?> CheckAnswer(Guid questionId, Guid answerId)
    {
        return await _state.AnswerQuestion(questionId, answerId);
    }

    public async Task<IEnumerable<QuestionDTO>> GetQuestions()
    {
        while(IsRequestingQuestions)
        {
            await Task.Delay(100);
        }

        if (_state.Questions.Count == 0)
        {
            await _state.LoadState();
        }

        return _state.Questions.Select(q => new QuestionDTO()
        {
            Question = q.QuestionText,
            QuestionId = q.Guid,
            Answers = q.Answers,
            Answered = q.SelectedAnswer,
            Correct = q.SelectedAnswer == null ? null : q.CorrectAnswer
        });
    }

    public Task<ResultDTO> GetResult()
        => Task.FromResult(new ResultDTO()
        {
            TotalQuestions = _state.Questions.Count,
            CorrectAnswers = _state.Questions.Where(q => q.SelectedAnswer == q.CorrectAnswer).Count(),
            AllAnswered = !_state.Questions.Any(q => q.SelectedAnswer == null)
        });

    public async Task StartTrivia(int amount, Category category = Category.ANY, Trivia.API.Enums.Type type = Trivia.API.Enums.Type.ANY, Difficulty difficulty = Difficulty.ANY)
    {
        IsRequestingQuestions = true;
        try
        {
            var response = await _api.RequestQuestions(new TriviaRequest()
            {
                NumberOfQuestions = amount,
                Category = category,
                Difficulty = difficulty,
                Type = type
            });

            if (response != null)
            {
                await _state.AddQuestionRange(response.Questions.Select(q => _mapper.MapToQuestion(q)));
            }
        } finally
        {
            IsRequestingQuestions = false;
        }
    }

    public async Task FinishTrivia()
    {
        await _state.ClearQuestions();
    }
}
