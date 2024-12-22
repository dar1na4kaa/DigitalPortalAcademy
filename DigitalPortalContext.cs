using System;
using System.Collections.Generic;
using DigitalPortalAcademy.Models;
using Microsoft.EntityFrameworkCore;

namespace DigitalPortalAcademy;

public partial class DigitalPortalContext : DbContext
{
    public DigitalPortalContext()
    {
    }

    public DigitalPortalContext(DbContextOptions<DigitalPortalContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Curator> Curators { get; set; }

    public virtual DbSet<Curriculum> Curriculums { get; set; }

    public virtual DbSet<CycleCommission> CycleCommissions { get; set; }

    public virtual DbSet<Department> Departments { get; set; }

    public virtual DbSet<Group> Groups { get; set; }

    public virtual DbSet<Mark> Marks { get; set; }

    public virtual DbSet<Specialty> Specialties { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    public virtual DbSet<Subject> Subjects { get; set; }

    public virtual DbSet<Teacher> Teachers { get; set; }

    public virtual DbSet<UniqueCode> UniqueCodes { get; set; }

    public virtual DbSet<User> Users { get; set; }
    public virtual DbSet<Staff> Staff { get; set; }



    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Server=DESKTOP-6QHTSJ1;Database=DigitalPortal;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
/*        modelBuilder.Entity<Staff>(entity =>
        {
            entity.HasKey(e => e.StaffId);

            entity.Property(e => e.StaffId).HasColumnName("StaffID");
            entity.Property(e => e.FirstName).HasMaxLength(100);
            entity.Property(e => e.LastName).HasMaxLength(100);
            entity.Property(e => e.MiddleName).HasMaxLength(100);
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.User).WithMany(p => p.Staff)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_Staff_User");
        });*/

        modelBuilder.Entity<Curator>(entity =>
        {
            entity.HasKey(e => e.CuratorId).HasName("PK__Curators__FC8BDC7AD72B1E07");

            entity.Property(e => e.CuratorId).HasColumnName("CuratorID");
            entity.Property(e => e.DepartmentId).HasColumnName("DepartmentID");
            entity.Property(e => e.FirstName).HasMaxLength(100);
            entity.Property(e => e.LastName).HasMaxLength(100);
            entity.Property(e => e.MiddleName).HasMaxLength(100);
            entity.Property(e => e.Phone).HasMaxLength(20);
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Department).WithMany(p => p.Curators)
                .HasForeignKey(d => d.DepartmentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Curator_Department");

            entity.HasOne(d => d.User).WithMany(p => p.Curators)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_Curator_User");
        });

        modelBuilder.Entity<Curriculum>(entity =>
        {
            entity.HasKey(e => e.CurriculumId).HasName("PK__Curricul__06C9FA7CA07706F1");

            entity.Property(e => e.CurriculumId).HasColumnName("CurriculumID");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.PlanFile).HasMaxLength(255);
            entity.Property(e => e.SpecialtyId).HasColumnName("SpecialtyID");

            entity.HasOne(d => d.Specialty).WithMany(p => p.Curricula)
                .HasForeignKey(d => d.SpecialtyId)
                .HasConstraintName("FK_Curriculums_Specialty");

            entity.HasOne(d => d.UploadedByNavigation).WithMany(p => p.Curricula)
                .HasForeignKey(d => d.UploadedBy)
                .HasConstraintName("FK_Curriculum_Uploader");
        });

        modelBuilder.Entity<CycleCommission>(entity =>
        {
            entity.HasKey(e => e.CycleCommissionId).HasName("PK__CycleCom__9F8301B2DC322E13");

            entity.Property(e => e.CycleCommissionId).HasColumnName("CycleCommissionID");
            entity.Property(e => e.CommissionName).HasMaxLength(100);
        });

        modelBuilder.Entity<Department>(entity =>
        {
            entity.HasKey(e => e.DepartmentId).HasName("PK__Departme__B2079BCD98B0068E");

            entity.Property(e => e.DepartmentId).HasColumnName("DepartmentID");
            entity.Property(e => e.DepartmentName).HasMaxLength(100);
            entity.Property(e => e.Description).HasMaxLength(100);
            entity.Property(e => e.HeadOfDepartment).HasMaxLength(255);
            entity.Property(e => e.PhoneNumber).HasMaxLength(20);
        });

        modelBuilder.Entity<Group>(entity =>
        {
            entity.HasKey(e => e.GroupId).HasName("PK__Groups__149AF30A27153481");

            entity.Property(e => e.GroupId).HasColumnName("GroupID");
            entity.Property(e => e.CuratorId).HasColumnName("CuratorID");
            entity.Property(e => e.GroupName).HasMaxLength(100);
            entity.Property(e => e.SpecialtyId).HasColumnName("SpecialtyID");

            entity.HasOne(d => d.Curator).WithMany(p => p.Groups)
                .HasForeignKey(d => d.CuratorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Group_Curator");

            entity.HasOne(d => d.Specialty).WithMany(p => p.Groups)
                .HasForeignKey(d => d.SpecialtyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Group_Specialty");
        });

        modelBuilder.Entity<Mark>(entity =>
        {
            entity.HasKey(e => e.MarkId).HasName("PK__Marks__4E30D346EF10E840");

            entity.Property(e => e.MarkId).HasColumnName("MarkID");
            entity.Property(e => e.Absences).HasDefaultValue(0);
            entity.Property(e => e.Date).HasColumnType("datetime");
            entity.Property(e => e.Mark1).HasColumnName("Mark");
            entity.Property(e => e.StudentId).HasColumnName("StudentID");
            entity.Property(e => e.SubjectId).HasColumnName("SubjectID");

            entity.HasOne(d => d.Student).WithMany(p => p.Marks)
                .HasForeignKey(d => d.StudentId)
                .HasConstraintName("FK_Marks_Student");

            entity.HasOne(d => d.Subject).WithMany(p => p.Marks)
                .HasForeignKey(d => d.SubjectId)
                .HasConstraintName("FK_Marks_Subject");
        });

        modelBuilder.Entity<Specialty>(entity =>
        {
            entity.HasKey(e => e.SpecialtyId).HasName("PK__Specialt__D768F6485CDEFD27");

            entity.Property(e => e.SpecialtyId).HasColumnName("SpecialtyID");
            entity.Property(e => e.SpecialtyName).HasMaxLength(255);
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(e => e.StudentId).HasName("PK__Students__32C52A792E4F2EF3");

            entity.Property(e => e.StudentId).HasColumnName("StudentID");
            entity.Property(e => e.Address).HasMaxLength(255);
            entity.Property(e => e.AttestatNumber).HasMaxLength(20);
            entity.Property(e => e.FirstName).HasMaxLength(100);
            entity.Property(e => e.GroupId).HasColumnName("GroupID");
            entity.Property(e => e.LastName).HasMaxLength(100);
            entity.Property(e => e.MiddleName).HasMaxLength(100);
            entity.Property(e => e.ParentName).HasMaxLength(255);
            entity.Property(e => e.ParentPhone).HasMaxLength(20);
            entity.Property(e => e.PassportIssuer).HasMaxLength(100);
            entity.Property(e => e.PassportNumber).HasMaxLength(20);
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Group).WithMany(p => p.Students)
                .HasForeignKey(d => d.GroupId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Student_Group");

            entity.HasOne(d => d.User).WithMany(p => p.Students)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_Student_User");
        });

        modelBuilder.Entity<Subject>(entity =>
        {
            entity.HasKey(e => e.SubjectId).HasName("PK__Subjects__AC1BA38848D7D301");

            entity.Property(e => e.SubjectId).HasColumnName("SubjectID");
            entity.Property(e => e.SubjectName).HasMaxLength(100);
        });

        modelBuilder.Entity<Teacher>(entity =>
        {
            entity.HasKey(e => e.TeacherId).HasName("PK__Teachers__EDF25944490F7BBF");

            entity.Property(e => e.TeacherId).HasColumnName("TeacherID");
            entity.Property(e => e.FirstName).HasMaxLength(100);
            entity.Property(e => e.LastName).HasMaxLength(100);
            entity.Property(e => e.MiddleName).HasMaxLength(100);
            entity.Property(e => e.Phone).HasMaxLength(20);
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.CycleCommission).WithMany(p => p.Teachers)
                .HasForeignKey(d => d.CycleCommissionId)
                .HasConstraintName("FK_Teacher_CycleCommission");

            entity.HasOne(d => d.User).WithMany(p => p.Teachers)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_Teacher_User");

            entity.HasMany(d => d.Subjects).WithMany(p => p.Teachers)
                .UsingEntity<Dictionary<string, object>>(
                    "TeacherSubject",
                    r => r.HasOne<Subject>().WithMany()
                        .HasForeignKey("SubjectId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_TeacherSubjects_Subject"),
                    l => l.HasOne<Teacher>().WithMany()
                        .HasForeignKey("TeacherId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_TeacherSubjects_Teacher"),
                    j =>
                    {
                        j.HasKey("TeacherId", "SubjectId").HasName("PK__TeacherS__7733E37C151217F6");
                        j.ToTable("TeacherSubjects");
                        j.IndexerProperty<int>("TeacherId").HasColumnName("TeacherID");
                        j.IndexerProperty<int>("SubjectId").HasColumnName("SubjectID");
                    });
        });

        modelBuilder.Entity<UniqueCode>(entity =>
        {
            entity.HasKey(e => e.UniqueCodeId).HasName("PK__UniqueCo__4D0875247BDABA20");

            entity.HasIndex(e => e.UniqueCode1, "UQ__UniqueCo__BB96DE6FDD60D19A").IsUnique();

            entity.Property(e => e.UniqueCodeId).HasColumnName("UniqueCodeID");
            entity.Property(e => e.IsUsed).HasDefaultValue(false);
            entity.Property(e => e.UniqueCode1)
                .HasMaxLength(100)
                .HasColumnName("UniqueCode");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CCAC902B8927");

            entity.HasIndex(e => e.Email, "UQ__Users__A9D105343915BEBC").IsUnique();

            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.PasswordHash).HasMaxLength(255);
            entity.Property(e => e.PhotoPath)
                .HasMaxLength(100)
                .HasDefaultValue("~/DigitalPortalAcademy.user.jpg");
            entity.Property(e => e.Role).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
