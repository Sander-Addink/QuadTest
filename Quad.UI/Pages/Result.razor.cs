namespace Quad.UI.Pages;

public partial class Result : PageBase
{
    private int correctCount { get; set; }
    private int totalCount { get; set; }
    private int percentage => totalCount > 0 ? (int)((double)correctCount / totalCount * 100) : 0;
    private string statusClass => percentage >= 70 ? "text-success" : "text-danger";

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            var result = await Service.GetResult();
            if (result.TotalQuestions == 0)
            {
                NavManager.NavigateTo("");
                return;
            }

            if (!result.AllAnswered)
            {
                NavManager.NavigateTo("questions");
                return;
            }

            correctCount = result.CorrectAnswers;
            totalCount = result.TotalQuestions;
        }
        await base.OnAfterRenderAsync(firstRender);
    }

    private async Task ClickHome()
    {
        await Service.FinishTrivia();
        NavManager.NavigateTo("");
    }
}
