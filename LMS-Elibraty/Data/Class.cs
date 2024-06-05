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

        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<User> Users { get; set; }
    }
}
