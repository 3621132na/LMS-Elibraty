using System;
using System.Collections.Generic;

namespace LMS_Elibraty.Data
{
    public partial class Ask
    {
        public Ask()
        {
            Answers = new HashSet<Answer>();
        }

        public int Id { get; set; }
        public int SubjectId { get; set; }
        public string UserId { get; set; } = null!;
        public DateTime Date { get; set; }
        public string Title { get; set; } = null!;
        public string Content { get; set; } = null!;

        public virtual Subject Subject { get; set; } = null!;
        public virtual User User { get; set; } = null!;
        public virtual ICollection<Answer> Answers { get; set; }
    }
}
