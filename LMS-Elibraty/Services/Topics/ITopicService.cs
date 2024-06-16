using LMS_Elibraty.Data;

namespace LMS_Elibraty.Services.Topics
{
    public interface ITopicService
    {
        Task<Topic> Create(Topic topic, string subjectId);
        Task<IEnumerable<Topic>> All();
        Task<Topic> Details(int id);
        Task<Topic?> Update(int id, Topic updatedTopic);
        Task<bool> Delete(int id);
    }
}
