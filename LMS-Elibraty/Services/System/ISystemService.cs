using LMS_Elibraty.Data;

namespace LMS_Elibraty.Services.System
{
    public interface ISystemService
    {
        Task<Systems> Create(Systems system);
        Task<Systems?> Details(int id);
        Task<IEnumerable<Systems>> All();
        Task<Systems?> Update(int id, Systems system);
        Task<bool> Delete(int id);
    }
}
