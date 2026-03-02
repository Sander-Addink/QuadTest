using Microsoft.AspNetCore.Mvc.Filters;
using Quad.TriviaApi.Models;
using Quad.TriviaApi.Services;

namespace Quad.TriviaApi.Filters;

public class TriviaSessionResourceFilter : IAsyncResourceFilter
{
    private readonly ITriviaCacheService _sessionService;

    public TriviaSessionResourceFilter(ITriviaCacheService sessionService)
    {
        _sessionService = sessionService;
    }

    public async Task OnResourceExecutionAsync(ResourceExecutingContext context, ResourceExecutionDelegate next)
    {
        var session = await _sessionService.GetCacheData();
        if (session == null)
        {
            context.HttpContext.Response.StatusCode = StatusCodes.Status404NotFound;
            return;
        }
        context.HttpContext.Items[nameof(TriviaData)] = session;
        await next();
    }
}
