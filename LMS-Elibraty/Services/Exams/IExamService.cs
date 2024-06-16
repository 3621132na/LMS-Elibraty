using LMS_Elibraty.Data;

namespace LMS_Elibraty.Services.Exams
{
    public interface IExamService
    {
        Task<Exam> Create(Exam exam, string subjectId);
        Task<IEnumerable<Exam>> All();
        Task<Exam> Details(string id);
        Task<Exam?> Update(string id, Exam updatedExam);
        Task<Exam?> UpdateStatus(string id, string newStatus);
        Task<bool> Delete(string id);
    }
}
