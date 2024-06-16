using LMS_Elibraty.Data;
using Microsoft.EntityFrameworkCore;
namespace LMS_Elibraty.Services.System
{
    public class SystemService:ISystemService
    {
        private readonly LMSElibraryContext _context;

        public SystemService(LMSElibraryContext context)
        {
            _context = context;
        }

        public async Task<Systems> Create(Systems system)
        {
            _context.Systems.Add(system);
            await _context.SaveChangesAsync();
            return system;
        }

        public async Task<Systems?> Details(int id)
        {
            return await _context.Systems.FindAsync(id);
        }

        public async Task<IEnumerable<Systems>> All()
        {
            return await _context.Systems.ToListAsync();
        }

        public async Task<Systems?> Update(int id, Systems system)
        {
            var existingSystem = await _context.Systems.FindAsync(id);
            if (existingSystem == null)
                return null;
            existingSystem.SchoolName = system.SchoolName;
            existingSystem.Website = system.Website;
            existingSystem.SchoolType = system.SchoolType;
            existingSystem.Principal = system.Principal;
            existingSystem.SystemName = system.SystemName;
            existingSystem.Address = system.Address;
            existingSystem.Phone = system.Phone;
            existingSystem.Email = system.Email;
            await _context.SaveChangesAsync();
            return existingSystem;
        }

        public async Task<bool> Delete(int id)
        {
            var system = await _context.Systems.FindAsync(id);
            if (system == null)
                return false;
            _context.Systems.Remove(system);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
