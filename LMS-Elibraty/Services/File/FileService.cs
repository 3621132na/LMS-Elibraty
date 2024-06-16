using LMS_Elibraty.Data;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Security.Claims;

namespace LMS_Elibraty.Services.File
{
    public class FileService:IFileService
    {
        private readonly LMSElibraryContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public FileService(LMSElibraryContext context, IHttpContextAccessor httpContextAccessor,IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<Files> Create(IFormFile file, int lessonId)
        {
            var userId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
                throw new InvalidOperationException("User not authenticated.");
            var fileName = Path.GetFileName(file.FileName);
            var fileCategory = GetFileCategory(fileName);
            var fileSizeInKb = (int)(file.Length / 1024);
            string directory = "wwwroot/files";
            var filePath = Path.Combine(directory, fileName);
            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            var newFile = new Files
            {
                Category = fileCategory,
                Name = fileName,
                UpdateBy = userId,
                UpdateAt = DateTime.Now,
                Size = fileSizeInKb,
                LessonId = lessonId,
                Url = filePath
            };
            _context.Files.Add(newFile);
            await _context.SaveChangesAsync();
            return newFile;
        }
        private string GetFileCategory(string fileName)
        {
            var extension = Path.GetExtension(fileName).ToLower();
            return extension switch
            {
                ".pptx" => "powerpoint",
                ".docx" => "word",
                ".xlsx" => "excel",
                _ => "other"
            };
        }

        public async Task<IEnumerable<Files>> All()
        {
            return await _context.Files.ToListAsync();
        }

        public async Task<Files> Details(int id)
        {
            return await _context.Files.FindAsync(id);
        }
        public async Task<Files?> Update(int id, Files updatedFile)
        {
            var file = await _context.Files.FindAsync(id);
            if (file == null)
                return null;

            var userId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
                throw new InvalidOperationException("User not authenticated.");
            file.Name = updatedFile.Name;
            file.UpdateBy = userId;
            file.UpdateAt = DateTime.Now;
            file.LessonId = updatedFile.LessonId;
            _context.Files.Update(file);
            await _context.SaveChangesAsync();
            return file;
        }
        public async Task<bool> Delete(int id)
        {
            var file = await _context.Files.FindAsync(id);
            if (file == null)
                return false;
            _context.Files.Remove(file);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
