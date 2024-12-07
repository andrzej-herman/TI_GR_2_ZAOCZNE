using Microsoft.AspNetCore.Mvc;
using Quiz.Api.Services;

namespace Quiz.Api.Controllers
{
	[ApiController]
	public class QuizController : ControllerBase
	{
        private readonly IQuizService _service;

        public QuizController(IQuizService service)
        {
            _service = service;
        }

        // daje pytanie z okreœlonej kategorii wraz z odpowiedziami
        // URL => https://localhost:7000/getquestion

        [HttpGet]
        [Route("getquestion")]
        public async Task<IActionResult> GetQuestion([FromQuery] int category)
        {
            var question = await _service.GetQuestion(category);
            return question.Error == null ? Ok(question) : BadRequest(question.Error);
        }

        // sprawdza czy dana odpwiedŸ jest prawid³owa lub nie
        // URL => https://localhost:7000/checkanswer

        [HttpGet]
        [Route("checkanswer")]
        public async Task<IActionResult> CheckAnswerAndGetNextCategory([FromQuery] Guid answerId, [FromQuery] int category)
        {
            var result = await _service.GetCheckAnswerAndGetNextCategory(answerId, category);
            return result.Error == null ? Ok(result) : Problem(result.Error);
        }

    }
}
