using System;
using System.Collections.Generic;

namespace LMS_Elibraty.Data
{
    public partial class Exam
    {
        public Exam()
        {
            ExamDetails = new HashSet<ExamDetail>();
        }

        public string Id { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string SubjectId { get; set; } = null!;
        public string Category { get; set; } = null!;
        public string Form { get; set; } = null!;
        public int Time { get; set; }
        public string Status { get; set; } = null!;
        public DateTime CreateAt { get; set; }
        public string Level { get; set; } = null!;

        public virtual Subject Subject { get; set; } = null!;
        public virtual ICollection<ExamDetail> ExamDetails { get; set; }
    }
}
