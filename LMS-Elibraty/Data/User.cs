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
            DocumentApprovedByNavigations = new HashSet<Document>();
            DocumentCreateByNavigations = new HashSet<Document>();
            FeedBacks = new HashSet<FeedBack>();
            Files = new HashSet<File>();
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
        public string? Faculty { get; set; }
        public int? ClassId { get; set; }

        public virtual Class? Class { get; set; }
        public virtual Role Role { get; set; } = null!;
        public virtual ICollection<Answer> Answers { get; set; }
        public virtual ICollection<Ask> Asks { get; set; }
        public virtual ICollection<Document> DocumentApprovedByNavigations { get; set; }
        public virtual ICollection<Document> DocumentCreateByNavigations { get; set; }
        public virtual ICollection<FeedBack> FeedBacks { get; set; }
        public virtual ICollection<File> Files { get; set; }
        public virtual ICollection<Subject> Subjects { get; set; }
    }
}
