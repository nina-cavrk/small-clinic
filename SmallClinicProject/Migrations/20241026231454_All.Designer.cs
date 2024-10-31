﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SmallClinicProject.Models;

#nullable disable

namespace SmallClinicProject.Migrations
{
    [DbContext(typeof(SmallClinicDB))]
    [Migration("20241026231454_All")]
    partial class All
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Finding", b =>
                {
                    b.Property<int>("FindingId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("FindingId"));

                    b.Property<int>("AdmissionId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateTimeOfFinding")
                        .HasColumnType("datetime");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("varchar(500)");

                    b.HasKey("FindingId");

                    b.HasIndex("AdmissionId");

                    b.ToTable("Findings");
                });

            modelBuilder.Entity("PatientsAdmission", b =>
                {
                    b.Property<int>("PatientsAdmissionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PatientsAdmissionId"));

                    b.Property<int>("DoctorId")
                        .HasColumnType("int");

                    b.Property<bool>("EmergencyAdmission")
                        .HasColumnType("bit");

                    b.Property<int>("PatientId")
                        .HasColumnType("int");

                    b.Property<DateTime>("PatientsAdmissionDateTime")
                        .HasColumnType("datetime");

                    b.HasKey("PatientsAdmissionId");

                    b.HasIndex("DoctorId");

                    b.HasIndex("PatientId");

                    b.ToTable("PatientsAdmissions");
                });

            modelBuilder.Entity("SmallClinicProject.Models.CGender", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(15)");

                    b.HasKey("Id");

                    b.ToTable("CGenders");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Muško"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Žensko"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Ostalo"
                        });
                });

            modelBuilder.Entity("SmallClinicProject.Models.CTitle", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(35)");

                    b.HasKey("Id");

                    b.ToTable("CTitles");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Specijalista"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Specijalizant"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Medicinska sestra"
                        });
                });

            modelBuilder.Entity("SmallClinicProject.Models.Doctor", b =>
                {
                    b.Property<int>("DoctorId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DoctorId"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("varchar(35)");

                    b.Property<DateTime?>("DateOfBirth")
                        .IsRequired()
                        .HasColumnType("date");

                    b.Property<string>("DoctorCode")
                        .IsRequired()
                        .HasColumnType("varchar(10)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(35)");

                    b.Property<int>("GenderId")
                        .HasColumnType("int");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("varchar(35)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("varchar(15)");

                    b.Property<int>("TitleId")
                        .HasColumnType("int");

                    b.Property<string>("UniqueIdentificationNumber")
                        .IsRequired()
                        .HasColumnType("varchar(13)");

                    b.HasKey("DoctorId");

                    b.HasIndex("GenderId");

                    b.HasIndex("TitleId");

                    b.ToTable("Doctors");

                    b.HasData(
                        new
                        {
                            DoctorId = 1,
                            Address = "SomePlace1",
                            DateOfBirth = new DateTime(1982, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DoctorCode = "TS01",
                            FirstName = "Tom",
                            GenderId = 1,
                            LastName = "Saywer",
                            PhoneNumber = "38761987789",
                            TitleId = 1,
                            UniqueIdentificationNumber = "010182320187"
                        },
                        new
                        {
                            DoctorId = 2,
                            Address = "SomePlace2",
                            DateOfBirth = new DateTime(1965, 7, 31, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DoctorCode = "JR02",
                            FirstName = "Joanne",
                            GenderId = 2,
                            LastName = "Rowling",
                            PhoneNumber = "38761987780",
                            TitleId = 1,
                            UniqueIdentificationNumber = "310765286815"
                        },
                        new
                        {
                            DoctorId = 3,
                            Address = "SomePlace3",
                            DateOfBirth = new DateTime(1969, 6, 12, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DoctorCode = "AF03",
                            FirstName = "Anna",
                            GenderId = 2,
                            LastName = "Frank",
                            PhoneNumber = "38762987780",
                            TitleId = 1,
                            UniqueIdentificationNumber = "120669175240"
                        },
                        new
                        {
                            DoctorId = 4,
                            Address = "SomePlace4",
                            DateOfBirth = new DateTime(1975, 11, 30, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DoctorCode = "MT04",
                            FirstName = "Mark",
                            GenderId = 1,
                            LastName = "Twain",
                            PhoneNumber = "38762987999",
                            TitleId = 2,
                            UniqueIdentificationNumber = "301175780848"
                        },
                        new
                        {
                            DoctorId = 5,
                            Address = "SomePlace5",
                            DateOfBirth = new DateTime(1990, 12, 10, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DoctorCode = "ED05",
                            FirstName = "Emily",
                            GenderId = 2,
                            LastName = "Dickinson",
                            PhoneNumber = "38762987999",
                            TitleId = 2,
                            UniqueIdentificationNumber = "101290296794"
                        },
                        new
                        {
                            DoctorId = 6,
                            Address = "SomePlace6",
                            DateOfBirth = new DateTime(1982, 1, 27, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DoctorCode = "LC06",
                            FirstName = "Lewis",
                            GenderId = 2,
                            LastName = "Carroll",
                            PhoneNumber = "38762987999",
                            TitleId = 3,
                            UniqueIdentificationNumber = "270182996598"
                        });
                });

            modelBuilder.Entity("SmallClinicProject.Models.Patient", b =>
                {
                    b.Property<int>("PatientId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PatientId"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("date");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("varchar(35)");

                    b.Property<int>("GenderId")
                        .HasColumnType("int");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("varchar(35)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("varchar(15)");

                    b.Property<string>("UniqueIdentificationNumber")
                        .IsRequired()
                        .HasColumnType("varchar(13)");

                    b.HasKey("PatientId");

                    b.HasIndex("GenderId");

                    b.ToTable("Patients");

                    b.HasData(
                        new
                        {
                            PatientId = 1,
                            Address = "Privet Drive",
                            DateOfBirth = new DateTime(1995, 4, 4, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            FirstName = "Harry",
                            GenderId = 1,
                            LastName = "Potter",
                            PhoneNumber = "38762987987",
                            UniqueIdentificationNumber = "040495223049"
                        },
                        new
                        {
                            PatientId = 2,
                            Address = "Hogwarts",
                            DateOfBirth = new DateTime(1987, 4, 13, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            FirstName = "Hermione",
                            GenderId = 2,
                            LastName = "Granger",
                            PhoneNumber = "38761908980",
                            UniqueIdentificationNumber = "130487959072"
                        },
                        new
                        {
                            PatientId = 3,
                            Address = "The Burrow",
                            DateOfBirth = new DateTime(1988, 5, 5, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            FirstName = "Ron",
                            GenderId = 1,
                            LastName = "Weasley",
                            PhoneNumber = "38765897888",
                            UniqueIdentificationNumber = "050588420747"
                        },
                        new
                        {
                            PatientId = 4,
                            Address = "The Burrow",
                            DateOfBirth = new DateTime(1991, 8, 11, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            FirstName = "Ginny",
                            GenderId = 2,
                            LastName = "Weasley",
                            PhoneNumber = "38765432112",
                            UniqueIdentificationNumber = "110891878101"
                        },
                        new
                        {
                            PatientId = 5,
                            Address = "Malfoy Manor",
                            DateOfBirth = new DateTime(1980, 6, 5, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            FirstName = "Draco",
                            GenderId = 1,
                            LastName = "Malfoy",
                            PhoneNumber = "38766789425",
                            UniqueIdentificationNumber = "050680170135"
                        },
                        new
                        {
                            PatientId = 6,
                            Address = "Lovegood House",
                            DateOfBirth = new DateTime(1998, 3, 21, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            FirstName = "Luna",
                            GenderId = 2,
                            LastName = "Lovegood",
                            PhoneNumber = "38761458779",
                            UniqueIdentificationNumber = "210398908123"
                        },
                        new
                        {
                            PatientId = 7,
                            Address = "Longbottom House",
                            DateOfBirth = new DateTime(1980, 7, 30, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            FirstName = "Neville",
                            GenderId = 1,
                            LastName = "Longbottom",
                            PhoneNumber = "38762345879",
                            UniqueIdentificationNumber = "300780807516"
                        });
                });

            modelBuilder.Entity("Finding", b =>
                {
                    b.HasOne("PatientsAdmission", "Admission")
                        .WithMany("Findings")
                        .HasForeignKey("AdmissionId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Admission");
                });

            modelBuilder.Entity("PatientsAdmission", b =>
                {
                    b.HasOne("SmallClinicProject.Models.Doctor", "Doctor")
                        .WithMany("PatientsAdmissions")
                        .HasForeignKey("DoctorId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("SmallClinicProject.Models.Patient", "Patient")
                        .WithMany("PatientsAdmissions")
                        .HasForeignKey("PatientId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Doctor");

                    b.Navigation("Patient");
                });

            modelBuilder.Entity("SmallClinicProject.Models.Doctor", b =>
                {
                    b.HasOne("SmallClinicProject.Models.CGender", "Gender")
                        .WithMany()
                        .HasForeignKey("GenderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SmallClinicProject.Models.CTitle", "Title")
                        .WithMany()
                        .HasForeignKey("TitleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Gender");

                    b.Navigation("Title");
                });

            modelBuilder.Entity("SmallClinicProject.Models.Patient", b =>
                {
                    b.HasOne("SmallClinicProject.Models.CGender", "Gender")
                        .WithMany()
                        .HasForeignKey("GenderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Gender");
                });

            modelBuilder.Entity("PatientsAdmission", b =>
                {
                    b.Navigation("Findings");
                });

            modelBuilder.Entity("SmallClinicProject.Models.Doctor", b =>
                {
                    b.Navigation("PatientsAdmissions");
                });

            modelBuilder.Entity("SmallClinicProject.Models.Patient", b =>
                {
                    b.Navigation("PatientsAdmissions");
                });
#pragma warning restore 612, 618
        }
    }
}
