using LMS_Elibraty.Data;
using LMS_Elibraty.Services.Lessons;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LMS_Elibraty.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LessonController : ControllerBase
    {
        private readonly ILessonService _lessonService;

        public LessonController(ILessonService lessonService)
        {
            _lessonService = lessonService;
        }
        [Authorize(Policy = "Admin,Teacher")]
        [HttpPost]
        public async Task<ActionResult<Lesson>> Create(Lesson lesson, int topicId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var createdLesson = await _lessonService.Create(lesson, topicId);
            return Ok(createdLesson);
        }
        [Authorize(Policy = "Admin,Teacher")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Lesson>>> GetAllLessons()
        {
            var lessons = await _lessonService.All();
            return Ok(lessons);
        }
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<Lesson>> Details(int id)
        {
            var lesson = await _lessonService.Details(id);
            if (lesson == null)
                return NotFound();
            return Ok(lesson);
        }
        [Authorize(Policy = "Admin,Teacher")]
        [HttpPut("{id}")]
        public async Task<ActionResult<Lesson>> Update(int id,Lesson updatedLesson)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var lesson = await _lessonService.Update(id, updatedLesson);
            if (lesson == null)
                return NotFound();
            return Ok(lesson);
        }
        [Authorize(Policy = "Admin,Teacher")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _lessonService.Delete(id);
            if (!result)
                return NotFound();
            return NoContent();
        }
    }
}
