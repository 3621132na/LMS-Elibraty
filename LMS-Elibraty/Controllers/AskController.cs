using LMS_Elibraty.Data;
using LMS_Elibraty.Services.Asks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LMS_Elibraty.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AskController : ControllerBase
    {
        private readonly IAskService _askService;

        public AskController(IAskService askService)
        {
            _askService = askService;
        }

        [HttpPost]
        public async Task<ActionResult<Ask>> Create(Ask ask, string subjectId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var createdAsk = await _askService.Create(ask, subjectId);
            return Ok(createdAsk);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Ask>>> All()
        {
            var asks = await _askService.All();
            return Ok(asks);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Ask>> Details(int id)
        {
            var ask = await _askService.Details(id);
            if (ask == null)
                return NotFound();
            return Ok(ask);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Ask>> Update(int id,Ask updatedAsk)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var ask = await _askService.Update(id, updatedAsk);
            if (ask == null)
                return NotFound();
            return Ok(ask);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _askService.Delete(id);
            if (!result)
                return NotFound();
            return NoContent();
        }
    }
}
