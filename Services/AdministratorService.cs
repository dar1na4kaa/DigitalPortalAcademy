using DigitalPortalAcademy.Models;
using DigitalPortalAcademy.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace DigitalPortalAcademy.Services
{
    public class AdministratorService
    {
        private readonly AcademyContext _context;

        public AdministratorService()
        {
            _context = new AcademyContext();
        }

        public List<User> GetUsers(int? userId)
        {
            return _context.Users
                .AsNoTracking()
                .Include(u => u.Role)
                .Where(u => u.UserId != userId)
                .Include(u => u.Employees)
                .Include(u => u.Students)
                .Include(u => u.Teachers)
                .ToList();
        }

        public User? GetUserById(int id)
        {
            return _context.Users
                .Include(u => u.Role)
                .Include(u => u.Employees)
                .Include(u => u.Students)
                .Include(u => u.Teachers)
                .FirstOrDefault(u => u.UserId == id);
        }

        public bool DeleteUser(int id)
        {
            var user = _context.Users
                .Include(u => u.Employees)
                .Include(u => u.Students)
                .Include(u => u.Teachers)
                .FirstOrDefault(u => u.UserId == id);

            if (user == null) return false;

            if (user.Employees != null)
                _context.Employees.RemoveRange(user.Employees);

            if (user.Students != null)
                _context.Students.RemoveRange(user.Students);

            if (user.Teachers != null)
                _context.Teachers.RemoveRange(user.Teachers);

            _context.Users.Remove(user);
            _context.SaveChanges();

            return true;
        }


        public EditUserViewModel? GetUserForEdit(int userId)
        {
            var user = GetUserById(userId);
            if (user == null) return null;

            var employee = user.Employees.FirstOrDefault();
            var student = user.Students.FirstOrDefault();
            var teacher = user.Teachers.FirstOrDefault();

            return new EditUserViewModel
            {
                UserId = user.UserId,
                Login = user.Login,
                RoleName = user.Role.Name,
                FirstName = employee?.FirstName ?? student?.FirstName ?? teacher?.FirstName ?? string.Empty,
                LastName = employee?.LastName ?? student?.LastName ?? teacher?.LastName ?? string.Empty,
                MiddleName = employee?.MiddleName ?? student?.MiddleName ?? teacher?.MiddleName
            };
        }
        public EditAccountInformationViewModel? GetAccountForEdit(int userId)
        {
            var user = GetUserById(userId);
            if (user == null) return null;

            var employee = user.Employees.FirstOrDefault();

            return new EditAccountInformationViewModel
            {
                UserId = user.UserId,
                Login = user.Login,
                RoleName = user.Role.Name,
                FirstName = employee?.FirstName ?? string.Empty,
                LastName = employee?.LastName ?? string.Empty,
                MiddleName = employee?.MiddleName,
                CurrentAvatarPath = user.PhotoPath
            };
        }

        public bool UpdateUser(EditUserViewModel model)
        {
            var user = GetUserById(model.UserId);
            if (user == null) return false;

            user.Login = model.Login;

            if (!string.IsNullOrEmpty(model.NewPassword))
                user.PasswordHash = PasswordHasher.HashPassword(model.NewPassword);

            if (user.Employees.Any())
            {
                var employee = user.Employees.First();
                employee.FirstName = model.FirstName;
                employee.LastName = model.LastName;
                employee.MiddleName = model.MiddleName;
            }
            if (user.Students.Any())
            {
                var student = user.Students.First();
                student.FirstName = model.FirstName;
                student.LastName = model.LastName;
                student.MiddleName = model.MiddleName;
            }
            if (user.Teachers.Any())
            {
                var teacher = user.Teachers.First();
                teacher.FirstName = model.FirstName;
                teacher.LastName = model.LastName;
                teacher.MiddleName = model.MiddleName;
            }

            _context.SaveChanges();
            return true;
        }
        public bool UpdateAccount(EditAccountInformationViewModel model)
        {
            var user = GetUserById(model.UserId);
            if (user == null) return false;

            user.Login = model.Login;

            if (model.AvatarFile != null)
            {
                var filePath = Path.Combine("wwwroot", "lib", "img", "files", "photo-user", model.AvatarFile.FileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    model.AvatarFile.CopyTo(stream);
                }

                user.PhotoPath = model.AvatarFile.FileName;
            }

            if (!string.IsNullOrEmpty(model.NewPassword))
                user.PasswordHash = PasswordHasher.HashPassword(model.NewPassword);

            var employee = user.Employees.First();
            employee.FirstName = model.FirstName;
            employee.LastName = model.LastName;
            employee.MiddleName = model.MiddleName;


            _context.SaveChanges();
            return true;
        }
        public List<User> GetFilteredUsers(int? roleId, string? search)
        {
            var usersQuery = _context.Users
                .Include(u => u.Role)
                .Include(u => u.Employees)
                    .ThenInclude(e => e.Position)
                        .ThenInclude(p => p.Department)
                .Include(u => u.Students)
                    .ThenInclude(s => s.Group)
                        .ThenInclude(g => g.Curator)
                            .ThenInclude(c => c.Position)
                                .ThenInclude(p => p.Department)
                .Include(u => u.Teachers)
                .AsQueryable();

            if (roleId.HasValue)
            {
                usersQuery = usersQuery.Where(u => u.RoleId == roleId.Value);
            }

            if (!string.IsNullOrWhiteSpace(search))
            {
                string lowerSearch = search.ToLower();
                usersQuery = usersQuery.Where(u =>
                    u.Employees.Any(e => (e.LastName + " " + e.FirstName + " " + e.MiddleName).ToLower().Contains(lowerSearch)) ||
                    u.Students.Any(s => (s.LastName + " " + s.FirstName + " " + s.MiddleName).ToLower().Contains(lowerSearch)) ||
                    u.Teachers.Any(t => (t.LastName + " " + t.FirstName + " " + t.MiddleName).ToLower().Contains(lowerSearch)));
            }

            return usersQuery.ToList();
        }


        public List<Department> GetAllDepartments()
        {
            return _context.Departments.ToList();
        }
    }
}
