using LMS_Elibraty.Data;
using LMS_Elibraty.Services.Subjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LMS_Elibraty.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubjectController : ControllerBase
    {
        private readonly ISubjectService _subjectService;

        public SubjectController(ISubjectService subjectService)
        {
            _subjectService = subjectService;
        }

        [HttpPost]
        public async Task<ActionResult<Subject>> CreateSubject(Subject subject)
        {
            var createdSubject = await _subjectService.Create(subject);
            return Ok(createdSubject);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Subject>> Details(string id)
        {
            var subject = await _subjectService.Details(id);
            if (subject == null)
            {
                return NotFound();
            }
            return Ok(subject);
        }

        [HttpGet]
        [Authorize(Policy = "Admin")]
        public async Task<ActionResult<IEnumerable<Subject>>> All()
        {
            var subjects = await _subjectService.All();
            return Ok(subjects);
        }
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Subject>>> AllById()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var subjects = await _subjectService.AllById(userId);
            return Ok(subjects);
        }
        [Authorize(Policy = "Admin")]
        [HttpPut("{id}")]
        public async Task<ActionResult<Subject>> UpdateSubject(string id,Subject subject)
        {
            var updatedSubject = await _subjectService.Update(id, subject);
            if (updatedSubject == null)
                return NotFound();
            return Ok(updatedSubject);
        }
        [Authorize(Policy = "Admin")]
        [HttpPatch("{id}/status")]
        public async Task<ActionResult<Subject>> UpdateSubjectStatus(string id,string newStatus)
        {
            if (string.IsNullOrWhiteSpace(newStatus))
                return BadRequest("Status cannot be empty");
            var updatedSubject = await _subjectService.UpdateStatus(id, newStatus);
            if (updatedSubject == null)
                return NotFound();
            return Ok(updatedSubject);
        }
        [Authorize(Policy = "Admin,Teacher")]
        [HttpPost("{id}/add-to-classes")]
        public async Task<IActionResult> AddSubjectToClasses(string id,IEnumerable<string> classIds)
        {
            var success = await _subjectService.AddToClasses(id, classIds);
            if (!success)
                return BadRequest("Failed to add subject to classes.");
            return Ok("Subject added to classes successfully.");
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSubject(string id)
        {
            var result = await _subjectService.Delete(id);
            if (!result)
                return NotFound();
            return NoContent();
        }
    }
}
