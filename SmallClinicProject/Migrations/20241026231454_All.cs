using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SmallClinicProject.Migrations
{
    /// <inheritdoc />
    public partial class All : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CGenders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(15)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CGenders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CTitles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(35)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CTitles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Patients",
                columns: table => new
                {
                    PatientId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "varchar(35)", nullable: false),
                    LastName = table.Column<string>(type: "varchar(35)", nullable: false),
                    UniqueIdentificationNumber = table.Column<string>(type: "varchar(13)", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "date", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "varchar(15)", nullable: false),
                    GenderId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patients", x => x.PatientId);
                    table.ForeignKey(
                        name: "FK_Patients_CGenders_GenderId",
                        column: x => x.GenderId,
                        principalTable: "CGenders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Doctors",
                columns: table => new
                {
                    DoctorId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(35)", nullable: false),
                    LastName = table.Column<string>(type: "varchar(35)", nullable: false),
                    UniqueIdentificationNumber = table.Column<string>(type: "varchar(13)", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "date", nullable: false),
                    Address = table.Column<string>(type: "varchar(35)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "varchar(15)", nullable: false),
                    DoctorCode = table.Column<string>(type: "varchar(10)", nullable: false),
                    GenderId = table.Column<int>(type: "int", nullable: false),
                    TitleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Doctors", x => x.DoctorId);
                    table.ForeignKey(
                        name: "FK_Doctors_CGenders_GenderId",
                        column: x => x.GenderId,
                        principalTable: "CGenders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Doctors_CTitles_TitleId",
                        column: x => x.TitleId,
                        principalTable: "CTitles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PatientsAdmissions",
                columns: table => new
                {
                    PatientsAdmissionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PatientId = table.Column<int>(type: "int", nullable: false),
                    DoctorId = table.Column<int>(type: "int", nullable: false),
                    PatientsAdmissionDateTime = table.Column<DateTime>(type: "datetime", nullable: false),
                    EmergencyAdmission = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatientsAdmissions", x => x.PatientsAdmissionId);
                    table.ForeignKey(
                        name: "FK_PatientsAdmissions_Doctors_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "Doctors",
                        principalColumn: "DoctorId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PatientsAdmissions_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "PatientId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Findings",
                columns: table => new
                {
                    FindingId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AdmissionId = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "varchar(500)", nullable: false),
                    DateTimeOfFinding = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Findings", x => x.FindingId);
                    table.ForeignKey(
                        name: "FK_Findings_PatientsAdmissions_AdmissionId",
                        column: x => x.AdmissionId,
                        principalTable: "PatientsAdmissions",
                        principalColumn: "PatientsAdmissionId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "CGenders",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Muško" },
                    { 2, "Žensko" },
                    { 3, "Ostalo" }
                });

            migrationBuilder.InsertData(
                table: "CTitles",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Specijalista" },
                    { 2, "Specijalizant" },
                    { 3, "Medicinska sestra" }
                });

            migrationBuilder.InsertData(
                table: "Doctors",
                columns: new[] { "DoctorId", "Address", "DateOfBirth", "DoctorCode", "FirstName", "GenderId", "LastName", "PhoneNumber", "TitleId", "UniqueIdentificationNumber" },
                values: new object[,]
                {
                    { 1, "SomePlace1", new DateTime(1982, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "TS01", "Tom", 1, "Saywer", "38761987789", 1, "010182320187" },
                    { 2, "SomePlace2", new DateTime(1965, 7, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), "JR02", "Joanne", 2, "Rowling", "38761987780", 1, "310765286815" },
                    { 3, "SomePlace3", new DateTime(1969, 6, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "AF03", "Anna", 2, "Frank", "38762987780", 1, "120669175240" },
                    { 4, "SomePlace4", new DateTime(1975, 11, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "MT04", "Mark", 1, "Twain", "38762987999", 2, "301175780848" },
                    { 5, "SomePlace5", new DateTime(1990, 12, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "ED05", "Emily", 2, "Dickinson", "38762987999", 2, "101290296794" },
                    { 6, "SomePlace6", new DateTime(1982, 1, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), "LC06", "Lewis", 2, "Carroll", "38762987999", 3, "270182996598" }
                });

            migrationBuilder.InsertData(
                table: "Patients",
                columns: new[] { "PatientId", "Address", "DateOfBirth", "FirstName", "GenderId", "LastName", "PhoneNumber", "UniqueIdentificationNumber" },
                values: new object[,]
                {
                    { 1, "Privet Drive", new DateTime(1995, 4, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "Harry", 1, "Potter", "38762987987", "040495223049" },
                    { 2, "Hogwarts", new DateTime(1987, 4, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), "Hermione", 2, "Granger", "38761908980", "130487959072" },
                    { 3, "The Burrow", new DateTime(1988, 5, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ron", 1, "Weasley", "38765897888", "050588420747" },
                    { 4, "The Burrow", new DateTime(1991, 8, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ginny", 2, "Weasley", "38765432112", "110891878101" },
                    { 5, "Malfoy Manor", new DateTime(1980, 6, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Draco", 1, "Malfoy", "38766789425", "050680170135" },
                    { 6, "Lovegood House", new DateTime(1998, 3, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), "Luna", 2, "Lovegood", "38761458779", "210398908123" },
                    { 7, "Longbottom House", new DateTime(1980, 7, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "Neville", 1, "Longbottom", "38762345879", "300780807516" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Doctors_GenderId",
                table: "Doctors",
                column: "GenderId");

            migrationBuilder.CreateIndex(
                name: "IX_Doctors_TitleId",
                table: "Doctors",
                column: "TitleId");

            migrationBuilder.CreateIndex(
                name: "IX_Findings_AdmissionId",
                table: "Findings",
                column: "AdmissionId");

            migrationBuilder.CreateIndex(
                name: "IX_Patients_GenderId",
                table: "Patients",
                column: "GenderId");

            migrationBuilder.CreateIndex(
                name: "IX_PatientsAdmissions_DoctorId",
                table: "PatientsAdmissions",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_PatientsAdmissions_PatientId",
                table: "PatientsAdmissions",
                column: "PatientId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Findings");

            migrationBuilder.DropTable(
                name: "PatientsAdmissions");

            migrationBuilder.DropTable(
                name: "Doctors");

            migrationBuilder.DropTable(
                name: "Patients");

            migrationBuilder.DropTable(
                name: "CTitles");

            migrationBuilder.DropTable(
                name: "CGenders");
        }
    }
}
