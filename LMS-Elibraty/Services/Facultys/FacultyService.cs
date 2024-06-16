using LMS_Elibraty.Data;
using Microsoft.EntityFrameworkCore;

namespace LMS_Elibraty.Services.Facultys
{
    public class FacultyService:IFacultyService
    {
        private readonly LMSElibraryContext _context;

        public FacultyService(LMSElibraryContext context)
        {
            _context = context;
        }

        public async Task<Faculty> Create(Faculty faculty)
        {
            _context.Faculties.Add(faculty);
            await _context.SaveChangesAsync();
            return faculty;
        }

        public async Task<Faculty?> Details(int id)
        {
            return await _context.Faculties.FindAsync(id);
        }

        public async Task<IEnumerable<Faculty>> All()
        {
            return await _context.Faculties.ToListAsync();
        }

        public async Task<Faculty?> Update(int id, Faculty faculty)
        {
            var existingFaculty = await _context.Faculties.FindAsync(id);
            if (existingFaculty == null)
                return null;
            existingFaculty.Name = faculty.Name;
            await _context.SaveChangesAsync();
            return existingFaculty;
        }

        public async Task<bool> Delete(int id)
        {
            var faculty = await _context.Faculties.FindAsync(id);
            if (faculty == null)
                return false;
            _context.Faculties.Remove(faculty);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
