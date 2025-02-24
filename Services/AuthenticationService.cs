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

        public User RegisterUser(string email, string password, string role, string fullName, string uniqueCode)
        {
            var (firstName, lastName, middleName) = ParseFullName(fullName);
            EnsureEmailIsUnique(email);
            int userId = GetUserIdByRole(role, firstName, lastName, middleName)
                         ?? throw new InvalidOperationException($"Пользователь с ФИО {fullName} и ролью {role} не найден.");
            ValidateUniqueCode(uniqueCode);

            var user = new User { Login = email, PasswordHash = PasswordHasher.HashPassword(password) };
            _context.Users.Add(user);
            _context.SaveChanges();
            UpdateUserId(role, userId, user.UserId);
            return user;
        }

        private (string FirstName, string LastName, string? MiddleName) ParseFullName(string fullName)
        {
            var parts = fullName.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length < 2)
                throw new InvalidOperationException("Полное имя должно содержать минимум имя и фамилию.");

            return (parts[0], parts[1], parts.Length > 2 ? parts[2] : null);
        }

        private void EnsureEmailIsUnique(string email)
        {
            if (_context.Users.Any(u => u.Login == email))
                throw new InvalidOperationException("Пользователь с таким email уже существует.");
        }

        private void ValidateUniqueCode(string uniqueCode)
        {
            var codeEntry = _context.RegistrationCodes.FirstOrDefault(c => c.Code == uniqueCode)
                            ?? throw new InvalidOperationException("Указанный уникальный код недействителен.");
            _context.RegistrationCodes.Remove(codeEntry);
        }

        private int? GetUserIdByRole(string role, string firstName, string lastName, string? middleName)
        {
            return role switch
            {
                "Преподаватель" => _context.Teachers.FirstOrDefault(t => t.FirstName == firstName && t.LastName == lastName && (middleName == null || t.MiddleName == middleName))?.TeacherId,
                "Студент" => _context.Students.FirstOrDefault(s => s.FirstName == firstName && s.LastName == lastName && (middleName == null || s.MiddleName == middleName))?.StudentId,
                "Педагог-организатор" or "Сотрудник учебной части" => _context.Employees.FirstOrDefault(e => e.FirstName == firstName && e.LastName == lastName && (middleName == null || e.MiddleName == middleName))?.EmployeeId,
                _ => throw new InvalidOperationException("Указанная роль недопустима.")
            };
        }

        private void UpdateUserId(string role, int dbId, int userId)
        {
            object? entity = role switch
            {
                "Преподаватель" => _context.Teachers.Find(dbId),
                "Студент" => _context.Students.Find(dbId),
                "Педагог-организатор" or "Сотрудник учебной части" => _context.Employees.Find(dbId),
                _ => throw new InvalidOperationException("Указанная роль недопустима.")
            };

            if (entity != null)
            {
                var userIdProperty = entity.GetType().GetProperty("UserId");
                if (userIdProperty != null)
                {
                    userIdProperty.SetValue(entity, userId);
                    _context.SaveChanges();
                }
            }
        }


        public User? GetUserByEmail(string email)
        {
            return _context.Users.Include(u => u.Role).FirstOrDefault(u => u.Login == email);
        }

        public bool VerifyPassword(User user, string enteredPassword)
        {
            return PasswordHasher.HashPassword(enteredPassword) == user.PasswordHash;
        }
    }
}
