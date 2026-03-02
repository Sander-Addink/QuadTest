using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Quad.Shared.Models;

namespace Quad.SessionStorage;

internal class QuestionSessionState
{
    private readonly ProtectedSessionStorage _sessionStorage;

    private readonly string QuestionsKey = "QuestionsStorage";
    private List<Question> _questions = new();
    public IReadOnlyCollection<Question> Questions => _questions.AsReadOnly();

    
    public QuestionSessionState(ProtectedSessionStorage sessionStorage)
    {
        _sessionStorage = sessionStorage;
    }

    public async Task AddQuestionRange(IEnumerable<Question> questions)
    {
        foreach (var question in questions)
        {
            await AddQuestion(question);
        }
    }

    public async Task AddQuestion(Question question)
    {
        _questions.Add(question);
        await Store();
    }

    public async Task<Guid?> AnswerQuestion(Guid questionId, Guid answerId)
    {
        var question = _questions.FirstOrDefault(q => q.Guid == questionId);
        if (question == null) return null;

        question.SelectedAnswer = answerId;
        await Store();

        return question.CorrectAnswer;
    }

    public async Task DeleteQuestion(Guid guid)
    {
        await LoadState();
        var question = _questions.FirstOrDefault(q => q.Guid == guid);
        if (question == null) return;

        var indexToRemove = _questions.IndexOf(question);
        _questions.Remove(question);

        await Store();
    }

    public async Task ClearQuestions()
    {
        _questions.Clear();
        await Store();
    }

    private async Task Store()
    {
        await _sessionStorage.SetAsync(QuestionsKey, _questions);
    }

    public async Task LoadState()
    {
        var questionsResult = await _sessionStorage.GetAsync<List<Question>>(QuestionsKey);
        if (questionsResult.Success && questionsResult.Value != null) _questions = questionsResult.Value;
    }
}
