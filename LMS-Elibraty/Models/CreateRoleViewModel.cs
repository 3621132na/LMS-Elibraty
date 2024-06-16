using LMS_Elibraty.Data;

namespace LMS_Elibraty.Models
{
    public class CreateRoleViewModel
    {
        public Role Role { get; set; } = null!;
        public List<int> PermissionIds { get; set; } = new List<int>();
    }
}
