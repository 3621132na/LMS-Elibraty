using LMS_Elibraty.Data;
using LMS_Elibraty.Services.Feedbacks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LMS_Elibraty.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class FeedbackController : ControllerBase
    {
        private readonly IFeedbackService _feedbackService;

        public FeedbackController(IFeedbackService feedbackService)
        {
            _feedbackService = feedbackService;
        }
        [HttpPost]
        public async Task<ActionResult<FeedBack>> Send(FeedBack feedback)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized("User is not logged in");
            }
            var createdFeedback = await _feedbackService.Send(feedback,userId);
            return Ok(createdFeedback);
        }
    }
}
