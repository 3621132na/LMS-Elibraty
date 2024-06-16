using LMS_Elibraty.Data;

namespace LMS_Elibraty.Services.File
{
    public interface IFileService
    {
        Task<Files> Create(IFormFile file, int lessonId);
        Task<IEnumerable<Files>> All();
        Task<Files> Details(int id);
        Task<Files?> Update(int id, Files updatedFile);
        Task<bool> Delete(int id);
    }
}
