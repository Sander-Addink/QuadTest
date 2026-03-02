using System.Text.Json;
using Trivia.API.Models;

namespace Trivia.API.Implementation;

internal class TriviaAPI : ITriviaAPI
{
    private readonly HttpClient _httpClient;

    public TriviaAPI(TriviaAPISettings settings)
    {
        _httpClient = new HttpClient()
        {
            BaseAddress = new Uri(settings.EndpointBase)
        };
    }

    public async Task<TriviaResponse?> RequestQuestions(TriviaRequest request)
    {
        var response = await _httpClient.GetAsync(request.CreateUrlQueryPart());
        var responseText = await response.Content.ReadAsStringAsync();
        var triviaResponse = JsonSerializer.Deserialize<TriviaResponse>(responseText);

        return triviaResponse;
    }
}
