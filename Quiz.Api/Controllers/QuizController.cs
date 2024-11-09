using Microsoft.AspNetCore.Mvc;

namespace Quiz.Api.Controllers
{
	[ApiController]
	public class QuizController : ControllerBase
	{
        // daje pytanie z okre�lonej kategorii wraz z odpowiedziami
        // URL => https://localhost:7000/getquestion

        [HttpGet]
        [Route("getquestion")]
        public IActionResult GetQuestion([FromQuery] int category)
        {
            return Ok();
               
        }

        // sprawdza czy dana odpwied� jest prawid�owa lub nie
        // URL => https://localhost:7000/checkanswer

        [HttpGet]
        [Route("checkanswer")]
        public IActionResult CheckAnswer([FromQuery] int answerId)
        {
            return Ok();
        }

    }
}
