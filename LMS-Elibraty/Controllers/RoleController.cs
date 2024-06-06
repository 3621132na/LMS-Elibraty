using LMS_Elibraty.Data;
using LMS_Elibraty.DTOs;
using LMS_Elibraty.Services.Roles;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LMS_Elibraty.Controllers
{
    //[Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;
        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Role>>> All()
        {
            var roles = await _roleService.All();
            return Ok(roles);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Role>> Details(int id)
        {
            var role = await _roleService.Details(id);
            if (role == null)
            {
                return NotFound();
            }
            return Ok(role);
        }

        [HttpPost]
        public async Task<ActionResult<Role>> Create(CreateRoleViewModel createRoleViewModel)
        {
            try
            {
                var newRole = await _roleService.Create(createRoleViewModel.Role, createRoleViewModel.PermissionIds);
                return Ok(newRole);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, CreateRoleViewModel updateRoleViewModel)
        {
            if (id != updateRoleViewModel.Role.Id)
                return BadRequest();
            try
            {
                await _roleService.Update(updateRoleViewModel.Role,updateRoleViewModel.PermissionIds);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (await _roleService.Details(id) == null)
                    return NotFound();
                else
                    throw;
            }
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _roleService.Delete(id);
            return NoContent();
        }
    }
}
