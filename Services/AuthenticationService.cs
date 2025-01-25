using DigitalPortalAcademy.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DigitalPortalAcademy.Services
{
    public class AuthenticationService
    {
        private readonly AcademyContext _context;
        public AuthenticationService(AcademyContext context)
        {
            _context = context;
        }
        public Task<User?> GetUserByEmailAsync(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                throw new ArgumentException("Email не может быть пустым или содержать только пробелы.", nameof(email));
            }

            return _context.Users
                .Include(u => u.Role) // Если нужно включить связанную таблицу ролей
                .FirstOrDefaultAsync(u => u.Login == email);
        }
        public bool VerifyPassword(User user, string enteredPassword)
        {
            string hashedPassword = PasswordHasher.HashPassword(enteredPassword);
            bool result = hashedPassword.Equals(user.PasswordHash);
            return result;
        }
    }
}
