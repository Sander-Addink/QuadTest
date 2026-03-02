using Microsoft.AspNetCore.Components;
using Quad.UI.Pages;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using Trivia.API.Enums;

namespace Quad.UI.Pages;

public partial class Home : PageBase
{
    [SupplyParameterFromForm]
    private FormModel formData { get; set; } = new();

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            await Service.FinishTrivia();
        }
        await base.OnAfterRenderAsync(firstRender);
    }

    private async Task HandleSubmit()
    {
        await Service.StartTrivia(formData.QuestionsAmount, formData.Category, formData.Type, formData.Difficulty);
        NavManager.NavigateTo("questions");
    }

    private string FormatEnumName(Enum value) =>
        value.ToString().ToLower().Replace("_", " ");
}

internal class FormModel
{
    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "Please enter a value greater than 0")]
    public int QuestionsAmount { get; set; }

    public Category Category { get; set; } = Category.ANY;
    public Trivia.API.Enums.Type Type { get; set; } = Trivia.API.Enums.Type.ANY;
    public Difficulty Difficulty { get; set; } = Difficulty.ANY;
}
