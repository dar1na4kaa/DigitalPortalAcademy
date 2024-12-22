using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DigitalPortalAcademy.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CycleCommissions",
                columns: table => new
                {
                    CycleCommissionID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CommissionName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__CycleCom__9F8301B2DC322E13", x => x.CycleCommissionID);
                });

            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    DepartmentID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DepartmentName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    HeadOfDepartment = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Departme__B2079BCD98B0068E", x => x.DepartmentID);
                });

            migrationBuilder.CreateTable(
                name: "Specialties",
                columns: table => new
                {
                    SpecialtyID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SpecialtyName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Specialt__D768F6485CDEFD27", x => x.SpecialtyID);
                });

            migrationBuilder.CreateTable(
                name: "Subjects",
                columns: table => new
                {
                    SubjectID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SubjectName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Subjects__AC1BA38848D7D301", x => x.SubjectID);
                });

            migrationBuilder.CreateTable(
                name: "UniqueCodes",
                columns: table => new
                {
                    UniqueCodeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UniqueCode = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IsUsed = table.Column<bool>(type: "bit", nullable: true, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__UniqueCo__4D0875247BDABA20", x => x.UniqueCodeID);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PhotoPath = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, defaultValue: "~/DigitalPortalAcademy.user.jpg"),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Role = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Users__1788CCAC902B8927", x => x.UserID);
                });

            migrationBuilder.CreateTable(
                name: "Curators",
                columns: table => new
                {
                    CuratorID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<int>(type: "int", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    MiddleName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    DepartmentID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Curators__FC8BDC7AD72B1E07", x => x.CuratorID);
                    table.ForeignKey(
                        name: "FK_Curator_Department",
                        column: x => x.DepartmentID,
                        principalTable: "Departments",
                        principalColumn: "DepartmentID");
                    table.ForeignKey(
                        name: "FK_Curator_User",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID");
                });

            migrationBuilder.CreateTable(
                name: "Curriculums",
                columns: table => new
                {
                    CurriculumID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Course = table.Column<int>(type: "int", nullable: false),
                    SpecialtyID = table.Column<int>(type: "int", nullable: true),
                    PlanFile = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    UploadedBy = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Curricul__06C9FA7CA07706F1", x => x.CurriculumID);
                    table.ForeignKey(
                        name: "FK_Curriculum_Uploader",
                        column: x => x.UploadedBy,
                        principalTable: "Users",
                        principalColumn: "UserID");
                    table.ForeignKey(
                        name: "FK_Curriculums_Specialty",
                        column: x => x.SpecialtyID,
                        principalTable: "Specialties",
                        principalColumn: "SpecialtyID");
                });

            migrationBuilder.CreateTable(
                name: "Teachers",
                columns: table => new
                {
                    TeacherID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<int>(type: "int", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    MiddleName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    CycleCommissionId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Teachers__EDF25944490F7BBF", x => x.TeacherID);
                    table.ForeignKey(
                        name: "FK_Teacher_CycleCommission",
                        column: x => x.CycleCommissionId,
                        principalTable: "CycleCommissions",
                        principalColumn: "CycleCommissionID");
                    table.ForeignKey(
                        name: "FK_Teacher_User",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID");
                });

            migrationBuilder.CreateTable(
                name: "Groups",
                columns: table => new
                {
                    GroupID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GroupName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CuratorID = table.Column<int>(type: "int", nullable: false),
                    CourseNumber = table.Column<int>(type: "int", nullable: false),
                    SpecialtyID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Groups__149AF30A27153481", x => x.GroupID);
                    table.ForeignKey(
                        name: "FK_Group_Curator",
                        column: x => x.CuratorID,
                        principalTable: "Curators",
                        principalColumn: "CuratorID");
                    table.ForeignKey(
                        name: "FK_Group_Specialty",
                        column: x => x.SpecialtyID,
                        principalTable: "Specialties",
                        principalColumn: "SpecialtyID");
                });

            migrationBuilder.CreateTable(
                name: "TeacherSubjects",
                columns: table => new
                {
                    TeacherID = table.Column<int>(type: "int", nullable: false),
                    SubjectID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__TeacherS__7733E37C151217F6", x => new { x.TeacherID, x.SubjectID });
                    table.ForeignKey(
                        name: "FK_TeacherSubjects_Subject",
                        column: x => x.SubjectID,
                        principalTable: "Subjects",
                        principalColumn: "SubjectID");
                    table.ForeignKey(
                        name: "FK_TeacherSubjects_Teacher",
                        column: x => x.TeacherID,
                        principalTable: "Teachers",
                        principalColumn: "TeacherID");
                });

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    StudentID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<int>(type: "int", nullable: true),
                    GroupID = table.Column<int>(type: "int", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    MiddleName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    PassportNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    PassportIssuer = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    AttestatNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    ParentName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    ParentPhone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Students__32C52A792E4F2EF3", x => x.StudentID);
                    table.ForeignKey(
                        name: "FK_Student_Group",
                        column: x => x.GroupID,
                        principalTable: "Groups",
                        principalColumn: "GroupID");
                    table.ForeignKey(
                        name: "FK_Student_User",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID");
                });

            migrationBuilder.CreateTable(
                name: "Marks",
                columns: table => new
                {
                    MarkID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentID = table.Column<int>(type: "int", nullable: true),
                    SubjectID = table.Column<int>(type: "int", nullable: true),
                    Mark = table.Column<int>(type: "int", nullable: true),
                    Absences = table.Column<int>(type: "int", nullable: true, defaultValue: 0),
                    Date = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Marks__4E30D346EF10E840", x => x.MarkID);
                    table.ForeignKey(
                        name: "FK_Marks_Student",
                        column: x => x.StudentID,
                        principalTable: "Students",
                        principalColumn: "StudentID");
                    table.ForeignKey(
                        name: "FK_Marks_Subject",
                        column: x => x.SubjectID,
                        principalTable: "Subjects",
                        principalColumn: "SubjectID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Curators_DepartmentID",
                table: "Curators",
                column: "DepartmentID");

            migrationBuilder.CreateIndex(
                name: "IX_Curators_UserID",
                table: "Curators",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Curriculums_SpecialtyID",
                table: "Curriculums",
                column: "SpecialtyID");

            migrationBuilder.CreateIndex(
                name: "IX_Curriculums_UploadedBy",
                table: "Curriculums",
                column: "UploadedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Groups_CuratorID",
                table: "Groups",
                column: "CuratorID");

            migrationBuilder.CreateIndex(
                name: "IX_Groups_SpecialtyID",
                table: "Groups",
                column: "SpecialtyID");

            migrationBuilder.CreateIndex(
                name: "IX_Marks_StudentID",
                table: "Marks",
                column: "StudentID");

            migrationBuilder.CreateIndex(
                name: "IX_Marks_SubjectID",
                table: "Marks",
                column: "SubjectID");

            migrationBuilder.CreateIndex(
                name: "IX_Students_GroupID",
                table: "Students",
                column: "GroupID");

            migrationBuilder.CreateIndex(
                name: "IX_Students_UserID",
                table: "Students",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Teachers_CycleCommissionId",
                table: "Teachers",
                column: "CycleCommissionId");

            migrationBuilder.CreateIndex(
                name: "IX_Teachers_UserID",
                table: "Teachers",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_TeacherSubjects_SubjectID",
                table: "TeacherSubjects",
                column: "SubjectID");

            migrationBuilder.CreateIndex(
                name: "UQ__UniqueCo__BB96DE6FDD60D19A",
                table: "UniqueCodes",
                column: "UniqueCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ__Users__A9D105343915BEBC",
                table: "Users",
                column: "Email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Curriculums");

            migrationBuilder.DropTable(
                name: "Marks");

            migrationBuilder.DropTable(
                name: "TeacherSubjects");

            migrationBuilder.DropTable(
                name: "UniqueCodes");

            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "Subjects");

            migrationBuilder.DropTable(
                name: "Teachers");

            migrationBuilder.DropTable(
                name: "Groups");

            migrationBuilder.DropTable(
                name: "CycleCommissions");

            migrationBuilder.DropTable(
                name: "Curators");

            migrationBuilder.DropTable(
                name: "Specialties");

            migrationBuilder.DropTable(
                name: "Departments");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
