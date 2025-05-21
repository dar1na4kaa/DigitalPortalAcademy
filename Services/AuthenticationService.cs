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

        public User RegisterUser(string email, string password, string role, string firstName, string lastName, string middleName, string uniqueNumber)
        {
            EnsureEmailIsUnique(email);

            int? userId = GetExistingPersonIdByRole(role, firstName, lastName, middleName, uniqueNumber);
            if (userId == null)
            {
                throw new InvalidOperationException($"{role} с ФИО {lastName} {firstName} {middleName} и уникальным номером {uniqueNumber} не найден.");
            }

            var roleEntity = _context.Roles.FirstOrDefault(t => t.Name == role);
            if (roleEntity == null)
            {
                throw new InvalidOperationException($"Роль '{role}' не найдена в системе.");
            }

            var user = new User
            {
                Login = email,
                PasswordHash = PasswordHasher.HashPassword(password),
                RoleId = roleEntity.RolesId
            };

            try
            {
                _context.Users.Add(user);
                _context.SaveChanges();

                UpdateUserId(role, userId.Value, user.UserId);
                return user;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Ошибка при регистрации пользователя", ex);
            }
        }

        private void EnsureEmailIsUnique(string email)
        {
            if (_context.Users.Any(u => u.Login == email))
                throw new InvalidOperationException("Пользователь с таким email уже существует.");
        }

        private int? GetExistingPersonIdByRole(string role, string firstName, string lastName, string? middleName, string uniqueNumber)
        {
            return role switch
            {
                "Преподаватель" => _context.Teachers.First(t => t.FirstName == firstName && t.LastName == lastName && (middleName == null || t.MiddleName == middleName) && t.PersonnelNumber == uniqueNumber)?.TeacherId,
                "Студент" => _context.Students.FirstOrDefault(s => s.FirstName == firstName && s.LastName == lastName && (middleName == null || s.MiddleName == middleName) && s.StudentNumber == uniqueNumber)?.StudentId,
                "Педагог-организатор" or "Сотрудник учебной части" or "Администратор" => _context.Employees.FirstOrDefault(e => e.FirstName == firstName && e.LastName == lastName && (middleName == null || e.MiddleName == middleName) && e.PersonnelNumber == uniqueNumber)?.EmployeeId,
            };
        }

        private void UpdateUserId(string role, int dbId, int userId)
        {
            object? entity = role switch
            {
                "Преподаватель" => _context.Teachers.Find(dbId),
                "Студент" => _context.Students.Find(dbId),
                "Педагог-организатор" or "Сотрудник учебной части" or "Администратор" => _context.Employees.Find(dbId),
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
            try
            {
                return _context.Users.Include(u => u.Role).FirstOrDefault(u => u.Login == email);

            }
            catch(Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }

        public bool VerifyPassword(User user, string enteredPassword)
        {
            /*return PasswordHasher.HashPassword(enteredPassword) == user.PasswordHash;*/
            return enteredPassword == user.PasswordHash;
        }
    }
}
