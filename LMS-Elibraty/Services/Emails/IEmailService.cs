namespace LMS_Elibraty.Services.Emails
{
    public interface IEmailService
    {
        void SendEmail(string toEmail, string subject, string body);
    }
}
