using Microsoft.AspNetCore.Components;
using Quad.Shared;

namespace Quad.UI.Pages;

public class PageBase : ComponentBase
{
    [Inject]
    protected NavigationManager NavManager { get; set; } = default!;

    [Inject]
    protected ITriviaService Service { get; set; } = default!;

    private bool _isLoading { get; set; } = true;
    protected bool IsLoading
    {
        get => _isLoading;
        set
        {
            _isLoading = value;
            StateHasChanged();
        }
    }

    protected override Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            IsLoading = false;
        }
        return Task.CompletedTask;
    }
}
