using LMS_Elibraty.Data;

namespace LMS_Elibraty.Services.Feedbacks
{
    public interface IFeedbackService
    {
        Task<FeedBack> Send(FeedBack feedback, string userId);
    }
}
