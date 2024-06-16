using LMS_Elibraty.Data;

namespace LMS_Elibraty.Services.Subjects
{
    public interface ISubjectService
    {
        Task<Subject> Create(Subject subject);
        Task<Subject?> Details(string id);
        Task<IEnumerable<Subject>> AllById(string userId);
        Task<IEnumerable<Subject>> All();
        Task<Subject?> Update(string id, Subject subject);
        Task<Subject?> UpdateStatus(string id, string newStatus);
        Task<bool> AddToClasses(string subjectId, IEnumerable<string> classIds);
        Task<bool> Delete(string id);
    }
}
