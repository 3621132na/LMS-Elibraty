using LMS_Elibraty.Data;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace LMS_Elibraty.Services.Exams
{
    public class ExamService:IExamService
    {
        private readonly LMSElibraryContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public ExamService(LMSElibraryContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<Exam> Create(Exam exam, string subjectId)
        {
            exam.SubjectId = subjectId;
            exam.Status = "Chờ phê duyệt";
            exam.CreateAt = DateTime.Now;
            _context.Exams.Add(exam);
            await _context.SaveChangesAsync();
            return exam;
        }

        public async Task<IEnumerable<Exam>> All()
        {
            return await _context.Exams.ToListAsync();
        }

        public async Task<Exam> Details(string id)
        {
            return await _context.Exams.FindAsync(id);
        }

        public async Task<Exam?> Update(string id, Exam updatedExam)
        {
            var exam = await _context.Exams.FindAsync(id);
            if (exam == null)
                return null;
            exam.Name = updatedExam.Name;
            exam.Category = updatedExam.Category;
            exam.Form = updatedExam.Form;
            exam.Time = updatedExam.Time;
            exam.Level = updatedExam.Level;
            await _context.SaveChangesAsync();
            return exam;
        }
        public async Task<Exam?> UpdateStatus(string id, string newStatus)
        {
            var existingExam = await _context.Exams.FindAsync(id);
            if (existingExam == null)
                return null;
            var role = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Role);
            if (role != "Admin")
                throw new UnauthorizedAccessException("You are not authorized to update this subject's status");
            existingExam.Status = newStatus;
            await _context.SaveChangesAsync();
            return existingExam;
        }

        public async Task<bool> Delete(string id)
        {
            var exam = await _context.Exams.FindAsync(id);
            if (exam == null)
                return false;
            _context.Exams.Remove(exam);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
