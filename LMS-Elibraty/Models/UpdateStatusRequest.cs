namespace LMS_Elibraty.Models
{
    public class UpdateStatusRequest
    {
        public string NewStatus { get; set; } = null!;
        public string ApprovedBy { get; set; } = null!;
        public string Note { get; set; } = null!;
    }
}
