using Microsoft.Extensions.DependencyInjection;
using Trivia.API.Implementation;
using Trivia.API.Models;

namespace Trivia.API;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddTrivia(this IServiceCollection services, TriviaAPISettings settings)
    {
        services.AddScoped<ITriviaAPI>(sp => new TriviaAPI(settings));
        return services;
    }
}
