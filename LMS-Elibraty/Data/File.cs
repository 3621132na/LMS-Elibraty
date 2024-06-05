using System;
using System.Collections.Generic;

namespace LMS_Elibraty.Data
{
    public partial class File
    {
        public int Id { get; set; }
        public string Category { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string UpdateBy { get; set; } = null!;
        public DateTime UpdateAt { get; set; }
        public int Size { get; set; }
        public int? LessonId { get; set; }

        public virtual User UpdateByNavigation { get; set; } = null!;
    }
}
