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
            // Разделяем полное имя на имя и фамилию
            var nameParts = fullName.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (nameParts.Length < 2)
            {
                throw new InvalidOperationException("Полное имя должно содержать как минимум имя и фамилию.");
            }

            var firstName = nameParts[0];
            var lastName = nameParts[1];

            // Проверяем, существует ли пользователь с указанным email
            var existingUserByEmail = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (existingUserByEmail != null)
            {
                throw new InvalidOperationException("Пользователь с таким email уже существует.");
            }

            // Проверяем наличие информации о пользователе в зависимости от роли
            bool isValidUser = role switch
            {
                "Преподаватель" => await _context.Teachers.AnyAsync(t => t.FirstName == firstName && t.LastName == lastName),
                "Студент" => await _context.Students.AnyAsync(s => s.FirstName == firstName && s.LastName == lastName),
                "Педагог" => await _context.Teachers.AnyAsync(p => p.FirstName == firstName && p.LastName == lastName),
                "Организатор" => await _context.Curators.AnyAsync(o => o.FirstName == firstName && o.LastName == lastName),
                "Сотрудник учебной части" => await _context.AcademicStaff.AnyAsync(a => a.FirstName == firstName && a.LastName == lastName),
                _ => throw new InvalidOperationException("Указанная роль недопустима.")
            };

            if (!isValidUser)
            {
                throw new InvalidOperationException($"Информация о пользователе с именем {fullName} и ролью {role} не найдена.");
            }

            // Проверяем наличие уникального кода
            var codeEntry = await _context.UniqueCodes.FirstOrDefaultAsync(c => c.Code == uniqueCode);
            if (codeEntry == null)
            {
                throw new InvalidOperationException("Указанный уникальный код недействителен.");
            }

            // Удаляем уникальный код из таблицы, так как он используется
            _context.UniqueCodes.Remove(codeEntry);

            // Хешируем пароль
            var passwordHash = _authenticationService.HashPassword(null, password);

            // Создаем нового пользователя
            var user = new User
            {
                Email = email,
                PasswordHash = passwordHash,
                Role = role,
            };

            // Добавляем нового пользователя в базу данных
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return user;
        }


        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }
    }
}
