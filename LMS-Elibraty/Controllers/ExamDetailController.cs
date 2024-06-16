using LMS_Elibraty.Data;
using LMS_Elibraty.Services.ExamDetails;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LMS_Elibraty.Controllers
{
    [Authorize(Policy = "Admin,Teacher")]
    [Route("api/[controller]")]
    [ApiController]
    public class ExamDetailController : ControllerBase
    {
        private readonly IExamDetailService _examDetailService;

        public ExamDetailController(IExamDetailService examDetailService)
        {
            _examDetailService = examDetailService;
        }

        [HttpPost("{examId}")]
        public async Task<ActionResult<ExamDetail>> Create(string examId,ExamDetail examDetail)
        {
            var createdExamDetail = await _examDetailService.Create(examDetail, examId);
            return Ok(createdExamDetail);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ExamDetail>>> All()
        {
            var examDetails = await _examDetailService.All();
            return Ok(examDetails);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ExamDetail>> Details (int id)
        {
            var examDetail = await _examDetailService.Details(id);
            if (examDetail == null)
                return NotFound();
            return Ok(examDetail);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ExamDetail>> Update(int id,ExamDetail updatedExamDetail)
        {
            var result = await _examDetailService.Update(id, updatedExamDetail);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _examDetailService.Delete(id);
            if (!result)
                return NotFound();
            return NoContent();
        }
    }
}
