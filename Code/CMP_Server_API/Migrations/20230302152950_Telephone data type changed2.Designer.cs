﻿// <auto-generated />
using System;
using CMP_Server_API.CMP.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CMP_Server_API.Migrations
{
    [DbContext(typeof(ClinicDBContext))]
    [Migration("20230302152950_Telephone data type changed2")]
    partial class Telephonedatatypechanged2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.0");

            modelBuilder.Entity("CMP_Server_API.CMP_Server_API.Data.Models.StaffModels.StaffView", b =>
                {
                    b.Property<int>("StaffID")
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("ClinicID")
                        .HasColumnType("int");

                    b.Property<DateTime>("DOB")
                        .HasColumnType("datetime2");

                    b.Property<int>("Gender")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Phone")
                        .HasColumnType("int");

                    b.Property<int>("Role")
                        .HasColumnType("int");

                    b.Property<int>("ShiftTime")
                        .HasColumnType("int");

                    b.Property<string>("fileName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("StaffID");

                    b.ToView("staffview2");
                });

            modelBuilder.Entity("CMP_Server_API.Models.Clinic_Models.Clinic", b =>
                {
                    b.Property<int>("ClinicId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Department")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("Telephone")
                        .HasColumnType("bigint");

                    b.HasKey("ClinicId");

                    b.ToTable("Clinic");

                    b.HasData(
                        new
                        {
                            ClinicId = 1,
                            Address = "Magarpatta, Pune-404040",
                            Department = 0,
                            Name = "Pune Clinic",
                            Telephone = 22334422L
                        },
                        new
                        {
                            ClinicId = 2,
                            Address = "Thane, Pune-404040",
                            Department = 1,
                            Name = "Mumbai Clinic",
                            Telephone = 33224422L
                        },
                        new
                        {
                            ClinicId = 3,
                            Address = "Churchgate, Pune-404040",
                            Department = 2,
                            Name = "Ayush Clinic",
                            Telephone = 22443322L
                        },
                        new
                        {
                            ClinicId = 4,
                            Address = "Hadapsar, Pune-404040",
                            Department = 3,
                            Name = "BestCare Clinic",
                            Telephone = 22554433L
                        });
                });

            modelBuilder.Entity("CMP_Server_API.Models.StaffModels.Staff", b =>
                {
                    b.Property<int>("ClinicID")
                        .HasColumnType("int");

                    b.Property<int>("StaffID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<DateTime>("DOB")
                        .HasColumnType("datetime2");

                    b.Property<string>("FileName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Gender")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("Phone")
                        .HasColumnType("bigint");

                    b.Property<int>("Role")
                        .HasColumnType("int");

                    b.Property<int>("ShiftTime")
                        .HasColumnType("int");

                    b.HasKey("ClinicID", "StaffID");

                    b.ToTable("Staff");

                    b.HasData(
                        new
                        {
                            ClinicID = 1,
                            StaffID = 1,
                            DOB = new DateTime(2005, 3, 2, 20, 59, 50, 561, DateTimeKind.Local).AddTicks(3929),
                            FileName = "d7570c8a-22e9-4ce9-9827-450f37f35860_images.jpg",
                            Gender = 0,
                            Name = "Piyush Mistry",
                            Password = "",
                            Phone = 12341234L,
                            Role = 1,
                            ShiftTime = 1
                        },
                        new
                        {
                            ClinicID = 1,
                            StaffID = 2,
                            DOB = new DateTime(2004, 3, 2, 20, 59, 50, 562, DateTimeKind.Local).AddTicks(2921),
                            FileName = "33ec0650-60a0-4884-bfef-b0fd26c3d723_download.jpg",
                            Gender = 1,
                            Name = "Mayuri Nehra",
                            Password = "",
                            Phone = 123413234L,
                            Role = 0,
                            ShiftTime = 0
                        },
                        new
                        {
                            ClinicID = 1,
                            StaffID = 3,
                            DOB = new DateTime(1995, 3, 2, 20, 59, 50, 562, DateTimeKind.Local).AddTicks(2973),
                            FileName = "33ec0650-60a0-4884-bfef-b0fd26c3d723_download.jpg",
                            Gender = 1,
                            Name = "Kedar Mhatre",
                            Password = "",
                            Phone = 12341234L,
                            Role = 2,
                            ShiftTime = 1
                        },
                        new
                        {
                            ClinicID = 1,
                            StaffID = 4,
                            DOB = new DateTime(1985, 3, 2, 20, 59, 50, 562, DateTimeKind.Local).AddTicks(2976),
                            FileName = "33ec0650-60a0-4884-bfef-b0fd26c3d723_download.jpg",
                            Gender = 1,
                            Name = "Bhushan Mhatre",
                            Password = "",
                            Phone = 12341234L,
                            Role = 2,
                            ShiftTime = 1
                        },
                        new
                        {
                            ClinicID = 2,
                            StaffID = 5,
                            DOB = new DateTime(1985, 3, 2, 20, 59, 50, 562, DateTimeKind.Local).AddTicks(2978),
                            FileName = "d7570c8a-22e9-4ce9-9827-450f37f35860_images.jpg",
                            Gender = 0,
                            Name = "Manish Patil",
                            Password = "",
                            Phone = 12341234L,
                            Role = 1,
                            ShiftTime = 2
                        },
                        new
                        {
                            ClinicID = 2,
                            StaffID = 6,
                            DOB = new DateTime(1975, 3, 2, 20, 59, 50, 562, DateTimeKind.Local).AddTicks(2979),
                            FileName = "33ec0650-60a0-4884-bfef-b0fd26c3d723_download.jpg",
                            Gender = 1,
                            Name = "Monika Madane",
                            Password = "",
                            Phone = 12341234L,
                            Role = 0,
                            ShiftTime = 2
                        },
                        new
                        {
                            ClinicID = 2,
                            StaffID = 7,
                            DOB = new DateTime(2005, 3, 2, 20, 59, 50, 562, DateTimeKind.Local).AddTicks(2981),
                            FileName = "33ec0650-60a0-4884-bfef-b0fd26c3d723_download.jpg",
                            Gender = 1,
                            Name = "Minakshi Mhatre",
                            Password = "",
                            Phone = 12341234L,
                            Role = 1,
                            ShiftTime = 1
                        },
                        new
                        {
                            ClinicID = 2,
                            StaffID = 8,
                            DOB = new DateTime(2005, 3, 2, 20, 59, 50, 562, DateTimeKind.Local).AddTicks(2983),
                            FileName = "33ec0650-60a0-4884-bfef-b0fd26c3d723_download.jpg",
                            Gender = 1,
                            Name = "Molly Morgan",
                            Password = "",
                            Phone = 12341234L,
                            Role = 1,
                            ShiftTime = 2
                        });
                });

            modelBuilder.Entity("CMP_Server_API.Models.StaffModels.Staff", b =>
                {
                    b.HasOne("CMP_Server_API.Models.Clinic_Models.Clinic", "Clinic")
                        .WithMany()
                        .HasForeignKey("ClinicID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Clinic");
                });
#pragma warning restore 612, 618
        }
    }
}
