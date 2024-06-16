using LMS_Elibraty.Data;
using Microsoft.EntityFrameworkCore;

namespace LMS_Elibraty.Services.Lessons
{
    public class LessonService:ILessonService
    {
        private readonly LMSElibraryContext _context;

        public LessonService(LMSElibraryContext context)
        {
            _context = context;
        }

        public async Task<Lesson> Create(Lesson lesson,int topicId)
        {
            lesson.TopicId = topicId;
            _context.Lessons.Add(lesson);
            await _context.SaveChangesAsync();
            return lesson;
        }

        public async Task<IEnumerable<Lesson>> All()
        {
            return await _context.Lessons.ToListAsync();
        }

        public async Task<Lesson> Details(int id)
        {
            return await _context.Lessons.FindAsync(id);
        }
        public async Task<Lesson?> Update(int id, Lesson updatedLesson)
        {
            var lesson = await _context.Lessons.FindAsync(id);
            if (lesson == null)
                return null;
            lesson.Title = updatedLesson.Title;
            _context.Lessons.Update(lesson);
            await _context.SaveChangesAsync();
            return lesson;
        }

        public async Task<bool> Delete(int id)
        {
            var lesson = await _context.Lessons.FindAsync(id);
            if (lesson == null)
                return false;
            _context.Lessons.Remove(lesson);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
