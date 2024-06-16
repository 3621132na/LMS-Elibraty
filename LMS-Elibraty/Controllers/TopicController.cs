using LMS_Elibraty.Data;
using LMS_Elibraty.Services.Topics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LMS_Elibraty.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TopicController : ControllerBase
    {
        private readonly ITopicService _topicService;

        public TopicController(ITopicService topicService)
        {
            _topicService = topicService;
        }
        [Authorize(Policy = "Admin,Teacher")]
        [HttpPost]
        public async Task<ActionResult<Topic>> Create(Topic topic,string subjectId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var createdTopic = await _topicService.Create(topic, subjectId);
            return Ok(createdTopic);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Topic>>> All()
        {
            var topics = await _topicService.All();
            return Ok(topics);
        }
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<Topic>> Details(int id)
        {
            var topic = await _topicService.Details(id);
            if (topic == null)
                return NotFound();
            return Ok(topic);
        }
        [Authorize(Policy = "Admin,Teacher")]
        [HttpPut("{id}")]
        public async Task<ActionResult<Topic>> Update(int id,Topic updatedTopic)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var topic = await _topicService.Update(id, updatedTopic);
            if (topic == null)
                return NotFound();
            return Ok(topic);
        }
        [Authorize(Policy = "Admin,Teacher")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _topicService.Delete(id);
            if (!result)
                return NotFound();
            return NoContent();
        }
    }
}
