using LMS_Elibraty.Data;

namespace LMS_Elibraty.Services.Answers
{
    public interface IAnswerService
    {
        Task<Answer> Create(Answer answer, int askId);
        Task<IEnumerable<Answer>> All();
        Task<Answer> Details(int id);
        Task<Answer?> Update(int id, Answer updatedAnswer);
        Task<bool> Delete(int id);
    }
}
