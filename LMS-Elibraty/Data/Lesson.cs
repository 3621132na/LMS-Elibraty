using System;
using System.Collections.Generic;

namespace LMS_Elibraty.Data
{
    public partial class Lesson
    {
        public int Id { get; set; }
        public int TopicId { get; set; }
        public string Title { get; set; } = null!;

        public virtual Topic Topic { get; set; } = null!;
    }
}
