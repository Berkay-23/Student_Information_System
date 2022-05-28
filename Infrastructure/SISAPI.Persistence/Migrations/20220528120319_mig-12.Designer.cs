﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SISAPI.Persistence.Contexts;

namespace SISAPI.Persistence.Migrations
{
    [DbContext(typeof(SISContext))]
    [Migration("20220528120319_mig-12")]
    partial class mig12
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.15")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("SISAPI.Domain.Entities.Absenteeism", b =>
                {
                    b.Property<DateTime?>("Date")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("date")
                        .HasColumnName("date")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<short>("LessonId")
                        .HasColumnType("smallint");

                    b.Property<string>("Period")
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte>("PracticalAbs")
                        .HasColumnType("tinyint");

                    b.Property<string>("StudentNo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte>("TheoreticalAbs")
                        .HasColumnType("tinyint");

                    b.ToTable("Absenteeism");
                });

            modelBuilder.Entity("SISAPI.Domain.Entities.Academic", b =>
                {
                    b.Property<short>("AcademicianId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("smallint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Department")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Faculty")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Mail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Surname")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("AcademicianId");

                    b.ToTable("Academics");
                });

            modelBuilder.Entity("SISAPI.Domain.Entities.AppUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SurName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("SISAPI.Domain.Entities.Faculty", b =>
                {
                    b.Property<int>("FacultyId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DeanId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("FacultyId");

                    b.ToTable("Faculties");
                });

            modelBuilder.Entity("SISAPI.Domain.Entities.GradeInformation", b =>
                {
                    b.Property<string>("StudentNo")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Gpas")
                        .HasColumnType("nvarchar(max)");

                    b.Property<short>("TotalEcts")
                        .HasColumnType("smallint");

                    b.Property<double>("TranscriptNote")
                        .HasColumnType("float");

                    b.HasKey("StudentNo");

                    b.ToTable("GradeInformations");
                });

            modelBuilder.Entity("SISAPI.Domain.Entities.Lesson", b =>
                {
                    b.Property<short>("LessonId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("smallint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<byte>("Credit")
                        .HasColumnType("tinyint");

                    b.Property<string>("Department")
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte>("Ects")
                        .HasColumnType("tinyint");

                    b.Property<byte>("GradeLevel")
                        .HasColumnType("tinyint");

                    b.Property<short?>("LecturerId")
                        .HasColumnType("smallint");

                    b.Property<string>("LessonCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LessonName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LessonType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte>("PraticalLimit")
                        .HasColumnType("tinyint");

                    b.Property<string>("Semester")
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte>("TheoreticalLimit")
                        .HasColumnType("tinyint");

                    b.HasKey("LessonId");

                    b.ToTable("Lessons");
                });

            modelBuilder.Entity("SISAPI.Domain.Entities.LessonDetails", b =>
                {
                    b.Property<short>("LessonId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("smallint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<float>("AA_startingGrade")
                        .HasColumnType("real");

                    b.Property<float>("BA_startingGrade")
                        .HasColumnType("real");

                    b.Property<float>("BB_startingGrade")
                        .HasColumnType("real");

                    b.Property<float>("CB_startingGrade")
                        .HasColumnType("real");

                    b.Property<float>("CC_startingGrade")
                        .HasColumnType("real");

                    b.Property<bool>("Certainty_of_final")
                        .HasColumnType("bit");

                    b.Property<bool>("Certainty_of_makeup1")
                        .HasColumnType("bit");

                    b.Property<bool>("Certainty_of_makeup2")
                        .HasColumnType("bit");

                    b.Property<bool>("Certainty_of_midterm")
                        .HasColumnType("bit");

                    b.Property<bool>("Certainty_of_percentages")
                        .HasColumnType("bit");

                    b.Property<short>("FinalPercentage")
                        .HasColumnType("smallint");

                    b.Property<short>("MidtermPercentage")
                        .HasColumnType("smallint");

                    b.HasKey("LessonId");

                    b.ToTable("LessonDetails");
                });

            modelBuilder.Entity("SISAPI.Domain.Entities.LessonInformation", b =>
                {
                    b.Property<string>("StudentNo")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RegisteredCourses")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Registration_confirmation")
                        .HasColumnType("bit");

                    b.Property<string>("RetakeFailCourses")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("StudentNo");

                    b.ToTable("LessonInformations");
                });

            modelBuilder.Entity("SISAPI.Domain.Entities.Note", b =>
                {
                    b.Property<int>("NoteId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<double?>("Average")
                        .HasColumnType("float");

                    b.Property<double?>("FinalExam")
                        .HasColumnType("float");

                    b.Property<short>("LessonId")
                        .HasColumnType("smallint");

                    b.Property<double?>("LetterScore")
                        .HasColumnType("float");

                    b.Property<double?>("MakeUpExam1")
                        .HasColumnType("float");

                    b.Property<double?>("MakeUpExam2")
                        .HasColumnType("float");

                    b.Property<double?>("MidtermExam")
                        .HasColumnType("float");

                    b.Property<string>("Period")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool?>("Status")
                        .HasColumnType("bit");

                    b.Property<string>("StudentNo")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("NoteId");

                    b.ToTable("Notes");
                });

            modelBuilder.Entity("SISAPI.Domain.Entities.Student", b =>
                {
                    b.Property<string>("StudentNo")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)")
                        .HasColumnName("student_no");

                    b.Property<short?>("AdvisorId")
                        .HasColumnType("smallint");

                    b.Property<string>("Department")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Faculty")
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte>("GradeLevel")
                        .HasColumnType("tinyint");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Surname")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("StudentNo");

                    b.ToTable("Students");
                });

            modelBuilder.Entity("SISAPI.Domain.Entities.UnsuccessfulStudent", b =>
                {
                    b.Property<short>("LessonId")
                        .HasColumnType("smallint");

                    b.Property<string>("Period")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StudentNo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TypeOfFailure")
                        .HasColumnType("nvarchar(max)");

                    b.ToTable("UnsuccessfulStudents");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("SISAPI.Domain.Entities.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("SISAPI.Domain.Entities.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SISAPI.Domain.Entities.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("SISAPI.Domain.Entities.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
