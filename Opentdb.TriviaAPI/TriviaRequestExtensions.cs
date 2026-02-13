using System.Text;
using Trivia.API.Models;

namespace Trivia.API;

internal static class TriviaRequestExtensions
{
    public static string CreateUrlQueryPart(this TriviaRequest request)
    {
        var queryPart = new StringBuilder();
        queryPart.Append($"?amount={request.NumberOfQuestions}");

        if (request.Category != Enums.Category.ANY)
        {
            queryPart.Append($"&category={(int)request.Category}");
        }

        if (request.Type != Enums.Type.ANY)
        {
            queryPart.Append($"&type={request.Type.ToString()}");
        }

        if (request.Difficulty != Enums.Difficulty.ANY)
        {
            queryPart.Append($"&difficulty={request.Difficulty.ToString()}");
        }

        return queryPart.ToString().ToLower();
    }
}
