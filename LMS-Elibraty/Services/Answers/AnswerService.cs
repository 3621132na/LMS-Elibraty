using LMS_Elibraty.Data;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace LMS_Elibraty.Services.Answers
{
    public class AnswerService:IAnswerService
    {
        private readonly LMSElibraryContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public AnswerService(LMSElibraryContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<Answer> Create(Answer answer,int askId)
        {
            var userId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
                throw new InvalidOperationException("User not authenticated.");
            answer.AskId = askId;
            answer.UserId = userId;
            _context.Answers.Add(answer);
            await _context.SaveChangesAsync();
            return answer;
        }

        public async Task<IEnumerable<Answer>> All()
        {
            return await _context.Answers.ToListAsync();
        }

        public async Task<Answer> Details(int id)
        {
            return await _context.Answers.FindAsync(id);
        }
        public async Task<Answer?> Update(int id, Answer updatedAnswer)
        {
            var answer = await _context.Answers.FindAsync(id);
            if (answer == null)
                return null;
            answer.Content = updatedAnswer.Content;
            _context.Answers.Update(answer);
            await _context.SaveChangesAsync();
            return answer;
        }

        public async Task<bool> Delete(int id)
        {
            var answer = await _context.Answers.FindAsync(id);
            if (answer == null)
                return false;
            _context.Answers.Remove(answer);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
