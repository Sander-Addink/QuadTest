using Questionnaire.Models;
using Trivia.API.Models;

namespace Questionnaire.Mappers;

public class QuestionMapper
{
    public Question MapToQuestion(TriviaQuestion input)
    {
        var question = new Question();
        question.QuestionText = input.QuestionText;
        question.Answers = input.AllAnswers.ToDictionary(_ => Guid.NewGuid(), a => a);
        question.Answered = false;
        question.CorrectAnswer = question.Answers.FirstOrDefault(a => a.Value == input.CorrectAnswer).Key;
        return question;
    }
}
