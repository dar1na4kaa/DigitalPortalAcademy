using DigitalPortalAcademy.Models;
using DigitalPortalAcademy.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace DigitalPortalAcademy.Services
{
    public class TeacherService
    {
        private readonly AcademyContext _context;
        public TeacherService() => _context = new AcademyContext();
        public Group GetGroupDetails(int groupId)
        {
            return _context.Groups
                .Include(g => g.Students)
                .Include(g => g.Curator)
                .Include(g => g.Specialty)
                .FirstOrDefault(g => g.GroupId == groupId);
        }


        public List<Group> GetTeacherGroups(int userId)
        {
            var teacherId = _context.Teachers
                .Where(t => t.UserId == userId)
                .Select(t => t.TeacherId)
                .FirstOrDefault();

            if (teacherId == 0) return new();

            return _context.Pairs
                .Where(p => p.TeacherSubject.TeacherId == teacherId)
                .Select(p => p.Group)
                .Distinct()
                .Include(g => g.Students)
                .Include(g => g.Curator)
                .ToList();
        }

        public List<Subject> GetSubjectsByGroup(int teacherId, int groupId)
        {
            return _context.Pairs
                .Include(p => p.TeacherSubject)
                    .ThenInclude(ts => ts.Subject)
                .Where(p => p.GroupId == groupId && p.TeacherSubject.TeacherId == teacherId)
                .Select(p => p.TeacherSubject.Subject)
                .Distinct()
                .ToList();
        }

        public List<Student> GetGroupStudents(int groupId)
        {
            return _context.Students
                .Where(s => s.GroupId == groupId)
                .ToList();
        }

        public ReportFilterViewModel GetInitialReportModel(int teacherId)
        {
            var groups = GetTeacherGroups(teacherId);

            var subjectIds = groups
                .SelectMany(g => GetSubjectsByGroup(teacherId, g.GroupId))
                .Distinct()
                .ToList();

            return new ReportFilterViewModel
            {
                Groups = groups,
                Subjects = subjectIds,
                Students = new(),
                Details = new()
            };
        }

        public ReportFilterViewModel GetReportData(int teacherId, int groupId, int subjectId)
        {
            var groups = GetTeacherGroups(teacherId);
            var subjects = GetSubjectsByGroup(teacherId, groupId);
            var students = GetGroupStudents(groupId);

            var details = students.Select(s => new ReportDetailViewModel
            {
                StudentId = s.StudentId,
                Mark = null,
                MissedHours = 0
            }).ToList();

            return new ReportFilterViewModel
            {
                Groups = groups,
                Subjects = subjects,
                Students = students,
                Details = details,
                SelectedGroupId = groupId,
                SelectedSubjectId = subjectId
            };
        }

        public void SaveReport(int teacherId, ReportFilterViewModel model)
        {
            var pair = _context.Pairs
                .Include(p => p.TeacherSubject)
                .FirstOrDefault(p =>
                    p.GroupId == model.SelectedGroupId &&
                    p.TeacherSubject.TeacherId == teacherId &&
                    p.TeacherSubject.SubjectId == model.SelectedSubjectId);

            if (pair == null)
                throw new Exception("Пара не найдена или доступ запрещён");

            var report = new MarksReport
            {
                PairId = pair.PairId,
                ReportMonth = DateOnly.FromDateTime(DateTime.Now),
                CreatedAt = DateTime.Now
            };

            _context.MarksReports.Add(report);
            _context.SaveChanges();

            foreach (var detail in model.Details)
            {
                _context.MarksReportDetails.Add(new MarksReportDetail
                {
                    ReportId = report.ReportId,
                    StudentId = detail.StudentId,
                    Mark = detail.Mark,
                    Absences = detail.MissedHours
                });
            }

            _context.SaveChanges();
        }
        public List<MarksReport> GetReportsByTeacher(int teacherId)
        {
            return _context.MarksReports
                .Include(r => r.Pair)
                    .ThenInclude(p => p.Group)
                .Include(r => r.Pair)
                    .ThenInclude(p => p.TeacherSubject)
                        .ThenInclude(ts => ts.Subject)
                .Where(r => r.Pair.TeacherSubject.TeacherId == teacherId)
                .OrderByDescending(r => r.CreatedAt)
                .ToList();
        }
        public MarksReport GetReportWithDetails(int reportId)
        {
            return _context.MarksReports
                .Include(r => r.Pair)
                    .ThenInclude(p => p.Group)
                .Include(r => r.Pair)
                    .ThenInclude(p => p.TeacherSubject)
                        .ThenInclude(ts => ts.Subject)
                .Include(r => r.MarksReportDetails)
                    .ThenInclude(d => d.Student)
                .FirstOrDefault(r => r.ReportId == reportId);
        }
        public void DeleteReport(int reportId)
        {
            var report = _context.MarksReports
                .Include(r => r.MarksReportDetails)
                .FirstOrDefault(r => r.ReportId == reportId);

            if (report != null)
            {
                _context.MarksReportDetails.RemoveRange(report.MarksReportDetails);
                _context.MarksReports.Remove(report);
                _context.SaveChanges();
            }
        }


    }
}
