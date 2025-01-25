using DigitalPortalAcademy.Models;
using Microsoft.EntityFrameworkCore;

namespace DigitalPortalAcademy.Services
{
    public class RegistrationService
    {
        private readonly AcademyContext _context;
        private readonly AuthenticationService _authenticationService;

        public RegistrationService(AcademyContext context, AuthenticationService authenticationService)
        {
            _context = context;
            _authenticationService = authenticationService;
        }

        public async Task<User> RegisterUserAsync(string email, string password, string role, string fullName, string uniqueCode)
        {
            var (firstName, lastName, middleName) = ParseFullName(fullName);

            await EnsureEmailIsUniqueAsync(email);
            await ValidateUniqueCodeAsync(uniqueCode);

            var userId = await GetUserIdByRoleAsync(role, firstName, lastName, middleName)
                         ?? throw new InvalidOperationException($"Пользователь с ФИО {fullName} и ролью {role} не найден.");

            var user = new User
            {
                Login = email,
                PasswordHash = PasswordHasher.HashPassword(password),
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            await UpdateRoleEntityUserIdAsync(role, userId, user.UserId);

            return user;
        }

        private static (string firstName, string lastName, string? middleName) ParseFullName(string fullName)
        {
            var nameParts = fullName.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (nameParts.Length < 2)
                throw new InvalidOperationException("Полное имя должно содержать минимум имя и фамилию.");

            return (nameParts[0], nameParts[1], nameParts.Length > 2 ? nameParts[2] : null);
        }

        private async Task EnsureEmailIsUniqueAsync(string email)
        {
            if (await _context.Users.AnyAsync(u => u.Login == email))
                throw new InvalidOperationException("Пользователь с таким email уже существует.");
        }

        private async Task ValidateUniqueCodeAsync(string uniqueCode)
        {
            var codeEntry = await _context.RegistrationCodes.FirstOrDefaultAsync(c => c.Code == uniqueCode)
                            ?? throw new InvalidOperationException("Указанный уникальный код недействителен.");

            _context.RegistrationCodes.Remove(codeEntry);
        }

        private async Task<int?> GetUserIdByRoleAsync(string role, string firstName, string lastName, string? middleName)
        {
            var query = role switch
            {
                "Преподаватель" => _context.Teachers
                    .Where(t => t.FirstName == firstName && t.LastName == lastName && (middleName == null || t.MiddleName == middleName))
                    .Select(t => t.TeacherId),

                "Студент" => _context.Students
                    .Where(s => s.FirstName == firstName && s.LastName == lastName && (middleName == null || s.MiddleName == middleName))
                    .Select(s => s.StudentId),

                "Педагог-организатор" or "Сотрудник учебной части" => _context.Employees
                    .Where(e => e.FirstName == firstName && e.LastName == lastName && (middleName == null || e.MiddleName == middleName))
                    .Select(e => e.EmployeeId),

                _ => throw new InvalidOperationException("Указанная роль недопустима.")
            };

            return await query.FirstOrDefaultAsync();
        }

        private async Task UpdateRoleEntityUserIdAsync(string role, int entityId, int userId)
        {
            switch (role)
            {
                case "Преподаватель":
                    var teacher = await _context.Teachers.FindAsync(entityId);
                    if (teacher != null) teacher.UserId = userId;
                    break;

                case "Студент":
                    var student = await _context.Students.FindAsync(entityId);
                    if (student != null) student.UserId = userId;
                    break;

                case "Педагог-организатор":
                case "Сотрудник учебной части":
                    var employee = await _context.Employees.FindAsync(entityId);
                    if (employee != null) employee.UserId = userId;
                    break;

                default:
                    throw new InvalidOperationException("Указанная роль недопустима.");
            }

            await _context.SaveChangesAsync();
        }
    }
}
