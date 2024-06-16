using LMS_Elibraty.Data;

namespace LMS_Elibraty.Services.Classes
{
    public interface IClassService
    {
        Task<Class> Create(Class cls);
        Task<Class?> Details(string id);
        Task<IEnumerable<Class>> All();
        Task<Class?> Update(string id, Class cls);
        Task<bool> Delete(string id);
    }
}
