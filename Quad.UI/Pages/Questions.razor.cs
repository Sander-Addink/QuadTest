using Quad.Shared.DTO;

namespace Quad.UI.Pages;

public partial class Questions : PageBase
{
    private List<QuestionDTO> AllQuestions { get; set; } = new();

    private int _selectedQuestionIndex = -1;
    private int SelectedQuestionIndex
    {
        get => _selectedQuestionIndex;
        set
        {
            _selectedQuestionIndex = value;
            StateHasChanged();
        }
    }
    private QuestionDTO? SelectedQuestion => AllQuestions.Count - 1 >= SelectedQuestionIndex ? AllQuestions[SelectedQuestionIndex] : null;

    private Guid? _selectedAnswer { get; set; }
    private Guid? SelectedAnswer
    {
        get => _selectedAnswer;
        set
        {
            _selectedAnswer = value;
            StateHasChanged();
        }
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender) {
            var result = await Service.GetQuestions();
            AllQuestions = result.ToList();
            SelectedQuestionIndex = 0;
        }
        await base.OnAfterRenderAsync(firstRender);
    }

    private string GetAnswerClass(Guid optionKey)
    {
        if (SelectedQuestion == null || SelectedQuestion.Answered == null) return "";
        if (SelectedQuestion.Correct == optionKey) return "correct-answer";
        if (optionKey == SelectedQuestion.Answered && SelectedQuestion.Correct != SelectedQuestion.Answered) return "wrong-answer";
        return "";
    }

    private async Task CheckAnswer()
    {
        if (SelectedQuestion == null || SelectedAnswer == null) return;
        var result = await Service.CheckAnswer(SelectedQuestion.QuestionId, (Guid)SelectedAnswer);
        SelectedQuestion.Answered = SelectedAnswer;
        SelectedQuestion.Correct = result;
        StateHasChanged();
    }

    private void NextClick()
    {
        if (AllQuestions.Count - 1 <= SelectedQuestionIndex)
        {
            NavManager.NavigateTo("result");
            return;
        }
        SelectedQuestionIndex++;
    }

    private void PreviousClick()
    {
        if (SelectedQuestionIndex <= 0) return;
        SelectedQuestionIndex--;
    }
}

internal class QuestionData
{
    public Guid? Answered { get; set; }
    public bool Correct { get; set; } = false;
}