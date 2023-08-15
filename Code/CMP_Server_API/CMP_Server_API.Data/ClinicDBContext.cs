using CMP_Server_API.CMP_Server_API.Data.Models.StaffModels;
using CMP_Server_API.CMP_Server_API.Infra.Services.Utility;
using CMP_Server_API.Models.Clinic_Models;
using CMP_Server_API.Models.StaffModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Data;
using System.Reflection;
using System.Xml.Linq;

namespace CMP_Server_API.CMP.Data
{
    public class ClinicDBContext : DbContext
    {
        private string encpwd = "";
        private readonly Encryptor _encryptor;
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            

            modelBuilder.Entity<Staff>().HasKey(s => new { s.ClinicID, s.StaffID });

            modelBuilder.Entity<StaffView>().ToView("staffview2").HasKey(t => t.StaffID);

            modelBuilder.Entity<Clinic>().HasData(
                    new Clinic
                    {
                        ClinicId = 1,
                        Name = "Pune Clinic",
                        Address = "Magarpatta, Pune-404040",
                        Department = (Department)0,
                        Telephone = 22334422
                    },
                    new Clinic
                    {
                        ClinicId = 2,
                        Name = "Mumbai Clinic",
                        Address = "Thane, Pune-404040",
                        Department = (Department)1,
                        Telephone = 33224422
                    },
                    new Clinic
                    {
                        ClinicId = 3,
                        Name = "Ayush Clinic",
                        Address = "Churchgate, Pune-404040",
                        Department = (Department)2,
                        Telephone = 22443322
                    },
                    new Clinic
                    {
                        ClinicId = 4,
                        Name = "BestCare Clinic",
                        Address = "Hadapsar, Pune-404040",
                        Department = (Department)3,
                        Telephone = 22554433
                    }

                );

            modelBuilder.Entity<Staff>().HasData(
                new Staff
                {
                    StaffID = 1,
                    Password = encpwd,
                    ClinicID = 1,
                    Name = "Piyush Mistry",
                    ShiftTime = (ShiftTime)1,
                    DOB = DateTime.Now.AddYears(-18),
                    Phone = 12341234,
                    Gender = (Gender)0,
                    Role = (Role)1,
                    FileName = "d7570c8a-22e9-4ce9-9827-450f37f35860_images.jpg"
                },

                new Staff
                {
                    StaffID= 2,
                    Password = encpwd,
                    ClinicID = 1,
                    Name = "Mayuri Nehra",
                    ShiftTime = (ShiftTime)0,
                    DOB = DateTime.Now.AddYears(-19),
                    Phone = 123413234,
                    Gender = (Gender)1,
                    Role = (Role)0,
                    FileName = "33ec0650-60a0-4884-bfef-b0fd26c3d723_download.jpg"
                },
                new Staff
                {

                    StaffID= 3,
                    Password = encpwd,
                    ClinicID = 1,
                    Name = "Kedar Mhatre",
                    ShiftTime = (ShiftTime)1,
                    DOB = DateTime.Now.AddYears(-28),
                    Phone = 12341234,
                    Gender = (Gender)1,
                    Role = (Role)2,
                    FileName = "33ec0650-60a0-4884-bfef-b0fd26c3d723_download.jpg"
                },
                new Staff
                {StaffID = 4,
                    Password = encpwd,
                    ClinicID = 1,
                    Name = "Bhushan Mhatre",
                    ShiftTime = (ShiftTime)1,
                    DOB = DateTime.Now.AddYears(-38),
                    Phone = 12341234,
                    Gender = (Gender)1,
                    Role = (Role)2,
                    FileName = "33ec0650-60a0-4884-bfef-b0fd26c3d723_download.jpg"
                },
                new Staff
                {
                    StaffID = 5,
                    Password = encpwd,
                    ClinicID = 2,
                    Name = "Manish Patil",
                    ShiftTime = (ShiftTime)2,
                    DOB = DateTime.Now.AddYears(-38),
                    Phone = 12341234,
                    Gender = (Gender)0,
                    Role = (Role)1,
                    FileName = "d7570c8a-22e9-4ce9-9827-450f37f35860_images.jpg"
                },

                new Staff
                {
                    StaffID = 6,
                    Password = encpwd,
                    ClinicID = 2,
                    Name = "Monika Madane",
                    ShiftTime = (ShiftTime)2,
                    DOB = DateTime.Now.AddYears(-48),
                    Phone = 12341234,
                    Gender = (Gender)1,
                    Role = (Role)0,
                    FileName = "33ec0650-60a0-4884-bfef-b0fd26c3d723_download.jpg"
                },
                new Staff
                {

                    StaffID = 7,
                    Password = encpwd,
                    ClinicID = 2,
                    Name = "Minakshi Mhatre",
                    ShiftTime = (ShiftTime)1,
                    DOB = DateTime.Now.AddYears(-18),
                    Phone = 12341234,
                    Gender = (Gender)1,
                    Role = (Role)1,
                    FileName = "33ec0650-60a0-4884-bfef-b0fd26c3d723_download.jpg"
                },
                new Staff
                {
                    StaffID = 8,
                    Password = encpwd,
                    ClinicID = 2,
                    Name = "Molly Morgan",
                    ShiftTime = (ShiftTime)2,
                    DOB = DateTime.Now.AddYears(-18),
                    Phone = 12341234,
                    Gender = (Gender)1,
                    Role = (Role)1,
                    FileName = "33ec0650-60a0-4884-bfef-b0fd26c3d723_download.jpg"
                }

            );

        }
        public ClinicDBContext(DbContextOptions<DbContext> options, Encryptor encryptor) : base(options) {
            _encryptor= encryptor;
            encpwd = encryptor.encrypt("1234Dasdf");
        }

        public ClinicDBContext() : base() { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=IN01-62RZRQ3\\SQLEXPRESS;Database=CMP;Trusted_Connection=True;MultipleActiveResultSets=true;",
                    builder =>
                    {
                        builder.EnableRetryOnFailure(5, System.TimeSpan.Zero, null);
                    });
                base.OnConfiguring(optionsBuilder);
            }


        }

        public DbSet<Clinic> Clinic { get; set; }

        public DbSet<Staff> Staff { get; set; }

        public DbSet<StaffView> StaffView { get;set;}
    }
}
