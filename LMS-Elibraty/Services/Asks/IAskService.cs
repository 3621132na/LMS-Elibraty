using LMS_Elibraty.Data;

namespace LMS_Elibraty.Services.Asks
{
    public interface IAskService
    {
        Task<Ask> Create(Ask ask, string subjectId);
        Task<IEnumerable<Ask>> All();
        Task<Ask> Details(int id);
        Task<Ask?> Update(int id, Ask updatedAsk);
        Task<bool> Delete(int id);
    }
}
