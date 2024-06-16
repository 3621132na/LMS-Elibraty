using LMS_Elibraty.Data;
using Microsoft.EntityFrameworkCore;

namespace LMS_Elibraty.Services.Topics
{
    public class TopicService:ITopicService
    {
        private readonly LMSElibraryContext _context;

        public TopicService(LMSElibraryContext context)
        {
            _context = context;
        }

        public async Task<Topic> Create(Topic topic,string subjectId)
        {
            topic.SubjectId = subjectId;
            _context.Topics.Add(topic);
            await _context.SaveChangesAsync();
            return topic;
        }

        public async Task<IEnumerable<Topic>> All()
        {
            return await _context.Topics.ToListAsync();
        }

        public async Task<Topic> Details(int id)
        {
            return await _context.Topics.FindAsync(id);
        }
        public async Task<Topic?> Update(int id, Topic updatedTopic)
        {
            var topic = await _context.Topics.FindAsync(id);
            if (topic == null)
                return null;
            topic.Topic1 = updatedTopic.Topic1;
            _context.Topics.Update(topic);
            await _context.SaveChangesAsync();
            return topic;
        }

        public async Task<bool> Delete(int id)
        {
            var topic = await _context.Topics.FindAsync(id);
            if (topic == null)
                return false;
            _context.Topics.Remove(topic);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
