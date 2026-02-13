using Microsoft.AspNetCore.Components;
using Questionnaire.Mappers;
using System.ComponentModel.DataAnnotations;
using Trivia.API;
using Trivia.API.Enums;
using Trivia.API.Models;

namespace Questionnaire.Components.Pages;

public partial class Home : PageBase
{
    [Inject]
    private ITriviaAPI api { get; set; } = default!;

    [Inject]
    private QuestionMapper mapper { get; set; } = default!;

    [SupplyParameterFromForm]
    private FormModel formData { get; set; } = new();

    private async Task HandleSubmit()
    {
        var questions = await api.RequestQuestions(new TriviaRequest()
        {
            NumberOfQuestions = formData.QuestionsAmount,
            Category = formData.Category,
            Difficulty = formData.Difficulty,
            Type = formData.Type,
        });
        if (questions == null) return;

        await State.ClearQuestions();
        await State.AddQuestionRange(questions.Questions.Select(q => mapper.MapToQuestion(q)));
        NavManager.NavigateTo("questionnaire");
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
