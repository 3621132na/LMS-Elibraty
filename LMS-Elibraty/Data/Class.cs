using System;
using System.Collections.Generic;

namespace LMS_Elibraty.Data
{
    public partial class Class
    {
        public Class()
        {
            Users = new HashSet<User>();
        }

        public string Id { get; set; } = null!;
        public string Name { get; set; } = null!;
        public int FacultyId { get; set; }

        public virtual Faculty Faculty { get; set; } = null!;
        public virtual ICollection<User> Users { get; set; }
    }
}
