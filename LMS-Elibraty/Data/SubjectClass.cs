using System;
using System.Collections.Generic;

namespace LMS_Elibraty.Data
{
    public partial class SubjectClass
    {
        public int ClassId { get; set; }
        public int SubjectId { get; set; }

        public virtual Class Class { get; set; } = null!;
        public virtual Subject Subject { get; set; } = null!;
    }
}
