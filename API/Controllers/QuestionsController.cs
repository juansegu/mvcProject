using Microsoft.AspNetCore.Mvc;
using API.Models;

[ApiController]
[Route("api/[controller]")]
public class QuestionsController : ControllerBase
{
    [HttpGet]
    public ActionResult<IEnumerable<Question>> GetQuestions()
    {
        var questions = new List<Question>
        {
           new Question{ Id = 1, Title = "What is 2+2?", KindOfQuestion = KindOfQuestion.Math, Ask = "Calculate the sum of 2 and 2.", Answer = "4" },
           new Question{ Id = 2, Title = "What is the capital of France?", KindOfQuestion = KindOfQuestion.Geography, Ask = "Write the France capital", Answer= "Paris" },
           new Question{ Id = 1, Title = "What is 3-5?", KindOfQuestion = KindOfQuestion.Math, Ask = "Calculate the substracion of 3 less 5.", Answer = "-2" },
           new Question{ Id = 1, Title = "What is 11+12?", KindOfQuestion = KindOfQuestion.Math, Ask = "Calculate the sum of 11 and 12.", Answer = "23" },

        };

        return Ok(questions);
    }
}