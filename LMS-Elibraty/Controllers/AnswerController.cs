using LMS_Elibraty.Data;
using LMS_Elibraty.Services.Answers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LMS_Elibraty.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AnswerController : ControllerBase
    {
        private readonly IAnswerService _answerService;

        public AnswerController(IAnswerService answerService)
        {
            _answerService = answerService;
        }

        [HttpPost]
        public async Task<ActionResult<Answer>> Create(Answer answer, int askId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var createdAnswer = await _answerService.Create(answer, askId);
            return Ok(createdAnswer);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Answer>>> All()
        {
            var answers = await _answerService.All();
            return Ok(answers);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Answer>> Details(int id)
        {
            var answer = await _answerService.Details(id);
            if (answer == null)
                return NotFound();
            return Ok(answer);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Answer>> Update(int id, Answer updatedAnswer)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var answer = await _answerService.Update(id, updatedAnswer);
            if (answer == null)
                return NotFound();
            return Ok(answer);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _answerService.Delete(id);
            if (!result)
                return NotFound();
            return NoContent();
        }
    }
}
