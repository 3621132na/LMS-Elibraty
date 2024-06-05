using System;
using System.Collections.Generic;

namespace LMS_Elibraty.Data
{
    public partial class Topic
    {
        public Topic()
        {
            Lessons = new HashSet<Lesson>();
        }

        public int Id { get; set; }
        public int SubjectId { get; set; }
        public string Topic1 { get; set; } = null!;

        public virtual Subject Subject { get; set; } = null!;
        public virtual ICollection<Lesson> Lessons { get; set; }
    }
}
