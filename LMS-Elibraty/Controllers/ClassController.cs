using LMS_Elibraty.Data;
using LMS_Elibraty.Services.Classes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LMS_Elibraty.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassController : ControllerBase
    {
        private readonly IClassService _classService;

        public ClassController(IClassService classService)
        {
            _classService = classService;
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<Class>> Create(Class cls)
        {
            var createdClass = await _classService.Create(cls);
            return Ok(createdClass);
        }
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<Class>> Details(string id)
        {
            var cls = await _classService.Details(id);
            if (cls == null)
                return NotFound();
            return Ok(cls);
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Class>>> All()
        {
            var classes = await _classService.All();
            return Ok(classes);
        }
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<ActionResult<Class>> Update(string id,Class cls)
        {
            var updatedClass = await _classService.Update(id, cls);
            if (updatedClass == null)
                return NotFound();
            return Ok(updatedClass);
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await _classService.Delete(id);
            if (!result)
                return NotFound();
            return NoContent();
        }
    }
}
