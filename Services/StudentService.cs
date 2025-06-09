using DigitalPortalAcademy.Models;
using DigitalPortalAcademy.ViewModels;
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
                .Include(u => u.Employees)
                .Include(u => u.Students)
                    .ThenInclude(s => s.Group)
                .Include(u => u.Teachers)
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
                        NumberPair = s.TimeSlot.StartTime.ToString(),
                        PairName = s.Pair.TeacherSubject.Subject.SubjectName,
                        TimeSlot = $"{s.TimeSlot.StartTime:hh\\:mm} - {s.TimeSlot.EndTime:hh\\:mm}",
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

    }
}
