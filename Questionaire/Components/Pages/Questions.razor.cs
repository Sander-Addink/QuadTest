namespace Questionnaire.Components.Pages;

public partial class Questions : PageBase
{
    private string GetAnswerClass(Guid optionKey)
    {
        if (State.Selected == null || !State.Selected.Answered) return ""; 
        if (optionKey == State.Selected.CorrectAnswer) return "correct-answer"; 
        if (optionKey == State.Selected.SelectedAnswer) return "wrong-answer"; 
        return "";
    } 
    private async Task SelectOption(Guid? optionKey)
    {
        if (optionKey == null) return;
        await State.SelectAnswer((Guid)optionKey);
    }

    private async Task CheckAnswer()
    {
        await State.Answer();
    }

    private async Task NextClick()
    {
        if (State.SelectedIndex == State.Questions.Count -1)
        {
            NavManager.NavigateTo("result");
            return;
        }
        await State.SelectNext();
    }
}
