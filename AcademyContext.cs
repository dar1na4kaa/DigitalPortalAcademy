using System;
using System.Collections.Generic;
using DigitalPortalAcademy.Models;
using Microsoft.EntityFrameworkCore;

namespace DigitalPortalAcademy;

public partial class AcademyContext : DbContext
{
    public AcademyContext()
    {
    }

    public AcademyContext(DbContextOptions<AcademyContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Announcement> Announcements { get; set; }

    public virtual DbSet<Building> Buildings { get; set; }

    public virtual DbSet<ClassType> ClassTypes { get; set; }

    public virtual DbSet<Curriculum> Curriculums { get; set; }

    public virtual DbSet<CycleCommission> CycleCommissions { get; set; }

    public virtual DbSet<Day> Days { get; set; }

    public virtual DbSet<Department> Departments { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<Group> Groups { get; set; }

    public virtual DbSet<MarksReport> MarksReports { get; set; }

    public virtual DbSet<MarksReportDetail> MarksReportDetails { get; set; }

    public virtual DbSet<Pair> Pairs { get; set; }

    public virtual DbSet<Position> Positions { get; set; }

    public virtual DbSet<RegistrationCode> RegistrationCodes { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Room> Rooms { get; set; }

    public virtual DbSet<Schedule> Schedules { get; set; }

    public virtual DbSet<Specialty> Specialties { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    public virtual DbSet<Subject> Subjects { get; set; }

    public virtual DbSet<Teacher> Teachers { get; set; }

    public virtual DbSet<TeacherSubject> TeacherSubjects { get; set; }

    public virtual DbSet<TimeSlot> TimeSlots { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Server=DESKTOP-6QHTSJ1;Database=AttApplication;Trusted_Connection=True;Encrypt = true;TrustServerCertificate = true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Announcement>(entity =>
        {
            entity.HasKey(e => e.AnnouncementId).HasName("PK__Announce__9DE44554F436F3E2");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.IsActive).HasDefaultValue(true);

            entity.HasOne(d => d.Author).WithMany(p => p.Announcements)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Announcements_Author");
        });

        modelBuilder.Entity<Building>(entity =>
        {
            entity.HasKey(e => e.BuildingId).HasName("PK__Building__5463CDE43027C05E");
        });

        modelBuilder.Entity<ClassType>(entity =>
        {
            entity.HasKey(e => e.ClassTypeId).HasName("PK__ClassTyp__9AB2129327A9BCD3");
        });

        modelBuilder.Entity<Curriculum>(entity =>
        {
            entity.HasKey(e => e.CurriculumId).HasName("PK__Curricul__06C9FA7C2F47DBC0");

            entity.HasOne(d => d.Specialty).WithMany(p => p.Curricula).HasConstraintName("FK_Curriculums_Specialty");
        });

        modelBuilder.Entity<CycleCommission>(entity =>
        {
            entity.HasKey(e => e.CycleCommissionId).HasName("PK__CycleCom__9F8301B2E187801E");
        });

        modelBuilder.Entity<Day>(entity =>
        {
            entity.HasKey(e => e.DayId).HasName("PK__Days__BF3DD8254089F9F6");
        });

        modelBuilder.Entity<Department>(entity =>
        {
            entity.HasKey(e => e.DepartmentId).HasName("PK__Departme__B2079BCDD516880D");
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.EmployeeId).HasName("PK__Employee__7AD04FF1609D3FF4");

            entity.HasOne(d => d.Position).WithMany(p => p.Employees)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Employee_Position");

            entity.HasOne(d => d.User).WithMany(p => p.Employees).HasConstraintName("FK_Employee_User");
        });

        modelBuilder.Entity<Group>(entity =>
        {
            entity.HasKey(e => e.GroupId).HasName("PK__Groups__149AF30AEE432155");

            entity.HasOne(d => d.Curator).WithMany(p => p.Groups)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Group_Curator");

            entity.HasOne(d => d.Specialty).WithMany(p => p.Groups)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Group_Specialty");
        });

        modelBuilder.Entity<MarksReport>(entity =>
        {
            entity.HasKey(e => e.ReportId).HasName("PK__MarksRep__D5BD48E571904205");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Pair).WithMany(p => p.MarksReports)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MarksReport_Pair");
        });

        modelBuilder.Entity<MarksReportDetail>(entity =>
        {
            entity.HasKey(e => e.ReportDetailId).HasName("PK__MarksRep__783429E193FA5F18");

            entity.HasOne(d => d.Report).WithMany(p => p.MarksReportDetails)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MarksReportDetail_Report");

            entity.HasOne(d => d.Student).WithMany(p => p.MarksReportDetails)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MarksReportDetail_Student");
        });

        modelBuilder.Entity<Pair>(entity =>
        {
            entity.HasKey(e => e.PairId).HasName("PK__Pairs__B543F62C8ED17802");

            entity.HasOne(d => d.Group).WithMany(p => p.Pairs)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Pairs_Group");

            entity.HasOne(d => d.TeacherSubject).WithMany(p => p.Pairs)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Pairs_TeacherSubject");
        });

        modelBuilder.Entity<Position>(entity =>
        {
            entity.HasKey(e => e.PositionId).HasName("PK__Position__60BB9A593FDEDA08");

            entity.HasOne(d => d.Department).WithMany(p => p.Positions)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Employee_Department");
        });

        modelBuilder.Entity<RegistrationCode>(entity =>
        {
            entity.HasKey(e => e.Code).HasName("PK__Registra__A25C5AA6A39EE829");

            entity.Property(e => e.IsUsed).HasDefaultValue(false);
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RolesId).HasName("PK__Roles__C4B278405123492F");
        });

        modelBuilder.Entity<Room>(entity =>
        {
            entity.HasKey(e => e.RoomId).HasName("PK__Rooms__32863919B4FAEC51");

            entity.HasOne(d => d.Building).WithMany(p => p.Rooms).HasConstraintName("FK_Room_Building");
        });

        modelBuilder.Entity<Schedule>(entity =>
        {
            entity.HasKey(e => e.ScheduleId).HasName("PK__Schedule__9C8A5B6952D67B72");

            entity.HasOne(d => d.ClassType).WithMany(p => p.Schedules)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Schedules_ClassType");

            entity.HasOne(d => d.Day).WithMany(p => p.Schedules)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Schedules_Day");

            entity.HasOne(d => d.Pair).WithMany(p => p.Schedules)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Schedules_Pair");

            entity.HasOne(d => d.Room).WithMany(p => p.Schedules)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Schedules_Room");

            entity.HasOne(d => d.TimeSlot).WithMany(p => p.Schedules)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Schedules_TimeSlot");
        });

        modelBuilder.Entity<Specialty>(entity =>
        {
            entity.HasKey(e => e.SpecialtyId).HasName("PK__Specialt__D768F64828C54965");
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(e => e.StudentId).HasName("PK__Students__32C52A798AE9B60E");

            entity.HasOne(d => d.Group).WithMany(p => p.Students).HasConstraintName("FK_Student_Group");

            entity.HasOne(d => d.User).WithMany(p => p.Students)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Student_User");
        });

        modelBuilder.Entity<Subject>(entity =>
        {
            entity.HasKey(e => e.SubjectId).HasName("PK__Subjects__AC1BA38870F31770");
        });

        modelBuilder.Entity<Teacher>(entity =>
        {
            entity.HasKey(e => e.TeacherId).HasName("PK__Teachers__EDF25964A0D03982");

            entity.HasOne(d => d.CycleCommission).WithMany(p => p.Teachers).HasConstraintName("FK_Teacher_CycleCommission");

            entity.HasOne(d => d.User).WithMany(p => p.Teachers).HasConstraintName("FK_Teacher_User");
        });

        modelBuilder.Entity<TeacherSubject>(entity =>
        {
            entity.HasKey(e => e.PareId).HasName("PK__TeacherS__609680B0A63A5CBC");

            entity.HasOne(d => d.Subject).WithMany(p => p.TeacherSubjects).HasConstraintName("FK_TeacherSubject_Subject");

            entity.HasOne(d => d.Teacher).WithMany(p => p.TeacherSubjects).HasConstraintName("FK_TeacherSubject_Teacher");
        });

        modelBuilder.Entity<TimeSlot>(entity =>
        {
            entity.HasKey(e => e.TimeSlotId).HasName("PK__TimeSlot__41CC1F52C8E6457C");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CCAC05DABBB5");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.PhotoPath).HasDefaultValue("~/DigitalPortalAcademy.user.jpg");

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_User_Role");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
