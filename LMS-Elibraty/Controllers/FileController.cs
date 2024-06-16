using LMS_Elibraty.Data;
using LMS_Elibraty.Services.File;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LMS_Elibraty.Controllers
{
    [Authorize(Policy = "Admin,Teacher")]
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly IFileService _fileService;

        public FileController(IFileService fileService)
        {
            _fileService = fileService;
        }

        [HttpPost("{lessonId}")]
        public async Task<ActionResult<Files>> Create(IFormFile file, int lessonId)
        {
            if (file == null || file.Length == 0)
                return BadRequest("No file uploaded or file is empty.");
            var createdFile = await _fileService.Create(file, lessonId);
            return Ok(createdFile);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Files>>> All()
        {
            var files = await _fileService.All();
            return Ok(files);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Files>> Details(int id)
        {
            var file = await _fileService.Details(id);
            if (file == null)
            {
                return NotFound();
            }

            return Ok(file);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Files>> Update(int id,Files updatedFile)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var file = await _fileService.Update(id, updatedFile);
            if (file == null)
                return NotFound();
            return Ok(file);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _fileService.Delete(id);
            if (!result)
                return NotFound();
            return NoContent();
        }
    }
}
