using System;
using System.Collections.Generic;

namespace LMS_Elibraty.Data
{
    public partial class Document
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Classify { get; set; } = null!;
        public string CreateBy { get; set; } = null!;
        public DateTime SentDate { get; set; }
        public string Status { get; set; } = null!;
        public string? ApprovedBy { get; set; }
        public string? Note { get; set; }

        public virtual User? ApprovedByNavigation { get; set; }
        public virtual User CreateByNavigation { get; set; } = null!;
    }
}
