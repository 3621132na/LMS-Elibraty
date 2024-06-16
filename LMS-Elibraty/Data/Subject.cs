using System;
using System.Collections.Generic;

namespace LMS_Elibraty.Data
{
    public partial class Subject
    {
        public Subject()
        {
            Asks = new HashSet<Ask>();
            Exams = new HashSet<Exam>();
            Topics = new HashSet<Topic>();
        }

        public string Id { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string UserId { get; set; } = null!;
        public DateTime SentDate { get; set; }
        public string Status { get; set; } = null!;
        public int Quantity { get; set; }
        public string Description { get; set; } = null!;

        public virtual User User { get; set; } = null!;
        public virtual ICollection<Ask> Asks { get; set; }
        public virtual ICollection<Exam> Exams { get; set; }
        public virtual ICollection<Topic> Topics { get; set; }
    }
}
