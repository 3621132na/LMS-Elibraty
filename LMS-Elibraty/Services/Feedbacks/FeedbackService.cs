using LMS_Elibraty.Data;
using LMS_Elibraty.Services.Emails;

namespace LMS_Elibraty.Services.Feedbacks
{
    public class FeedbackService:IFeedbackService
    {
        private readonly LMSElibraryContext _context;
        private readonly IConfiguration _configuration;
        private readonly IEmailService _emailService;

        public FeedbackService(LMSElibraryContext context,IConfiguration configuration,IEmailService emailService)
        {
            _context = context;
            _configuration = configuration;
            _emailService = emailService;
        }
        public async Task<FeedBack> Send(FeedBack feedback, string userId)
    {
        feedback.UserId = userId;
        _context.FeedBacks.Add(feedback);
        await _context.SaveChangesAsync();
        _emailService.SendEmail(_configuration["Smtp:FromEmail"], "Feed back", $"{feedback.Content}");
        return feedback;
    }
    }
}
