using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SIS.Persistence;
using SISAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SISAPI.Persistence.Contexts
{
    public class SISContext : IdentityDbContext<AppUser>
    {
        public SISContext(DbContextOptions<SISContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(Configuration.ConnectionString);
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Absenteeism>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Absenteeism");

                entity.Property(e => e.Date)
                    .HasColumnType("date")
                    .HasColumnName("date")
                    .HasDefaultValueSql("(getdate())");

            });

            modelBuilder.Entity<Academic>(entity =>
            {
                entity.HasKey(e => e.AcademicianId);

            });

            modelBuilder.Entity<GradeInformation>(entity =>
            {
                entity.HasKey(e => e.StudentNo);

            });

            modelBuilder.Entity<Lesson>(entity =>
            {
                entity.HasKey(e => e.LessonId);

            });

            modelBuilder.Entity<LessonInformation>(entity =>
            {
                entity.HasNoKey();

            });

            modelBuilder.Entity<Note>(entity =>
            {
                entity.HasNoKey();

            });

            modelBuilder.Entity<Student>(entity =>
            {
                entity.HasKey(e => e.StudentNo);

                entity.Property(e => e.StudentNo)
                    .HasMaxLength(10)
                    .HasColumnName("student_no");
            });

            modelBuilder.Entity<UnsuccessfulStudent>(entity =>
            {
                entity.HasNoKey();

            });
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Absenteeism> Absenteeisms { get; set; }
        public DbSet<Academic> Academics { get; set; }
        public DbSet<GradeInformation> GradeInformations { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<LessonInformation> LessonInformations { get; set; }
        public DbSet<Note> Notes { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<UnsuccessfulStudent> UnsuccessfulStudents { get; set; }
    }
}
