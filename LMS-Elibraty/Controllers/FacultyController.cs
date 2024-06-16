using LMS_Elibraty.Data;
using LMS_Elibraty.Services.Facultys;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LMS_Elibraty.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class FacultyController : ControllerBase
    {
        private readonly IFacultyService _facultyService;

        public FacultyController(IFacultyService facultyService)
        {
            _facultyService = facultyService;
        }

        [HttpPost]
        public async Task<ActionResult<Faculty>> Create(Faculty faculty)
        {
            var createdFaculty = await _facultyService.Create(faculty);
            return Ok(createdFaculty);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Faculty>> Details(int id)
        {
            var faculty = await _facultyService.Details(id);
            if (faculty == null)
                return NotFound();
            return Ok(faculty);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Faculty>>> All()
        {
            var faculties = await _facultyService.All();
            return Ok(faculties);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Faculty>> Update(int id,Faculty faculty)
        {
            var updatedFaculty = await _facultyService.Update(id, faculty);
            if (updatedFaculty == null)
                return NotFound();
            return Ok(updatedFaculty);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _facultyService.Delete(id);
            if (!result)
                return NotFound();
            return NoContent();
        }
    }
}
