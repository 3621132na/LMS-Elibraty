using LMS_Elibraty.Data;
using LMS_Elibraty.Services.System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LMS_Elibraty.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class SystemController : ControllerBase
    {
        private readonly ISystemService _systemService;

        public SystemController(ISystemService systemService)
        {
            _systemService = systemService;
        }

        [HttpPost]
        public async Task<ActionResult<Systems>> Create(Systems system)
        {
            var createdSystem = await _systemService.Create(system);
            return Ok(createdSystem);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Systems>> Details(int id)
        {
            var system = await _systemService.Details(id);
            if (system == null)
                return NotFound();
            return Ok(system);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Systems>>> All()
        {
            var systems = await _systemService.All();
            return Ok(systems);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Systems>> Update(int id,Systems system)
        {
            var updatedSystem = await _systemService.Update(id, system);
            if (updatedSystem == null)
                return NotFound();
            return Ok(updatedSystem);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _systemService.Delete(id);
            if (!result)
                return NotFound();
            return NoContent();
        }
    }
}
