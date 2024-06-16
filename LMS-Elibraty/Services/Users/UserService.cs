using LMS_Elibraty.Data;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.ComponentModel.DataAnnotations;
using LMS_Elibraty.Models;
using LMS_Elibraty.Services.Emails;
using System.Data;

namespace LMS_Elibraty.Services.Users
{
    public class UserService : IUserService 
    {
        private readonly LMSElibraryContext _context;
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IEmailService _emailService;
        public UserService(LMSElibraryContext context, IConfiguration configuration, IWebHostEnvironment webHostEnvironment,IEmailService emailService)
        {
            _context = context;
            _configuration = configuration;
            _webHostEnvironment = webHostEnvironment;
            _emailService = emailService;
        }
        private int adminCounter = 0;
        private int studentCounter = 0;
        public async Task<User> Register(User user)
        {
            if (await _context.Users.AnyAsync(u => u.Email == user.Email))
                throw new Exception("Email already exists");
            ValidatePhoneNumber(user.Phone);
            user.Id = GenerateUserId(user.RoleId);
            ValidatePassword(user.Password);
            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<string> Login(LoginViewModel model)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == model.Email);
            if (user == null || !BCrypt.Net.BCrypt.Verify(model.Password, user.Password))
                throw new Exception("Invalid email or password");
            var token = GenerateJwtToken(user);
            return token;
        }

        public async Task<User> Detail(string id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<IEnumerable<User>> All()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User> Update(string id, UserViewModel user)
        {
            var existingUser = await _context.Users.FindAsync(id);
            if (existingUser == null)
                throw new Exception("User not found");
            ValidatePhoneNumber(user.Phone);
            user.Email = user.Email;
            user.Phone = user.Phone;
            user.Address = user.Address;
            user.Faculty = user.Faculty;
            _context.Users.Update(existingUser);
            await _context.SaveChangesAsync();
            return existingUser;
        }
        public async Task<bool> ChangeAvatar(string id, IFormFile avatar)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
                throw new KeyNotFoundException("User not found");
            string directory;
            if (user.RoleId == 3)
                directory = "wwwroot/avatars/hv";
            else
                directory = "wwwroot/avatars/gv";
            if (avatar != null && avatar.Length > 0)
            {
                var fileName = $"{id}_{Path.GetFileName(avatar.FileName)}";

                var filePath = Path.Combine(directory, fileName);
                if (!Directory.Exists(directory))
                    Directory.CreateDirectory(directory);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await avatar.CopyToAsync(stream);
                }
                user.Avatar = filePath;
                _context.Users.Update(user);
                await _context.SaveChangesAsync();
            }
            return true;
        }

        public async Task<bool> Delete(string id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
                throw new Exception("User not found");
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<User> ChangePassword(string id, ChangePasswordViewModel model)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
                throw new Exception("User not found");
            if (BCrypt.Net.BCrypt.HashPassword(model.OldPassword) != user.Password)
                throw new Exception("Old password is incorrect");
            if (model.NewPassword != model.ConfirmPassword)
                throw new Exception("New password and confirm password do not match");
            user.Password = BCrypt.Net.BCrypt.HashPassword(model.NewPassword);
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<bool> ForgotPassword(string email)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (user == null)
                throw new Exception("User not found");
            var newPassword = GenerateRandomPassword();
            ValidatePassword(newPassword);
            user.Password = BCrypt.Net.BCrypt.HashPassword(newPassword);
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            _emailService.SendEmail(user.Email, "Password Reset", $"Your new password is: {newPassword}");
            return true;
        }
        private void ValidatePhoneNumber(string phoneNumber)
        {
            var regex = new Regex(@"^0\d{9}$");
            if (!regex.IsMatch(phoneNumber))
                throw new ValidationException("Phone number must start with 0 and be exactly 10 digits.");
        }
        private string GenerateUserId(int role)
        {
            string prefix;
            int counter;

            if (role == 1 || role == 2)
            {
                prefix = "GV";
                counter = ++adminCounter;
            }
            else
            {
                prefix = "HV";
                counter = ++studentCounter;
            }

            return $"{prefix}{counter:D3}";
        }
        private void ValidatePassword(string password)
        {
            if (password.Length < 8)
                throw new Exception("Password must be at least 8 characters long");
            if (!password.Any(char.IsUpper))
                throw new Exception("Password must contain at least one uppercase letter");
            if (!Regex.IsMatch(password, @"[!@#$%^&*(),.?"":{}|<>]"))
                throw new Exception("Password must contain at least one special character");
        }
        private string GenerateJwtToken(User user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var roleName = _context.Roles.FirstOrDefault(r => r.Id == user.RoleId)?.Name;
            var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.Name),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.MobilePhone, user.Phone),
            new Claim(ClaimTypes.Gender, user.Gender.ToString()),
            new Claim(ClaimTypes.StreetAddress, user.Address),
            new Claim(ClaimTypes.Role, roleName)
        };
            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: credentials
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        private string GenerateRandomPassword()
        {
            const string upper = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            const string lower = "abcdefghijklmnopqrstuvwxyz";
            const string digits = "0123456789";
            const string special = "!@#$%^&*(),.?\":{}|<>";
            const string allChars = upper + lower + digits + special;
            var random = new Random();
            var passwordChars = new char[12];
            passwordChars[0] = upper[random.Next(upper.Length)];
            passwordChars[1] = lower[random.Next(lower.Length)];
            passwordChars[2] = digits[random.Next(digits.Length)];
            passwordChars[3] = special[random.Next(special.Length)];
            for (int i = 4; i < passwordChars.Length; i++)
                passwordChars[i] = allChars[random.Next(allChars.Length)];
            return new string(passwordChars.OrderBy(c => random.Next()).ToArray());
        }
    }
}
