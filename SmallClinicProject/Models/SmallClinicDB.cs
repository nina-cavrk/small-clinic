using Microsoft.EntityFrameworkCore;
using System;

namespace SmallClinicProject.Models
{
    public class SmallClinicDB : DbContext
    {
        public SmallClinicDB(DbContextOptions<SmallClinicDB> options) : base(options) { }
        public DbSet<CGender> CGenders { get; set; }
        public DbSet<CTitle> CTitles { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<PatientsAdmission> PatientsAdmissions { get; set; }
        public DbSet<Finding> Findings { get; set; } // Add Findings table

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<CGender>(entity =>
            {
                entity.Property(e => e.Id)
                      .HasColumnType("int")
                      .IsRequired();

                entity.Property(e => e.Name)
                      .HasColumnType("varchar(15)")
                      .IsRequired();
            });

            modelBuilder.Entity<CTitle>(entity =>
            {
                entity.Property(e => e.Id)
                      .HasColumnType("int")
                      .IsRequired();

                entity.Property(e => e.Name)
                      .HasColumnType("varchar(35)")
                      .IsRequired();
            });

            modelBuilder.Entity<Patient>(entity =>
            {
                entity.Property(e => e.PatientId)
                      .HasColumnType("int")
                      .IsRequired();

                entity.Property(e => e.FirstName)
                      .HasColumnType("varchar(35)")
                      .IsRequired();

                entity.Property(e => e.LastName)
                      .HasColumnType("varchar(35)")
                      .IsRequired();

                entity.Property(e => e.UniqueIdentificationNumber)
                      .HasColumnType("varchar(13)")
                      .IsRequired();

                entity.Property(e => e.DateOfBirth)
                      .HasColumnType("date")
                      .IsRequired();

                entity.Property(e => e.Address)
                      .HasColumnType("nvarchar(50)");

                entity.Property(e => e.PhoneNumber)
                      .HasColumnType("varchar(15)");
            });

            modelBuilder.Entity<Doctor>(entity =>
            {
                entity.Property(e => e.DoctorId)
                      .HasColumnType("int")
                      .IsRequired();

                entity.Property(e => e.FirstName)
                      .HasColumnType("nvarchar(35)")
                      .IsRequired();

                entity.Property(e => e.LastName)
                      .HasColumnType("varchar(35)")
                      .IsRequired();

                entity.Property(e => e.UniqueIdentificationNumber)
                      .HasColumnType("varchar(13)")
                      .IsRequired();

                entity.Property(e => e.DateOfBirth)
                      .HasColumnType("date")
                      .IsRequired();

                entity.Property(e => e.Address)
                      .HasColumnType("varchar(35)");

                entity.Property(e => e.PhoneNumber)
                      .HasColumnType("varchar(15)");

                entity.Property(e => e.DoctorCode)
                      .HasColumnType("varchar(10)")
                      .IsRequired();
            });

            modelBuilder.Entity<PatientsAdmission>(entity =>
            {
                entity.Property(e => e.PatientsAdmissionId)
                      .HasColumnType("int")
                      .IsRequired();

                entity.Property(e => e.PatientsAdmissionDateTime)
                      .HasColumnType("datetime");

                entity.Property(e => e.EmergencyAdmission)
                      .HasColumnType("bit");
            });

            modelBuilder.Entity<Finding>(entity =>
            {
                entity.Property(e => e.FindingId)
                      .HasColumnType("int")
                      .IsRequired();

                entity.Property(e => e.Description)
                      .HasColumnType("varchar(500)")
                      .IsRequired();

                entity.Property(e => e.DateTimeOfFinding)
                      .HasColumnType("datetime")
                      .IsRequired();
            });

            // Relationships for PatientsAdmission
            modelBuilder.Entity<PatientsAdmission>()
                .HasOne(pa => pa.Patient)
                .WithMany(p => p.PatientsAdmissions)
                .HasForeignKey(pa => pa.PatientId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PatientsAdmission>()
                .HasOne(pa => pa.Doctor)
                .WithMany(d => d.PatientsAdmissions)
                .HasForeignKey(pa => pa.DoctorId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Finding>()
                .HasOne(f => f.Admission)
                .WithMany(a => a.Findings)
                .HasForeignKey(f => f.AdmissionId)
                .OnDelete(DeleteBehavior.Restrict);

            SeedGenders(modelBuilder);
            SeedTitles(modelBuilder);
            SeedDoctors(modelBuilder);
            SeedPatients(modelBuilder);
        
    }

        private void SeedGenders(ModelBuilder modelBuilder)
        {
            var genders = new[]
            {
                new CGender { Id = 1, Name = "Muško" },
                new CGender { Id = 2, Name = "Žensko" },
                new CGender { Id = 3, Name = "Ostalo" }
            };

            modelBuilder.Entity<CGender>().HasData(genders);
        }

        private void SeedTitles(ModelBuilder modelBuilder)
        {
            var titles = new[]
            {
                new CTitle { Id = 1, Name = "Specijalista" },
                new CTitle { Id = 2, Name = "Specijalizant" },
                new CTitle { Id = 3, Name = "Medicinska sestra" }
            };

            modelBuilder.Entity<CTitle>().HasData(titles);
        }

        private void SeedDoctors(ModelBuilder modelBuilder)
        {
            var doctors = new[]
            {
                new Doctor
                {
                    DoctorId = 1,
                    FirstName = "Tom",
                    LastName = "Saywer",
                    UniqueIdentificationNumber = GenerateUniqueIdNumber(new DateTime(1982, 1, 1)),
                    DateOfBirth = new DateTime(1982, 1, 1),
                    Address = "SomePlace1",
                    PhoneNumber = "38761987789",
                    GenderId = 1,
                    TitleId = 1,
                    DoctorCode = GenerateDoctorCode("Tom", "Saywer", 1)
                },
                new Doctor
                {
                    DoctorId = 2,
                    FirstName = "Joanne",
                    LastName = "Rowling",
                    UniqueIdentificationNumber = GenerateUniqueIdNumber(new DateTime(1965, 7, 31)),
                    DateOfBirth = new DateTime(1965, 7, 31),
                    Address = "SomePlace2",
                    PhoneNumber = "38761987780",
                    GenderId = 2,
                    TitleId = 1,
                    DoctorCode = GenerateDoctorCode("Joanne", "Rowling", 2)
                },
                new Doctor
                {
                    DoctorId = 3,
                    FirstName = "Anna",
                    LastName = "Frank",
                    UniqueIdentificationNumber = GenerateUniqueIdNumber(new DateTime(1969, 6, 12)),
                    DateOfBirth = new DateTime(1969, 6, 12),
                    Address = "SomePlace3",
                    PhoneNumber = "38762987780",
                    GenderId = 2,
                    TitleId = 1,
                    DoctorCode = GenerateDoctorCode("Anna", "Frank", 3)
                },
                new Doctor
                {
                    DoctorId = 4,
                    FirstName = "Mark",
                    LastName = "Twain",
                    UniqueIdentificationNumber = GenerateUniqueIdNumber(new DateTime(1975, 11, 30)),
                    DateOfBirth = new DateTime(1975, 11, 30),
                    Address = "SomePlace4",
                    PhoneNumber = "38762987999",
                    GenderId = 1,
                    TitleId = 2,
                    DoctorCode = GenerateDoctorCode("Mark", "Twain", 4)
                },
                new Doctor
                {
                    DoctorId = 5,
                    FirstName = "Emily",
                    LastName = "Dickinson",
                    UniqueIdentificationNumber = GenerateUniqueIdNumber(new DateTime(1990, 12, 10)),
                    DateOfBirth = new DateTime(1990, 12, 10),
                    Address = "SomePlace5",
                    PhoneNumber = "38762987999",
                    GenderId = 2,
                    TitleId = 2,
                    DoctorCode = GenerateDoctorCode("Emily", "Dickinson", 5)
                },
                new Doctor
                {
                    DoctorId = 6,
                    FirstName = "Lewis",
                    LastName = "Carroll",
                    UniqueIdentificationNumber = GenerateUniqueIdNumber(new DateTime(1982, 1, 27)),
                    DateOfBirth = new DateTime(1982, 1, 27),
                    Address = "SomePlace6",
                    PhoneNumber = "38762987999",
                    GenderId = 2,
                    TitleId = 3,
                    DoctorCode = GenerateDoctorCode("Lewis", "Carroll", 6)
                }
            };

            modelBuilder.Entity<Doctor>().HasData(doctors);
        }

        private void SeedPatients(ModelBuilder modelBuilder)
        {
            var patients = new[]
            {
                new Patient
                {
                    PatientId = 1,
                    FirstName = "Harry",
                    LastName = "Potter",
                    UniqueIdentificationNumber = GenerateUniqueIdNumber(new DateTime(1995, 4, 4)),
                    DateOfBirth = new DateTime(1995, 4, 4),
                    Address = "Privet Drive",
                    PhoneNumber = "38762987987",
                    GenderId = 1
                },
                new Patient
                {
                    PatientId = 2,
                    FirstName = "Hermione",
                    LastName = "Granger",
                    UniqueIdentificationNumber = GenerateUniqueIdNumber(new DateTime(1987, 4, 13)),
                    DateOfBirth = new DateTime(1987, 4, 13),
                    Address = "Hogwarts",
                    PhoneNumber = "38761908980",
                    GenderId = 2
                },
                new Patient
                {
                    PatientId = 3,
                    FirstName = "Ron",
                    LastName = "Weasley",
                    UniqueIdentificationNumber = GenerateUniqueIdNumber(new DateTime(1988, 5, 5)),
                    DateOfBirth = new DateTime(1988, 5, 5),
                    Address = "The Burrow",
                    PhoneNumber = "38765897888",
                    GenderId = 1
                },
                new Patient
                {
                    PatientId = 4,
                    FirstName = "Ginny",
                    LastName = "Weasley",
                    UniqueIdentificationNumber = GenerateUniqueIdNumber(new DateTime(1991, 8, 11)),
                    DateOfBirth = new DateTime(1991, 8, 11),
                    Address = "The Burrow",
                    PhoneNumber = "38765432112",
                    GenderId = 2
                },
                new Patient
                {
                    PatientId = 5,
                    FirstName = "Draco",
                    LastName = "Malfoy",
                    UniqueIdentificationNumber = GenerateUniqueIdNumber(new DateTime(1980, 6, 5)),
                    DateOfBirth = new DateTime(1980, 6, 5),
                    Address = "Malfoy Manor",
                    PhoneNumber = "38766789425",
                    GenderId = 1
                },
                new Patient
                {
                    PatientId = 6,
                    FirstName = "Luna",
                    LastName = "Lovegood",
                    UniqueIdentificationNumber = GenerateUniqueIdNumber(new DateTime(1998, 3, 21)),
                    DateOfBirth = new DateTime(1998, 3, 21),
                    Address = "Lovegood House",
                    PhoneNumber = "38761458779",
                    GenderId = 2
                },
                new Patient
                {
                    PatientId = 7,
                    FirstName = "Neville",
                    LastName = "Longbottom",
                    UniqueIdentificationNumber = GenerateUniqueIdNumber(new DateTime(1980, 7, 30)),
                    DateOfBirth = new DateTime(1980, 7, 30),
                    Address = "Longbottom House",
                    PhoneNumber = "38762345879",
                    GenderId = 1
                }
            };

            modelBuilder.Entity<Patient>().HasData(patients);
        }

        private static readonly Random random = new Random();

        private string GenerateUniqueIdNumber(DateTime dateOfBirth)
        {
            //keep leading zeros
           string dobPart = dateOfBirth.ToString("ddMMyy");

           string randomPart = random.Next(100000, 999999).ToString();
   
           return $"{dobPart}{randomPart}";
        }

        private string GenerateDoctorCode(string firstName, string lastName, int serialNumber)
        {
            return $"{firstName[0]}{lastName[0]}{serialNumber:D2}";
        }
    }
}
