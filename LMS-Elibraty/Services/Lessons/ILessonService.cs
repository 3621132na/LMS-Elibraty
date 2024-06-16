using LMS_Elibraty.Data;

namespace LMS_Elibraty.Services.Lessons
{
    public interface ILessonService
    {
        Task<Lesson> Create(Lesson lesson, int topicId);
        Task<IEnumerable<Lesson>> All();
        Task<Lesson> Details(int id);
        Task<Lesson?> Update(int id, Lesson updatedLesson);
        Task<bool> Delete(int id);
    }
}
