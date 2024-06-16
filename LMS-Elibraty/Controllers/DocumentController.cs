using LMS_Elibraty.Data;
using LMS_Elibraty.Models;
using LMS_Elibraty.Services.Document;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LMS_Elibraty.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentController : ControllerBase
    {
        private readonly IDocumentService _documentService;

        public DocumentController(IDocumentService documentService)
        {
            _documentService = documentService;
        }
        [Authorize(Policy = "Admin,Teacher")]
        [HttpPost]
        public async Task<ActionResult<Documents>> Create(Documents document)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var createdDocument = await _documentService.Create(document);
            return Ok(createdDocument);
        }
        [Authorize(Policy = "Admin,Teacher")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Documents>>> All()
        {
            var documents = await _documentService.All();
            return Ok(documents);
        }
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<Documents>> Details(int id)
        {
            var document = await _documentService.Details(id);
            if (document == null)
                return NotFound();
            return Ok(document);
        }
        [Authorize(Policy = "Admin,Teacher")]
        [HttpPut("{id}")]
        public async Task<ActionResult<Documents>> Update(int id, Documents updatedDocument)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var document = await _documentService.Update(id, updatedDocument);
            if (document == null)
                return NotFound();
            return Ok(document);
        }
        [Authorize(Policy = "Admin")]
        [HttpPatch("{id}/status")]
        public async Task<ActionResult<Documents>> UpdateStatus(int id, UpdateStatusRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var document = await _documentService.UpdateStatus(id, request.NewStatus, request.ApprovedBy, request.Note);
            if (document == null)
                return NotFound();
            return Ok(document);
        }
        [Authorize(Policy = "Admin,Teacher")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDocument(int id)
        {
            var result = await _documentService.Delete(id);
            if (!result)
                return NotFound();

            return NoContent();
        }
    }
}
