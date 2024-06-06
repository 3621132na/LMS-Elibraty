using LMS_Elibraty.Data;

namespace LMS_Elibraty.DTOs
{
    public class CreateRoleViewModel
    {
        public Role Role { get; set; } = null!;
        public List<int> PermissionIds { get; set; } = new List<int>();
    }
}
