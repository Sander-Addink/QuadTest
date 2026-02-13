using Trivia.API.Implementation;
using Trivia.API.Models;

var settings = new TriviaAPISettings()
{
    EndpointBase = "https://opentdb.com/api.php"
};
var api = new TriviaAPI(settings);
await api.RequestQuestions(new TriviaRequest()
{
    NumberOfQuestions = 15,
    Type = Trivia.API.Enums.Type.MULTIPLE,
    Difficulty = Trivia.API.Enums.Difficulty.MEDIUM,
    Category = Trivia.API.Enums.Category.ENTERTAINMENT_TV
});
