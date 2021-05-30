using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace AppStudentiData
{
    public partial class appstudentiContext : DbContext
    {
        public appstudentiContext(DbContextOptions<appstudentiContext> options)
            : base(options)
        {

        }

        public virtual DbSet<Course> Course { get; set; }
        public virtual DbSet<Province> Province { get; set; }
        public virtual DbSet<Student> Student { get; set; }
        public virtual DbSet<Teacher> Teacher { get; set; }
        public virtual DbSet<Workshop> Workshop { get; set; }
        public virtual DbSet<WorkshopDetail> WorkshopDetail { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=appstudenti;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Course>(entity =>
            {
                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<Province>(entity =>
            {
                entity.Property(e => e.ProvinceId)
                    .HasMaxLength(3)
                    .ValueGeneratedNever();

                entity.Property(e => e.ProvinceName)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<Student>(entity =>
            {
                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Cap)
                    .IsRequired()
                    .HasMaxLength(5);

                entity.Property(e => e.City)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Province)
                    .IsRequired()
                    .HasMaxLength(3);

                entity.Property(e => e.Surname)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<Teacher>(entity =>
            {
                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Cap)
                    .IsRequired()
                    .HasMaxLength(5);

                entity.Property(e => e.City)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Province)
                    .IsRequired()
                    .HasMaxLength(3);

                entity.Property(e => e.Surname)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<Workshop>(entity =>
            {
                entity.Property(e => e.DateFi).HasColumnType("datetime");

                entity.Property(e => e.DateIn).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.Workshop)
                    .HasForeignKey(d => d.CourseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Workshop_Course");

                entity.HasOne(d => d.Teacher)
                    .WithMany(p => p.Workshop)
                    .HasForeignKey(d => d.TeacherId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Workshop_Teacher");
            });

            modelBuilder.Entity<WorkshopDetail>(entity =>
            {
                entity.HasOne(d => d.Student)
                    .WithMany(p => p.WorkshopDetail)
                    .HasForeignKey(d => d.StudentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_WorkshopDetail_Student");

                entity.HasOne(d => d.Workshop)
                    .WithMany(p => p.WorkshopDetail)
                    .HasForeignKey(d => d.WorkshopId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_WorkshopDetail_Workshop");
            });
        }
    }
}
