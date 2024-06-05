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

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int SubjectId { get; set; }
        public string Category { get; set; } = null!;
        public string Form { get; set; } = null!;
        public int Time { get; set; }
        public string Status { get; set; } = null!;
        public DateTime CreateAt { get; set; }

        public virtual Subject Subject { get; set; } = null!;
        public virtual ICollection<ExamDetail> ExamDetails { get; set; }
    }
}
