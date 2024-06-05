using System;
using System.Collections.Generic;

namespace LMS_Elibraty.Data
{
    public partial class FeedBack
    {
        public int Id { get; set; }
        public string Content { get; set; } = null!;
        public string UserId { get; set; } = null!;

        public virtual User User { get; set; } = null!;
    }
}
