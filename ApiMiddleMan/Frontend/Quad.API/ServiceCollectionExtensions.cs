using Microsoft.Extensions.DependencyInjection;
using Quad.API.Implementation;
using Quad.API.Models;
using Quad.Shared;

namespace Quad.API;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddQuadApi(this IServiceCollection services, QuadApiSettings settings)
    {
        services.AddTransient<CookieDelegatingHandler>();
        services.AddHttpClient("api", client => client.BaseAddress = new Uri(settings.EndpointBase))
            .AddHttpMessageHandler<CookieDelegatingHandler>();
        services.AddScoped<ITriviaService, ApiTriviaService>();
        services.AddScoped<IQuadTriviaAPI, QuadTriviaAPI>();
        return services;
    }
}
