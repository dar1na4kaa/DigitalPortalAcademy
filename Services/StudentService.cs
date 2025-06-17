using System.Globalization;
using DigitalPortalAcademy.Models;
using DigitalPortalAcademy.ViewModels;
using DigitalPortalAcademy.ViewModels.DigitalPortalAcademy.Models.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace DigitalPortalAcademy.Services
{
    public class StudentService
    {
        private readonly AcademyContext _context;
        public StudentService()
        {
            _context = new AcademyContext();
        }
        public User? GetUserById(int id)
        {
            return _context.Users
                .Include(u => u.Role)
                .Include(u => u.Students)
                    .ThenInclude(s => s.Group)
                        .ThenInclude(s => s.Specialty)
                .FirstOrDefault(u => u.UserId == id);
        }

        public List<SheduleViewModel>? GetScheduleForStudent(int userId)
        {
            var user = GetUserById(userId);
            if (user == null)
                return null;

            var student = user.Students?.FirstOrDefault();
            if (student == null || student.Group == null)
                return null;

            string groupName = student.Group.GroupName;

            var schedule = new List<SheduleViewModel>
                            {
                                new() { Day = DayOfWeek.Monday },
                                new() { Day = DayOfWeek.Tuesday },
                                new() { Day = DayOfWeek.Wednesday },
                                new() { Day = DayOfWeek.Thursday },
                                new() { Day = DayOfWeek.Friday }
                            };

            try
            {
                var schedules = _context.Schedules
                    .Include(s => s.Pair)
                        .ThenInclude(p => p.Group)
                    .Include(s => s.Pair.TeacherSubject)
                        .ThenInclude(ts => ts.Subject)
                    .Include(s => s.Pair.TeacherSubject.Teacher)
                    .Include(s => s.Day)
                    .Include(s => s.Room)
                        .ThenInclude(r => r.Building)
                    .Include(s => s.ClassType)
                    .Include(s => s.TimeSlot)
                    .Where(s => s.Pair.Group.GroupName == groupName)
                    .ToList();

                foreach (var s in schedules)
                {
                    var day = MapRussianDayToDayOfWeek(s.Day.DayName);
                    if (day == null) continue;

                    var viewModel = new PairViewModel
                    {
                        NumberPair = s.PairId.ToString(),
                        PairName = s.Pair.TeacherSubject.Subject.SubjectName,
                        TimeSlot = $"{s.TimeSlot.StartTime} - {s.TimeSlot.EndTime}",
                        Room = s.Room.RoomName,
                        ClassType = s.ClassType.ClassTypeName
                    };

                    schedule.First(d => d.Day == day).Pairs.Add(viewModel);
                }
            }
            catch
            {
                return null;
            }

            return schedule;
        }

        private DayOfWeek? MapRussianDayToDayOfWeek(string dayName)
        {
            return dayName switch
            {
                "Понедельник" => DayOfWeek.Monday,
                "Вторник" => DayOfWeek.Tuesday,
                "Среда" => DayOfWeek.Wednesday,
                "Четверг" => DayOfWeek.Thursday,
                "Пятница" => DayOfWeek.Friday,
                "Суббота" => DayOfWeek.Saturday,
                "Воскресенье" => DayOfWeek.Sunday,
                _ => null
            };
        }
        public Curriculum? GetCurriculumForStudent(int userId)
        {
            return _context.Students
                .Where(s => s.UserId == userId)
                .SelectMany(s => s.Group.Specialty.Curricula)
                .OrderByDescending(c => c.StartYear)
                .FirstOrDefault();
        }
        public List<ReferenceRequestViewModel> GetReferenceRequests(int userId)
        {
            var student = _context.Students.FirstOrDefault(s => s.UserId == userId);
            if (student == null)
                return new List<ReferenceRequestViewModel>();

            return _context.ReferenceRequests
                .Include(r => r.Reference)
                .Where(r => r.StudentId == student.StudentId)
                .OrderByDescending(r => r.CreatedAt)
                .Select(r => new ReferenceRequestViewModel
                {
                    ReferenceType = r.ReferenceType,
                    Comment = r.Comment,
                    CreatedAt = r.CreatedAt,
                    FilePath = r.Reference != null ? Path.Combine("wwwroot", "lib", "img", "spravki", r.Reference.FilePath) : null
                })
                .ToList();
        }
        public void CreateReferenceRequest(int userId, string requestType, string? comment)
        {
            var student = _context.Students.FirstOrDefault(s => s.UserId == userId);
            if (student == null) return;

            var request = new ReferenceRequest
            {
                StudentId = student.StudentId,
                ReferenceType = requestType,
                Comment = comment,
                CreatedAt = DateTime.Now
            };

            _context.ReferenceRequests.Add(request);
            _context.SaveChanges();
        }
        public List<PerfomanceItemViewModel> GetPerformance(int userId, string selectedMonth)
        {
            var student = GetStudentByUserId(userId);
            if (student == null) return new List<PerfomanceItemViewModel>();

            int year = GetCurrentAcademicYear();
            var marks = GetMarksForStudent(student.StudentId, year, selectedMonth);

            return MapMarksToViewModel(marks, selectedMonth);
        }

        private Student GetStudentByUserId(int userId)
        {
            return _context.Students.FirstOrDefault(s => s.UserId == userId);
        }

        private int GetCurrentAcademicYear()
        {
            var now = DateTime.Now;
            return now.Month >= 7 ? now.Year : now.Year - 1;
        }

        private IEnumerable<MarksReportDetail> GetMarksForStudent(int studentId, int year, string selectedMonth)
        {
            var query = _context.MarksReportDetails
                .Include(mrd => mrd.Report)
                    .ThenInclude(r => r.Pair)
                        .ThenInclude(p => p.TeacherSubject)
                            .ThenInclude(ts => ts.Subject)
                .Where(mrd =>
                    mrd.StudentId == studentId);

            if (!string.IsNullOrEmpty(selectedMonth) && selectedMonth.ToLower() != "все")
            {
                int monthNum = MonthNameToNumber(selectedMonth);
                query = query.Where(mrd => mrd.Report.ReportMonth.Month == monthNum);
            }

            return query.ToList();
        }

        private List<PerfomanceItemViewModel> MapMarksToViewModel(IEnumerable<MarksReportDetail> marks, string selectedMonth)
        {
            var culture = CultureInfo.GetCultureInfo("ru-RU");

            return marks.Select(mrd => new PerfomanceItemViewModel
            {
                Month = mrd.Report.ReportMonth.ToString("MMMM", culture),
                Subject = mrd.Report.Pair.TeacherSubject.Subject.SubjectName,
                Mark = mrd.Mark,
                MissedHours = mrd.Absences
            })
            .OrderBy(pi => MonthNameToNumber(pi.Month))
            .ThenBy(pi => pi.Subject)
            .ToList();
        }


        public List<string> GetAvailableSubjects(int userId)
        {
            var student = _context.Students.FirstOrDefault(s => s.UserId == userId);
            if (student == null) return new List<string>();

            var subjects = _context.Pairs
                .Where(p => p.GroupId == student.GroupId)
                .Select(p => p.TeacherSubject.Subject.SubjectName)
                .Distinct()
                .ToList();

            return subjects;
        }


        private int MonthNameToNumber(string monthName)
        {
            return monthName.ToLower() switch
            {
                "январь" => 1,
                "февраль" => 2,
                "март" => 3,
                "апрель" => 4,
                "май" => 5,
                "июнь" => 6,
                "июль" => 7,
                "август" => 8,
                "сентябрь" => 9,
                "октябрь" => 10,
                "ноябрь" => 11,
                "декабрь" => 12,
                _ => 0
            };
        }
        public EditUserViewModel? GetUserForEdit(int userId)
        {
            var user = GetUserById(userId);
            if (user == null) return null;

            var student = user.Students.FirstOrDefault();

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
        public EditAccountInformationViewModel? GetAccountForEdit(int userId)
        {
            var user = GetUserById(userId);
            if (user == null) return null;

            var student = user.Students.FirstOrDefault();

            return new EditAccountInformationViewModel
            {
                UserId = user.UserId,
                Login = user.Login,
                RoleName = user.Role.Name,
                FirstName = student?.FirstName ?? string.Empty,
                LastName = student?.LastName ?? string.Empty,
                MiddleName = student?.MiddleName,
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

            var student = user.Students.First();
            student.FirstName = model.FirstName;
            student.LastName = model.LastName;
            student.MiddleName = model.MiddleName;


            _context.SaveChanges();
            return true;
        }
        public List<Announcement> GetNews()
        {
            var announcements = _context.Announcements
                                .Include(a => a.Author)
                                    .ThenInclude(u => u.Employees)
                                .Where(a => a.IsActive == true &&
                                            (a.ExpirationDate == null || a.ExpirationDate > DateTime.Now))
                                .OrderByDescending(a => a.CreatedAt)
                                .ToList();
            return announcements;
        }
    }
}
