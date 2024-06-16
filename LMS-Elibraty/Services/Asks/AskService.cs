using LMS_Elibraty.Data;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace LMS_Elibraty.Services.Asks
{
    public class AskService:IAskService
    {
        private readonly LMSElibraryContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AskService(LMSElibraryContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<Ask> Create(Ask ask, string subjectId)
        {
            var userId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
                throw new InvalidOperationException("User not authenticated.");
            ask.SubjectId = subjectId;
            ask.UserId = userId;
            ask.Date = DateTime.Now;
            _context.Asks.Add(ask);
            await _context.SaveChangesAsync();
            return ask;
        }

        public async Task<IEnumerable<Ask>> All()
        {
            return await _context.Asks.ToListAsync();
        }

        public async Task<Ask> Details(int id)
        {
            return await _context.Asks.FindAsync(id);
        }
        public async Task<Ask?> Update(int id, Ask updatedAsk)
        {
            var ask = await _context.Asks.FindAsync(id);
            if (ask == null)
                return null;
            ask.Title = updatedAsk.Title;
            ask.Content = updatedAsk.Content;
            ask.Date = DateTime.Now;
            _context.Asks.Update(ask);
            await _context.SaveChangesAsync();
            return ask;
        }

        public async Task<bool> Delete(int id)
        {
            var ask = await _context.Asks.FindAsync(id);
            if (ask == null)
                return false;
            _context.Asks.Remove(ask);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
