using Quad.Shared.Models;
using Trivia.API.Models;

namespace Quad.Shared.Mappers;

public class QuestionMapper
{
    public Question MapToQuestion(TriviaQuestion input)
    {
        var question = new Question();
        question.QuestionText = input.QuestionText;
        question.Answers = input.AllAnswers.ToDictionary(_ => Guid.NewGuid(), a => a);
        question.CorrectAnswer = question.Answers.FirstOrDefault(a => a.Value == input.CorrectAnswer).Key;
        return question;
    }
}
