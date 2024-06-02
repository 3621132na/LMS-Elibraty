using LMS_Elibraty.Models;
using System.ComponentModel.DataAnnotations;

namespace LMS_Elibraty.Data
{
    public class User
    {
        [Key]
        public string Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public Gender Gender { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        [StringLength(10)]
        public string Phone { get; set; }
        [Required]
        public string Address { get; set; }
        public string? Avatar { get; set; }
        [Required]
        public Role Role { get; set; }
        [Required]
        public string Faculty { get; set; }
    }
}