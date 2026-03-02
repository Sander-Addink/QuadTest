using Microsoft.Extensions.DependencyInjection;
using Quad.Shared;
using Quad.Shared.Mappers;
using Trivia.API;
using Trivia.API.Models;

namespace Quad.SessionStorage;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddTriviaSessionStorage(this IServiceCollection services, TriviaAPISettings settings)
    {
        services.AddTrivia(settings);
        services.AddScoped<ITriviaService, SessionTriviaService>();
        services.AddScoped<QuestionSessionState>();
        services.AddScoped<QuestionMapper>();
        return services;
    }
}
