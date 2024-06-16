using System;
using System.Collections.Generic;

namespace LMS_Elibraty.Data
{
    public partial class Faculty
    {
        public Faculty()
        {
            Classes = new HashSet<Class>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<Class> Classes { get; set; }
    }
}
