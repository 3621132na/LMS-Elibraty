using LMS_Elibraty.Data;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace LMS_Elibraty.Services.Document
{
        public class DocumentService:IDocumentService
        {
            private readonly LMSElibraryContext _context;
            private readonly IHttpContextAccessor _httpContextAccessor;

            public DocumentService(LMSElibraryContext context, IHttpContextAccessor httpContextAccessor)
            {
                _context = context;
                _httpContextAccessor = httpContextAccessor;
            }

            public async Task<Documents> Create(Documents document)
            {
                var userId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(userId))
                    throw new InvalidOperationException("User not authenticated.");
                document.CreateBy=userId;
                document.SentDate = DateTime.Now;
                document.Status = "Chờ phê duyệt";
                _context.Documents.Add(document);
                await _context.SaveChangesAsync();
                return document;
            }

            public async Task<IEnumerable<Documents>> All()
            {
                return await _context.Documents.ToListAsync();
            }

            public async Task<Documents> Details(int id)
            {
                return await _context.Documents.FindAsync(id);
            }
        public async Task<Documents?> Update(int id, Documents updatedDocument)
        {
            var document = await _context.Documents.FindAsync(id);
            if (document == null)
                return null;
            document.Name = updatedDocument.Name;
            document.Classify = updatedDocument.Classify;
            _context.Documents.Update(document);
            await _context.SaveChangesAsync();
            return document;
        }
        public async Task<Documents?> UpdateStatus(int id, string newStatus,string approvedBy, string note)
        {
            var document = await _context.Documents.FindAsync(id);
            if (document == null)
                return null;
            var userId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
                throw new InvalidOperationException("User not authenticated.");
            var role = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Role);
            if (role != "Admin")
                throw new UnauthorizedAccessException("You are not authorized to update this subject's status");
            document.Status = newStatus;
            document.ApprovedBy = userId;
            document.Note = note;
            _context.Documents.Update(document);
            await _context.SaveChangesAsync();
            return document;
        }
        public async Task<bool> Delete(int id)
        {
            var document = await _context.Documents.FindAsync(id);
            if (document == null)
                return false;
            _context.Documents.Remove(document);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
