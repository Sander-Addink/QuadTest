namespace Questionnaire.Components.Pages;

public partial class Result : PageBase
{
    private int correctCount => State.Questions.Count(q => q.SelectedAnswer == q.CorrectAnswer);
    private int totalCount => State.Questions.Count;
    private int percentage => totalCount > 0 ? (int)((double)correctCount / totalCount * 100) : 0;
    private string statusClass => percentage >= 70 ? "text-success" : "text-danger";

    private async Task ClickHome()
    {
        await State.ClearQuestions();
        NavManager.NavigateTo("");
    }
}
