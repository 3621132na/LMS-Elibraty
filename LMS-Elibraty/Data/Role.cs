using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMS_Elibraty.Data
{
    public partial class Role
    {
        public Role()
        {
            Users = new HashSet<User>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public DateTime UpdateAt { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
