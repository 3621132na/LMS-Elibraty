using System;
using System.Collections.Generic;

namespace LMS_Elibraty.Data
{
    public partial class SubjectClass
    {
        public string ClassId { get; set; } = null!;
        public string SubjectId { get; set; } = null!;

        public virtual Class Class { get; set; } = null!;
        public virtual Subject Subject { get; set; } = null!;
    }
}
