using System.ComponentModel.DataAnnotations;
using Trivia.API.Models;

namespace Quad.TriviaApi.Models;

public class TriviaRequestDTO : TriviaRequest
{
    [Required]
    [Range(1, int.MaxValue)]
    public override int NumberOfQuestions { get; set; }
}
