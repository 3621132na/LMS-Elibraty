using LMS_Elibraty.Data;

namespace LMS_Elibraty.Services.ExamDetails
{
    public interface IExamDetailService
    {
        Task<ExamDetail> Create(ExamDetail examDetail, string examId);
        Task<IEnumerable<ExamDetail>> All();
        Task<ExamDetail> Details(int id);
        Task<ExamDetail> Update(int id, ExamDetail updatedExamDetail);
        Task<bool> Delete(int id);
    }
}
