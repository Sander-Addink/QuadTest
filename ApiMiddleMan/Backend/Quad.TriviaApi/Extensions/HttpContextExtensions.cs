using Quad.TriviaApi.Models;

namespace Quad.TriviaApi.Extensions;

public static class HttpContextExtensions
{
    public static TriviaData? GetTriviaSession(this HttpContext context)
        => context.Items[nameof(TriviaData)] as TriviaData;
}
