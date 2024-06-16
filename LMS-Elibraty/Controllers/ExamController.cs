using LMS_Elibraty.Data;
using LMS_Elibraty.Services.Exams;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LMS_Elibraty.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExamController : ControllerBase
    {
        private readonly IExamService _examService;

        public ExamController(IExamService examService)
        {
            _examService = examService;
        }
        [Authorize(Policy = "Admin,Teacher")]
        [HttpPost("{subjectId}")]
        public async Task<ActionResult<Exam>> Create(string subjectId, Exam exam)
        {
            var createdExam = await _examService.Create(exam, subjectId);
            return Ok(createdExam);
        }
        [Authorize(Policy = "Admin,Teacher")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Exam>>> All()
        {
            var exams = await _examService.All();
            return Ok(exams);
        }
        [Authorize(Policy = "Admin,Teacher")]
        [HttpGet("{id}")]
        public async Task<ActionResult<Exam>> Details(string id)
        {
            var exam = await _examService.Details(id);
            if (exam == null)
                return NotFound();
            return Ok(exam);
        }
        [Authorize(Policy = "Admin,Teacher")]
        [HttpPut("{id}")]
        public async Task<ActionResult<Exam>> Update(string id,Exam updatedExam)
        {
            var result = await _examService.Update(id, updatedExam);
            if (result == null)
                return NotFound();
            return Ok(result);
        }
        [Authorize(Policy = "Admin")]
        [HttpPatch("{id}/status")]
        public async Task<ActionResult<Subject>> UpdateExamStatus(string id, string newStatus)
        {
            if (string.IsNullOrWhiteSpace(newStatus))
                return BadRequest("Status cannot be empty");
            var updatedSubject = await _examService.UpdateStatus(id, newStatus);
            if (updatedSubject == null)
                return NotFound();
            return Ok(updatedSubject);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await _examService.Delete(id);
            if (!result)
                return NotFound();
            return NoContent();
        }
    }
}
