using LMS_Elibraty.Data;

namespace LMS_Elibraty.Services.Facultys
{
    public interface IFacultyService
    {
        Task<Faculty> Create(Faculty faculty);
        Task<Faculty?> Details(int id);
        Task<IEnumerable<Faculty>> All();
        Task<Faculty?> Update(int id, Faculty faculty);
        Task<bool> Delete(int id);
    }
}
