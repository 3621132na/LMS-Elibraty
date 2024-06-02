using LMS_Elibraty.Models;
using Microsoft.EntityFrameworkCore;

namespace LMS_Elibraty.Data
{
    public class LMSElibraryDbContext: DbContext
    {
        public LMSElibraryDbContext(DbContextOptions<LMSElibraryDbContext> options): base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().Property(u => u.Gender)
                .HasConversion(
                    v => v.ToString(),
                    v => (Gender)Enum.Parse(typeof(Gender), v));
            modelBuilder.Entity<User>().Property(u => u.Role)
                .HasConversion(
                    v => v.ToString(),
                    v => (Role)Enum.Parse(typeof(Role), v));
        }
    }
}