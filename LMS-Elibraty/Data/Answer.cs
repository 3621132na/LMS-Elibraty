using System;
using System.Collections.Generic;

namespace LMS_Elibraty.Data
{
    public partial class Answer
    {
        public int Id { get; set; }
        public int AskId { get; set; }
        public string Content { get; set; } = null!;
        public string UserId { get; set; } = null!;

        public virtual Ask Ask { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}
