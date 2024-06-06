using LMS_Elibraty.Data;

namespace LMS_Elibraty.Services.Roles
{
    public interface IRoleService
    {
        Task<IEnumerable<Role>> All();
        Task<Role> Details(int id);
        Task<Role> Create(Role role, List<int> permissionIds);
        Task<Role> Update(Role role, List<int> permissionIds);
        Task Delete(int id);
    }
}
