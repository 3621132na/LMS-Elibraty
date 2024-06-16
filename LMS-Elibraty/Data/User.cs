using System;
using System.Collections.Generic;

namespace LMS_Elibraty.Data
{
    public partial class User
    {
        public User()
        {
            Answers = new HashSet<Answer>();
            Asks = new HashSet<Ask>();
            DocumentApprovedByNavigations = new HashSet<Documents>();
            DocumentCreateByNavigations = new HashSet<Documents>();
            FeedBacks = new HashSet<FeedBack>();
            Files = new HashSet<Files>();
            Subjects = new HashSet<Subject>();
        }

        public string Id { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Gender { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string? Avatar { get; set; }
        public int RoleId { get; set; }
        public string? ClassId { get; set; }

        public virtual Class? Class { get; set; }
        public virtual Role? Role { get; set; }
        public virtual ICollection<Answer> Answers { get; set; }
        public virtual ICollection<Ask> Asks { get; set; }
        public virtual ICollection<Documents> DocumentApprovedByNavigations { get; set; }
        public virtual ICollection<Documents> DocumentCreateByNavigations { get; set; }
        public virtual ICollection<FeedBack> FeedBacks { get; set; }
        public virtual ICollection<Files> Files { get; set; }
        public virtual ICollection<Subject> Subjects { get; set; }
    }
}
