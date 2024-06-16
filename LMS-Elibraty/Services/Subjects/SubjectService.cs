using LMS_Elibraty.Data;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace LMS_Elibraty.Services.Subjects
{
    public class SubjectService:ISubjectService
    {
        private readonly LMSElibraryContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SubjectService(LMSElibraryContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }
        private int count = 0;
        public async Task<Subject> Create(Subject subject)
        {
            var userId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
                throw new InvalidOperationException("User not authenticated.");
            subject.Id = GenerateSubjectId();
            subject.UserId = userId;
            subject.SentDate = DateTime.Now;
            subject.Status = "Chờ phê duyệt";
            _context.Subjects.Add(subject);
            await _context.SaveChangesAsync();
            return subject;
        }

        public async Task<Subject?> Details(string id)
        {
            return await _context.Subjects.FindAsync(id);
        }
        public async Task<IEnumerable<Subject>> AllById(string userId)
        {
            return await _context.Subjects.Where(s => s.UserId == userId).ToListAsync();
        }
        public async Task<IEnumerable<Subject>> All()
        {
            return await _context.Subjects.ToListAsync();
        }

        public async Task<Subject?> Update(string id, Subject subject)
        {
            var existingSubject = await _context.Subjects.FindAsync(id);
            if (existingSubject == null)
                return null;
            var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var role = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Role);
            if (existingSubject.UserId != userId && role != "Admin")
                throw new UnauthorizedAccessException("You are not authorized to update this subject");
            existingSubject.Name = subject.Name;
            existingSubject.SentDate = DateTime.Now;
            existingSubject.Quantity = subject.Quantity;
            existingSubject.Description = subject.Description;
            await _context.SaveChangesAsync();
            return existingSubject;
        }
        public async Task<Subject?> UpdateStatus(string id, string newStatus)
        {
            var existingSubject = await _context.Subjects.FindAsync(id);
            if (existingSubject == null)
                return null;
            var role = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Role);
            if (role != "Admin")
                throw new UnauthorizedAccessException("You are not authorized to update this subject's status");
            existingSubject.Status = newStatus;
            await _context.SaveChangesAsync();
            return existingSubject;
        }
        public async Task<bool> AddToClasses(string subjectId, IEnumerable<string> classIds)
        {
            var subject = await _context.Subjects.FindAsync(subjectId);
            if (subject == null || subject.Status != "Đã phê duyệt")
                return false;
            foreach (var classId in classIds)
            {
                var existingSubjectClass = await _context.SubjectClasses.FindAsync(classId, subjectId);
                if (existingSubjectClass != null)
                    continue;
                var subjectClass = new SubjectClass
                {
                    ClassId = classId,
                    SubjectId = subjectId
                };
                _context.SubjectClasses.Add(subjectClass);
            }
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> Delete(string id)
        {
            var subject = await _context.Subjects.FindAsync(id);
            if (subject == null)
                return false;
            _context.Subjects.Remove(subject);
            await _context.SaveChangesAsync();
            return true;
        }
        private string GenerateSubjectId()
        {
            string prefix;
            int counter;
            prefix = "SLK";
            counter = ++count;
            return $"{prefix}{counter:D3}";
        }
    }
}
