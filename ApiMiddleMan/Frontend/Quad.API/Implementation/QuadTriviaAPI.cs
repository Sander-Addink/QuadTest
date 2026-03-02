using Quad.API.Models;
using System.Net.Http.Json;
using System.Text.Json;

namespace Quad.API.Implementation;

internal class QuadTriviaAPI : IQuadTriviaAPI
{
    private readonly HttpClient _httpClient;

    public QuadTriviaAPI(IHttpClientFactory clientFactory)
    {
        _httpClient = clientFactory.CreateClient("api");
    }

    public async Task<Guid?> AnswerQuestion(Guid questionId, Guid answerId)
    {
        var response = await _httpClient.PostAsync($"api/quiz/{questionId}/{answerId}", null);
        var responseText = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<Guid?>(responseText);
    }

    public async Task<IEnumerable<QuadQuestionResponse>> GetQuestions()
    {
        var response = await _httpClient.GetAsync("api/quiz");
        var responseText = await response.Content.ReadAsStringAsync();
        var triviaResponse = JsonSerializer.Deserialize<IEnumerable<QuadQuestionResponse>>(responseText, new JsonSerializerOptions()
        {
            PropertyNameCaseInsensitive = true
        });
        return triviaResponse ?? new List<QuadQuestionResponse>();
    }

    public async Task<QuadResultResponse?> GetResult()
    {
        var response = await _httpClient.GetAsync("api/quiz/result");
        var responseText = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<QuadResultResponse>(responseText, new JsonSerializerOptions()
        {
            PropertyNameCaseInsensitive = true
        });
    }

    public async Task StartTriviaSession(QuadRequest request)
    {
        var response = await _httpClient.PostAsJsonAsync("/api/triviasession", request);
    }

    public async Task StopTriviaSession()
    {
        var response = await _httpClient.DeleteAsync("/api/triviasession");
    }
}
