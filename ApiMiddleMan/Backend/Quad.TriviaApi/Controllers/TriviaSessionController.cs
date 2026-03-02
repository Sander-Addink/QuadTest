using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Quad.TriviaApi.Models;
using Quad.TriviaApi.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Quad.TriviaApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TriviaSessionController : ControllerBase
{
    private readonly ITriviaCacheService _sessionService;
    private readonly IQuestionService _questionService;

    public TriviaSessionController(ITriviaCacheService sessionService, IQuestionService questionService)
    {
        _sessionService = sessionService;
        _questionService = questionService;
    }

    [HttpPost]
    public async Task<IActionResult> StartSession([FromBody] TriviaRequestDTO triviaRequest)
    {
        var key = Guid.NewGuid().ToString();
        HttpContext.Session.SetString(nameof(TriviaData), key);
        await _questionService.CreateQuestions(triviaRequest);

        return Ok();
    }

    [HttpDelete]
    public async Task<IActionResult> EndSession()
    {
        await _sessionService.ClearCache();
        return NoContent();
    }
}
