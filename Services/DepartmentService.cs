using DigitalPortalAcademy.Models;
using DigitalPortalAcademy.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace DigitalPortalAcademy.Services
{
    public class DepartmentService
    {
        private readonly AcademyContext _context;

        public DepartmentService(AcademyContext context)
        {
            _context = context;
        }
        public List<Specialty> GetAllSpecialties()
        {
            return _context.Specialties.ToList();
        }
        public string GetSpecialtyNameById(int id)
        {
            return _context.Specialties.FirstOrDefault(s => s.SpecialtyId == id)?.SpecialtyName ?? "Unknown";
        }
        public void SaveCurriculum(CurriculumAddViewModel model)
        {
            var fileName = $"{Guid.NewGuid()}_{model.PlanFile?.FileName}";
            var filePath = Path.Combine("wwwroot", "lib", "img", "files", "curriculum-path", fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                model.PlanFile?.CopyTo(stream);
            }

            var curriculum = new Curriculum
            {
                StudyYear = model.Course,
                SpecialtyId = model.Specialty,
                StartYear = DateTime.Now.Year,
                PlanFilePath = $"{fileName}"
            };

            _context.Curriculums.Add(curriculum);
            _context.SaveChanges();
        }
        public List<Announcement> GetAnnouncements()
        {
            var announcements = _context.Announcements
                    .Include(a => a.Author)
                        .ThenInclude(u => u.Employees)
                    .Where(a => a.IsActive == true && a.ExpirationDate >= DateTime.Now)
                    .OrderByDescending(a => a.CreatedAt)
                    .ToList();
            return announcements;
        }
        public void SaveAnnoucement(AnnouncementAddViewModel model,int userId)
        {
            var announcement = new Announcement
            {
                Title = model.Title,
                Description = model.Description,
                CreatedAt = DateTime.Now,
                ExpirationDate = model.ExpirationDate,
                AuthorId = userId,
                IsActive = true
            };

            _context.Announcements.Add(announcement);
            _context.SaveChanges();
        }
        public EditUserViewModel? GetUserForEdit(int userId)
        {
            var user = GetUserById(userId);
            if (user == null) return null;

            var student = user.Employees.FirstOrDefault();

            return new EditUserViewModel
            {
                UserId = user.UserId,
                Login = user.Login,
                RoleName = user.Role.Name,
                FirstName = student?.FirstName ?? string.Empty,
                LastName = student?.LastName ?? string.Empty,
                MiddleName = student?.MiddleName ?? string.Empty
            };
        }
        public User? GetUserById(int id)
        {
            return _context.Users
                .Include(u => u.Role)
                .Include(u => u.Employees)
                        .ThenInclude(u => u.Position)
                            .ThenInclude(u => u.Department)
                .FirstOrDefault(u => u.UserId == id);
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
                user.PasswordHash =  /*PasswordHasher.HashPassword(model.NewPassword); */ model.NewPassword;

            var employee = user.Employees.First();
            employee.FirstName = model.FirstName;
            employee.LastName = model.LastName;
            employee.MiddleName = model.MiddleName;


            _context.SaveChanges();
            return true;
        }

    }
}
