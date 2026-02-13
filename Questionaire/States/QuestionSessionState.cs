using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Questionnaire.Models;

namespace Questionnaire.States;

public class QuestionSessionState
{
    private readonly ProtectedSessionStorage _sessionStorage;
    public event Action? OnChange;

    private const string QuestionsKey = "questions_list";
    private List<Question> _questions = new();
    public IReadOnlyCollection<Question> Questions => _questions.AsReadOnly();

    private const string SelectedIndexKey = "selected_questions_index";
    public int SelectedIndex { get; private set; } = -1;

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
        if (_questions.Count == 1) SelectedIndex = 0;
        await SafeAndNotify();
    }

    public async Task SelectAnswer(Guid key)
    {
        if (Selected == null || !Selected.Answers.ContainsKey(key) || Selected.Answered) return;
        Selected.SelectedAnswer = key;
        await SafeAndNotify();
    }

    public async Task Answer()
    {
        if (Selected == null || Selected.SelectedAnswer == null || !Selected.Answers.ContainsKey((Guid)Selected.SelectedAnswer)) return;
        Selected.Answered = true;
        await SafeAndNotify();
    }

    public async Task DeleteQuestion(Guid guid)
    {
        var question = _questions.FirstOrDefault(q => q.Guid == guid);
        if (question == null) return;

        var indexToRemove = _questions.IndexOf(question);
        _questions.Remove(question);

        if (SelectedIndex >= _questions.Count) SelectedIndex = _questions.Count - 1;
        if (_questions.Count == 0) SelectedIndex = -1;

        await SafeAndNotify();
    }

    public async Task ClearQuestions()
    {
        _questions.Clear();
        SelectedIndex = -1;
        await SafeAndNotify();
    }

    public Question? Selected => SelectedIndex != -1 ? _questions[SelectedIndex] : null;
    public async Task<Question?> SelectNext()
    {
        if (SelectedIndex < _questions.Count - 1 && SelectedIndex >= 0)
        {
            SelectedIndex++;
            await SafeAndNotify();
        }
        return Selected;
    }

    public async Task<Question?> SelectPrevious()
    {
        if (SelectedIndex > 0)
        {
            SelectedIndex--;
            await SafeAndNotify();
        }
        return Selected;
    }

    private async Task SafeAndNotify()
    {
        await _sessionStorage.SetAsync(QuestionsKey, _questions);
        await _sessionStorage.SetAsync(SelectedIndexKey, SelectedIndex);
        OnChange?.Invoke();
    }

    public async Task LoadState()
    {
        var questionsResult = await _sessionStorage.GetAsync<List<Question>>(QuestionsKey);
        if (questionsResult.Success && questionsResult.Value != null) _questions = questionsResult.Value;

        var indexResult = await _sessionStorage.GetAsync<int?>(SelectedIndexKey);
        if (indexResult.Success && indexResult.Value != null) SelectedIndex = (int)indexResult.Value;
        OnChange?.Invoke();
    }
}
