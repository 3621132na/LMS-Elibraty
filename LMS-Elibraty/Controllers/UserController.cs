using LMS_Elibraty.Data;
using LMS_Elibraty.DTOs;
using LMS_Elibraty.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LMS_Elibraty.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UserController(IUserService userService, IHttpContextAccessor httpContextAccessor)
        {
            _userService = userService;
            _httpContextAccessor = httpContextAccessor;
        }
        [HttpPost("register")]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> Register(User user)
        {
            try
            {
                var newUser = await _userService.Register(user);
                return Ok(newUser);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginModel model)
        {
            try
            {
                var token = await _userService.Login(model);
                return Ok(token);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("logout")]
        [Authorize]
        public IActionResult Logout()
        {
            Response.Cookies.Delete("AuthenticationCookieName");
            return Ok(new { message = "Logout successful." });
        }
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Detail(string id)
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var userRole = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Role)?.Value;
            if (string.IsNullOrEmpty(userId))
                return Unauthorized();
            if (userRole == "Admin" || userId == id)
            {
                var user = await _userService.Detail(id);
                if (user == null)
                    return NotFound();
                return Ok(user);
            }
            else
                return Forbid();
        }
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> Update(string id, UserViewModel user)
        {
            try
            {
                var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var userRole = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Role)?.Value;
                if (string.IsNullOrEmpty(userId))
                    return Unauthorized();
                if (userRole == "Admin" || userId == id)
                {
                    var updatedUser = await _userService.Update(id,user);
                    return Ok(updatedUser);
                }
                else
                    return Forbid();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("changeavatar/{id}")]
        [Authorize]
        public async Task<IActionResult> ChangeAvatar(string id, IFormFile model)
        {
            try
            {
                var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var userRole = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Role)?.Value;
                if (string.IsNullOrEmpty(userId))
                    return Unauthorized();
                if (userRole == "Admin" || userId == id)
                {
                    var updatedUser = await _userService.ChangeAvatar(id, model);
                    return Ok(updatedUser);
                }
                else
                    return Forbid();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                var result = await _userService.Delete(id);
                if (result)
                    return Ok();
                return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> All()
        {
            var users = await _userService.All();
            return Ok(users);
        }
        [HttpPost("changepassword/{id}")]
        [Authorize]
        public async Task<IActionResult> ChangePassword(string id, ChangePasswordModel model)
        {
            try
            {
                var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(userId))
                    return Unauthorized();
                var result = await _userService.ChangePassword(id, model);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("forgotpassword")]
        public async Task<IActionResult> ForgotPassword(string email)
        {
            try
            {
                var result = await _userService.ForgotPassword(email);
                if (result)
                    return Ok();
                return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}