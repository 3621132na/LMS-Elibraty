﻿using System;
using System.Collections.Generic;

namespace LMS_Elibraty.Data
{
    public partial class ExamDetail
    {
        public int Id { get; set; }
        public string ExamId { get; set; } = null!;
        public string Question { get; set; } = null!;
        public string? Answer { get; set; }
        public string? Select { get; set; }

        public virtual Exam Exam { get; set; } = null!;
    }
}
