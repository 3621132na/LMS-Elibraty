using LMS_Elibraty.Data;
using LMS_Elibraty.DTOs;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace LMS_Elibraty.Services.Roles
{
    public class RoleService : IRoleService
    {
        private readonly LMSElibraryContext _context;
        public RoleService(LMSElibraryContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Role>> All()
        {
            return await _context.Roles.ToListAsync();
        }

        public async Task<Role> Details(int id)
        {
            return await _context.Roles.FindAsync(id);
        }

        public async Task<Role> Create(Role role, List<int> permissionIds)
        {
            role.UpdateAt = DateTime.Now;
            _context.Roles.Add(role);
            await _context.SaveChangesAsync();
            foreach (var permissionId in permissionIds)
            {
                var rolePermission = new RolePermission
                {
                    RoleId = role.Id,
                    PermissionId = permissionId
                };
                _context.RolePermissions.Add(rolePermission);
            }
            await _context.SaveChangesAsync();
            return role;
        }

        public async Task<Role> Update(Role role, List<int> permissionIds)
        {
            role.UpdateAt = DateTime.Now;
            _context.Entry(role).State = EntityState.Modified;
            var existingRolePermissions = _context.RolePermissions.Where(rp => rp.RoleId == role.Id);
            _context.RolePermissions.RemoveRange(existingRolePermissions);
            await _context.SaveChangesAsync();
            foreach (var permissionId in permissionIds)
            {
                var rolePermission = new RolePermission
                {
                    RoleId = role.Id,
                    PermissionId = permissionId
                };
                _context.RolePermissions.Add(rolePermission);
            }
            _context.Entry(role).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return role;
        }

        public async Task Delete(int id)
        {
            var role = await _context.Roles.FindAsync(id);
            if (role != null)
            {
                _context.Roles.Remove(role);
                await _context.SaveChangesAsync();
            }
        }
    }
}
