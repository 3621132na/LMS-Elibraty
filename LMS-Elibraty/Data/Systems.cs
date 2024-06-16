using System;
using System.Collections.Generic;

namespace LMS_Elibraty.Data
{
    public partial class Systems
    {
        public int Id { get; set; }
        public string SchoolName { get; set; } = null!;
        public string Website { get; set; } = null!;
        public string SchoolType { get; set; } = null!;
        public string Principal { get; set; } = null!;
        public string SystemName { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string Email { get; set; } = null!;
    }
}
