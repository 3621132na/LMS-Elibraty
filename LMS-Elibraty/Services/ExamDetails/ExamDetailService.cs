using LMS_Elibraty.Data;
using Microsoft.EntityFrameworkCore;

namespace LMS_Elibraty.Services.ExamDetails
{
    public class ExamDetailService:IExamDetailService
    {
        private readonly LMSElibraryContext _context;

        public ExamDetailService(LMSElibraryContext context)
        {
            _context = context;
        }

        public async Task<ExamDetail> Create(ExamDetail examDetail, string examId)
        {
            examDetail.ExamId = examId;
            _context.ExamDetails.Add(examDetail);
            await _context.SaveChangesAsync();
            return examDetail;
        }

        public async Task<IEnumerable<ExamDetail>> All()
        {
            return await _context.ExamDetails.ToListAsync();
        }

        public async Task<ExamDetail> Details(int id)
        {
            return await _context.ExamDetails.FindAsync(id);
        }

        public async Task<ExamDetail> Update(int id, ExamDetail updatedExamDetail)
        {
            var examDetail = await _context.ExamDetails.FindAsync(id);
            if (examDetail == null)
                throw new KeyNotFoundException("ExamDetail not found");
            examDetail.Question = updatedExamDetail.Question;
            examDetail.Answer = updatedExamDetail.Answer;
            examDetail.Select = updatedExamDetail.Select;
            await _context.SaveChangesAsync();
            return examDetail;
        }

        public async Task<bool> Delete(int id)
        {
            var examDetail = await _context.ExamDetails.FindAsync(id);
            if (examDetail == null)
                return false;
            _context.ExamDetails.Remove(examDetail);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
