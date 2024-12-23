using DigitalPortalAcademy.Models;
using Microsoft.EntityFrameworkCore;

namespace DigitalPortalAcademy.Services
{
    public class UserAccountService
    {
        private readonly DigitalPortalContext _context;
        private readonly AuthenticationService _authenticationService;

        public UserAccountService(DigitalPortalContext context, AuthenticationService authenticationService)
        {
            _context = context;
            _authenticationService = authenticationService;
        }
        public async Task<User> RegisterUserAsync(string email, string password, string role, string fullName, string uniqueCode)
        {
            ValidateFullName(fullName, out string firstName, out string lastName, out string? middleName);

            await EnsureEmailIsUniqueAsync(email);
            await ValidateUniqueCodeAsync(uniqueCode);

            int? userId = await GetUserIdByRoleAsync(role, firstName, lastName, middleName);
            if (userId == null || userId == 0)
            {
                throw new InvalidOperationException($"Информация о пользователе с именем {fullName} и ролью {role} не найдена.");
            }

            var passwordHash = _authenticationService.HashPassword(null, password);

            var user = new User
            {
                Email = email,
                PasswordHash = passwordHash,
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            await LinkUserToRoleAsync(role, userId.Value, user.UserId);

            return user;
        }

        private void ValidateFullName(string fullName, out string firstName, out string lastName, out string? middleName)
        {
            var nameParts = fullName.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (nameParts.Length < 2)
            {
                throw new InvalidOperationException("Полное имя должно содержать как минимум имя и фамилию.");
            }

            firstName = nameParts[0];
            lastName = nameParts[1];
            middleName = nameParts.Length > 2 ? nameParts[2] : null;
        }

        private async Task EnsureEmailIsUniqueAsync(string email)
        {
            var existingUserByEmail = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (existingUserByEmail != null)
            {
                throw new InvalidOperationException("Пользователь с таким email уже существует.");
            }
        }

        private async Task ValidateUniqueCodeAsync(string uniqueCode)
        {
            var codeEntry = await _context.UniqueCodes.FirstOrDefaultAsync(c => c.UniqueCodeName == uniqueCode);
            if (codeEntry == null)
            {
                throw new InvalidOperationException("Указанный уникальный код недействителен.");
            }

            _context.UniqueCodes.Remove(codeEntry);
        }

        private async Task<int?> GetUserIdByRoleAsync(string role, string firstName, string lastName, string? middleName)
        {
            return role switch
            {
                "Преподаватель" => await _context.Teachers
                    .Where(t => t.FirstName == firstName && t.LastName == lastName && (middleName == null || t.MiddleName == middleName))
                    .Select(t => t.TeacherId)
                    .FirstOrDefaultAsync(),

                "Студент" => await _context.Students
                    .Where(s => s.FirstName == firstName && s.LastName == lastName && (middleName == null || s.MiddleName == middleName))
                    .Select(s => s.StudentId)
                    .FirstOrDefaultAsync(),

                "Педагог-организатор" => await _context.Curators
                    .Where(o => o.FirstName == firstName && o.LastName == lastName && (middleName == null || o.MiddleName == middleName))
                    .Select(o => o.CuratorId)
                    .FirstOrDefaultAsync(),

                "Сотрудник учебной части" => await _context.Staff
                    .Where(a => a.FirstName == firstName && a.LastName == lastName && (middleName == null || a.MiddleName == middleName))
                    .Select(a => a.StaffId)
                    .FirstOrDefaultAsync(),

                _ => throw new InvalidOperationException("Указанная роль недопустима.")
            };
        }

        private async Task LinkUserToRoleAsync(string role, int userId, int accountId)
        {
            switch (role)
            {
                case "Преподаватель":
                    var teacher = await _context.Teachers.FindAsync(userId);
                    if (teacher != null) teacher.UserId = accountId;
                    break;

                case "Студент":
                    var student = await _context.Students.FindAsync(userId);
                    if (student != null) student.UserId = accountId;
                    break;

                case "Педагог-организатор":
                    var curator = await _context.Curators.FindAsync(userId);
                    if (curator != null) curator.UserId = accountId;
                    break;

                case "Сотрудник учебной части":
                    var staff = await _context.Staff.FindAsync(userId);
                    if (staff != null) staff.UserId = accountId;
                    break;

                default:
                    throw new InvalidOperationException("Указанная роль недопустима.");
            }

            await _context.SaveChangesAsync();
        }



        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }
    }
}
