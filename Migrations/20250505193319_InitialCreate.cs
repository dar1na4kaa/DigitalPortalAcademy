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
                name: "Buildings",
                columns: table => new
                {
                    BuildingID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BuildingName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Building__5463CDE43027C05E", x => x.BuildingID);
                });

            migrationBuilder.CreateTable(
                name: "ClassTypes",
                columns: table => new
                {
                    ClassTypeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClassTypeName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__ClassTyp__9AB2129327A9BCD3", x => x.ClassTypeID);
                });

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
                    table.PrimaryKey("PK__CycleCom__9F8301B2E187801E", x => x.CycleCommissionID);
                });

            migrationBuilder.CreateTable(
                name: "Days",
                columns: table => new
                {
                    DayID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DayName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Days__BF3DD8254089F9F6", x => x.DayID);
                });

            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    DepartmentID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DepartmentName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Departme__B2079BCDD516880D", x => x.DepartmentID);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    RolesId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Roles__C4B278405123492F", x => x.RolesId);
                });

            migrationBuilder.CreateTable(
                name: "Specialties",
                columns: table => new
                {
                    SpecialtyID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SpecialtyName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    SpecialtyCode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Specialt__D768F64828C54965", x => x.SpecialtyID);
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
                    table.PrimaryKey("PK__Subjects__AC1BA38870F31770", x => x.SubjectID);
                });

            migrationBuilder.CreateTable(
                name: "TimeSlots",
                columns: table => new
                {
                    TimeSlotID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartTime = table.Column<TimeOnly>(type: "time", nullable: false),
                    EndTime = table.Column<TimeOnly>(type: "time", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__TimeSlot__41CC1F52C8E6457C", x => x.TimeSlotID);
                });

            migrationBuilder.CreateTable(
                name: "Rooms",
                columns: table => new
                {
                    RoomID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoomName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    BuildingID = table.Column<int>(type: "int", nullable: false),
                    Capacity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Rooms__32863919B4FAEC51", x => x.RoomID);
                    table.ForeignKey(
                        name: "FK_Room_Building",
                        column: x => x.BuildingID,
                        principalTable: "Buildings",
                        principalColumn: "BuildingID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Positions",
                columns: table => new
                {
                    PositionID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DepartmentID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Position__60BB9A593FDEDA08", x => x.PositionID);
                    table.ForeignKey(
                        name: "FK_Employee_Department",
                        column: x => x.DepartmentID,
                        principalTable: "Departments",
                        principalColumn: "DepartmentID");
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PhotoPath = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, defaultValue: "~/DigitalPortalAcademy.user.jpg"),
                    Login = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Users__1788CCAC05DABBB5", x => x.UserID);
                    table.ForeignKey(
                        name: "FK_User_Role",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "RolesId");
                });

            migrationBuilder.CreateTable(
                name: "Curriculums",
                columns: table => new
                {
                    CurriculumID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartYear = table.Column<int>(type: "int", nullable: false),
                    StudyYear = table.Column<int>(type: "int", nullable: false),
                    SpecialtyID = table.Column<int>(type: "int", nullable: true),
                    PlanFilePath = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Curricul__06C9FA7C2F47DBC0", x => x.CurriculumID);
                    table.ForeignKey(
                        name: "FK_Curriculums_Specialty",
                        column: x => x.SpecialtyID,
                        principalTable: "Specialties",
                        principalColumn: "SpecialtyID");
                });

            migrationBuilder.CreateTable(
                name: "Announcements",
                columns: table => new
                {
                    AnnouncementID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AuthorID = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    ExpirationDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Announce__9DE44554F436F3E2", x => x.AnnouncementID);
                    table.ForeignKey(
                        name: "FK_Announcements_Author",
                        column: x => x.AuthorID,
                        principalTable: "Users",
                        principalColumn: "UserID");
                });

            migrationBuilder.CreateTable(
                name: "Employee",
                columns: table => new
                {
                    EmployeeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonnelNumber = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    UserID = table.Column<int>(type: "int", nullable: true),
                    PositionID = table.Column<int>(type: "int", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    MiddleName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    WorkEmail = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Employee__7AD04FF1609D3FF4", x => x.EmployeeID);
                    table.ForeignKey(
                        name: "FK_Employee_Position",
                        column: x => x.PositionID,
                        principalTable: "Positions",
                        principalColumn: "PositionID");
                    table.ForeignKey(
                        name: "FK_Employee_User",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID");
                });

            migrationBuilder.CreateTable(
                name: "Teachers",
                columns: table => new
                {
                    TeacherId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonnelNumber = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    UserID = table.Column<int>(type: "int", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    MiddleName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    WorkEmail = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    CycleCommissionId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Teachers__EDF25964A0D03982", x => x.TeacherId);
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
                    SpecialtyID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Groups__149AF30AEE432155", x => x.GroupID);
                    table.ForeignKey(
                        name: "FK_Group_Curator",
                        column: x => x.CuratorID,
                        principalTable: "Employee",
                        principalColumn: "EmployeeID");
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
                    PareId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TeacherID = table.Column<int>(type: "int", nullable: false),
                    SubjectID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__TeacherS__609680B0A63A5CBC", x => x.PareId);
                    table.ForeignKey(
                        name: "FK_TeacherSubject_Subject",
                        column: x => x.SubjectID,
                        principalTable: "Subjects",
                        principalColumn: "SubjectID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TeacherSubject_Teacher",
                        column: x => x.TeacherID,
                        principalTable: "Teachers",
                        principalColumn: "TeacherId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    StudentID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GroupID = table.Column<int>(type: "int", nullable: false),
                    StudentNumber = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    UserID = table.Column<int>(type: "int", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    MiddleName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    PassportNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    AttestatNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    PersonalEmail = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Students__32C52A798AE9B60E", x => x.StudentID);
                    table.ForeignKey(
                        name: "FK_Student_Group",
                        column: x => x.GroupID,
                        principalTable: "Groups",
                        principalColumn: "GroupID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Student_User",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Pairs",
                columns: table => new
                {
                    PairID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TeacherSubjectID = table.Column<int>(type: "int", nullable: false),
                    GroupID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Pairs__B543F62C8ED17802", x => x.PairID);
                    table.ForeignKey(
                        name: "FK_Pairs_Group",
                        column: x => x.GroupID,
                        principalTable: "Groups",
                        principalColumn: "GroupID");
                    table.ForeignKey(
                        name: "FK_Pairs_TeacherSubject",
                        column: x => x.TeacherSubjectID,
                        principalTable: "TeacherSubjects",
                        principalColumn: "PareId");
                });

            migrationBuilder.CreateTable(
                name: "MarksReports",
                columns: table => new
                {
                    ReportID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PairID = table.Column<int>(type: "int", nullable: false),
                    ReportMonth = table.Column<DateOnly>(type: "date", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__MarksRep__D5BD48E571904205", x => x.ReportID);
                    table.ForeignKey(
                        name: "FK_MarksReport_Pair",
                        column: x => x.PairID,
                        principalTable: "Pairs",
                        principalColumn: "PairID");
                });

            migrationBuilder.CreateTable(
                name: "Schedules",
                columns: table => new
                {
                    ScheduleID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PairID = table.Column<int>(type: "int", nullable: false),
                    RoomID = table.Column<int>(type: "int", nullable: false),
                    DayID = table.Column<int>(type: "int", nullable: false),
                    TimeSlotID = table.Column<int>(type: "int", nullable: false),
                    ClassTypeID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Schedule__9C8A5B6952D67B72", x => x.ScheduleID);
                    table.ForeignKey(
                        name: "FK_Schedules_ClassType",
                        column: x => x.ClassTypeID,
                        principalTable: "ClassTypes",
                        principalColumn: "ClassTypeID");
                    table.ForeignKey(
                        name: "FK_Schedules_Day",
                        column: x => x.DayID,
                        principalTable: "Days",
                        principalColumn: "DayID");
                    table.ForeignKey(
                        name: "FK_Schedules_Pair",
                        column: x => x.PairID,
                        principalTable: "Pairs",
                        principalColumn: "PairID");
                    table.ForeignKey(
                        name: "FK_Schedules_Room",
                        column: x => x.RoomID,
                        principalTable: "Rooms",
                        principalColumn: "RoomID");
                    table.ForeignKey(
                        name: "FK_Schedules_TimeSlot",
                        column: x => x.TimeSlotID,
                        principalTable: "TimeSlots",
                        principalColumn: "TimeSlotID");
                });

            migrationBuilder.CreateTable(
                name: "MarksReportDetails",
                columns: table => new
                {
                    ReportDetailID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReportID = table.Column<int>(type: "int", nullable: false),
                    StudentID = table.Column<int>(type: "int", nullable: false),
                    Mark = table.Column<int>(type: "int", nullable: true),
                    Absences = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__MarksRep__783429E193FA5F18", x => x.ReportDetailID);
                    table.ForeignKey(
                        name: "FK_MarksReportDetail_Report",
                        column: x => x.ReportID,
                        principalTable: "MarksReports",
                        principalColumn: "ReportID");
                    table.ForeignKey(
                        name: "FK_MarksReportDetail_Student",
                        column: x => x.StudentID,
                        principalTable: "Students",
                        principalColumn: "StudentID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Announcements_AuthorID",
                table: "Announcements",
                column: "AuthorID");

            migrationBuilder.CreateIndex(
                name: "UQ__ClassTyp__184FD14328FBF07C",
                table: "ClassTypes",
                column: "ClassTypeName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Curriculums_SpecialtyID",
                table: "Curriculums",
                column: "SpecialtyID");

            migrationBuilder.CreateIndex(
                name: "UQ__Days__04F2C90B79A56CBE",
                table: "Days",
                column: "DayName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ__Departme__D949CC34DC092056",
                table: "Departments",
                column: "DepartmentName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employee_PositionID",
                table: "Employee",
                column: "PositionID");

            migrationBuilder.CreateIndex(
                name: "IX_Employee_UserID",
                table: "Employee",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "UQ__Employee__EC6A9E5CB58100A9",
                table: "Employee",
                column: "PersonnelNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ__Employee_A9D10534231CCD59",
                table: "Employee",
                column: "WorkEmail",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Groups_CuratorID",
                table: "Groups",
                column: "CuratorID");

            migrationBuilder.CreateIndex(
                name: "IX_Groups_SpecialtyID",
                table: "Groups",
                column: "SpecialtyID");

            migrationBuilder.CreateIndex(
                name: "IX_MarksReportDetails_ReportID",
                table: "MarksReportDetails",
                column: "ReportID");

            migrationBuilder.CreateIndex(
                name: "IX_MarksReportDetails_StudentID",
                table: "MarksReportDetails",
                column: "StudentID");

            migrationBuilder.CreateIndex(
                name: "IX_MarksReports_PairID",
                table: "MarksReports",
                column: "PairID");

            migrationBuilder.CreateIndex(
                name: "IX_Pairs_GroupID",
                table: "Pairs",
                column: "GroupID");

            migrationBuilder.CreateIndex(
                name: "IX_Pairs_TeacherSubjectID",
                table: "Pairs",
                column: "TeacherSubjectID");

            migrationBuilder.CreateIndex(
                name: "IX_Positions_DepartmentID",
                table: "Positions",
                column: "DepartmentID");

            migrationBuilder.CreateIndex(
                name: "UQ__Position__737584F6F61E50A3",
                table: "Positions",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ__Roles__737584F6FD469E23",
                table: "Roles",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_BuildingID",
                table: "Rooms",
                column: "BuildingID");

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_ClassTypeID",
                table: "Schedules",
                column: "ClassTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_DayID",
                table: "Schedules",
                column: "DayID");

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_PairID",
                table: "Schedules",
                column: "PairID");

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_RoomID",
                table: "Schedules",
                column: "RoomID");

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_TimeSlotID",
                table: "Schedules",
                column: "TimeSlotID");

            migrationBuilder.CreateIndex(
                name: "UQ__Specialt__739F5275FB9108B9",
                table: "Specialties",
                column: "SpecialtyCode",
                unique: true,
                filter: "[SpecialtyCode] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Students_GroupID",
                table: "Students",
                column: "GroupID");

            migrationBuilder.CreateIndex(
                name: "IX_Students_UserID",
                table: "Students",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "UQ__Students__A9D10534231CCD58",
                table: "Students",
                column: "StudentNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ__Students__A9D10534231CCD59",
                table: "Students",
                column: "PersonalEmail",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Teachers_CycleCommissionId",
                table: "Teachers",
                column: "CycleCommissionId");

            migrationBuilder.CreateIndex(
                name: "IX_Teachers_UserID",
                table: "Teachers",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "UQ__Teachers__A9D10534231CCD58",
                table: "Teachers",
                column: "WorkEmail",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ__Teachers__EC6A9E5C1A09CCDB",
                table: "Teachers",
                column: "PersonnelNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TeacherSubjects_SubjectID",
                table: "TeacherSubjects",
                column: "SubjectID");

            migrationBuilder.CreateIndex(
                name: "UQ_TeacherSubject",
                table: "TeacherSubjects",
                columns: new[] { "TeacherID", "SubjectID" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleId",
                table: "Users",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "UQ__Users__5E55825B45A1EFB3",
                table: "Users",
                column: "Login",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Announcements");

            migrationBuilder.DropTable(
                name: "Curriculums");

            migrationBuilder.DropTable(
                name: "MarksReportDetails");

            migrationBuilder.DropTable(
                name: "Schedules");

            migrationBuilder.DropTable(
                name: "MarksReports");

            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "ClassTypes");

            migrationBuilder.DropTable(
                name: "Days");

            migrationBuilder.DropTable(
                name: "Rooms");

            migrationBuilder.DropTable(
                name: "TimeSlots");

            migrationBuilder.DropTable(
                name: "Pairs");

            migrationBuilder.DropTable(
                name: "Buildings");

            migrationBuilder.DropTable(
                name: "Groups");

            migrationBuilder.DropTable(
                name: "TeacherSubjects");

            migrationBuilder.DropTable(
                name: "Employee");

            migrationBuilder.DropTable(
                name: "Specialties");

            migrationBuilder.DropTable(
                name: "Subjects");

            migrationBuilder.DropTable(
                name: "Teachers");

            migrationBuilder.DropTable(
                name: "Positions");

            migrationBuilder.DropTable(
                name: "CycleCommissions");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Departments");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}
