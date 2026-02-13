using Microsoft.AspNetCore.Components;
using Questionnaire.States;

namespace Questionnaire.Components.Pages;

public class PageBase : ComponentBase
{
    [Inject]
    protected NavigationManager NavManager { get; set; } = default!;

    [Inject]
    protected QuestionSessionState State { get; set; } = default!;

    protected bool IsLoading { get; set; } = true;

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            await State.LoadState();
            IsLoading = false;
            State.OnChange += StateHasChanged;
            StateHasChanged();
        }
    }

    public void Dispose() => State.OnChange -= StateHasChanged;
}
