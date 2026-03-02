using Microsoft.AspNetCore.Mvc;
using Quad.Shared.DTO;
using Quad.TriviaApi.Extensions;
using Quad.TriviaApi.Filters;
using Quad.TriviaApi.Services;

namespace Quad.TriviaApi.Controllers;
[Route("api/[controller]")]
[ApiController]
public class QuizController : ControllerBase
{
    private readonly IQuestionService _questionService;

    public QuizController(IQuestionService questionService)
    {
        _questionService = questionService;
    }

    [TypeFilter<TriviaSessionResourceFilter>]
    [HttpGet]
    public ActionResult<IEnumerable<QuestionDTO>> GetQuestions()
    {
        var session = HttpContext.GetTriviaSession();
        
        if (session == null || session.Questions.Count == 0)
        {
            return NotFound(); 
        }

        return Ok(session.Questions.Select(q => new QuestionDTO()
        {
            QuestionId = q.Guid,
            Question = q.QuestionText,
            Answers = q.Answers,
            Answered = q.SelectedAnswer,
            Correct = q.SelectedAnswer == null ? null : q.CorrectAnswer
        }));
    }

    [TypeFilter<TriviaSessionResourceFilter>]
    [HttpPost("{questionId}/{answerId}")]
    public async Task<ActionResult<Guid>> CheckAnswer([FromRoute] Guid questionId, [FromRoute] Guid answerId)
    {
        if (await _questionService.IsAnswered(questionId))
        {
            return BadRequest("Question has already been answered");
        }

        var answer = await _questionService.AnswerQuestion(questionId, answerId);
        if (answer == null)
        {
            return BadRequest();
        }

        return answer;
    }

    [TypeFilter<TriviaSessionResourceFilter>]
    [HttpGet("result")]
    public ActionResult<ResultDTO> GetResults()
    {
        var session = HttpContext.GetTriviaSession();

        if (session == null || session.Questions.Count == 0)
        {
            return NotFound(); 
        }

        var result = new ResultDTO()
        {
            CorrectAnswers = session.Questions.Where(q => q.SelectedAnswer == q.CorrectAnswer && q.CorrectAnswer != null).Count(),
            TotalQuestions = session.Questions.Count,
            AllAnswered = !session.Questions.Any(q => q.SelectedAnswer == null)
        };

        return Ok(result);
    }
}
