using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace symphonylimited.Models;

public partial class SymphonyContext : DbContext
{
    public SymphonyContext()
    {
    }

    public SymphonyContext(DbContextOptions<SymphonyContext> options)
        : base(options)
    {
    }

    public virtual DbSet<About> Abouts { get; set; }

    public virtual DbSet<Contact> Contacts { get; set; }

    public virtual DbSet<Course> Courses { get; set; }

    public virtual DbSet<EntranceStudent> EntranceStudents { get; set; }

    public virtual DbSet<Exam> Exams { get; set; }

    public virtual DbSet<Fee> Fees { get; set; }

    public virtual DbSet<RegisteredStudent> RegisteredStudents { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=symphony;Integrated Security=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<About>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__about__3213E83F4A9090C8");

            entity.ToTable("about");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Discription).HasColumnName("discription");
            entity.Property(e => e.Image).HasColumnName("image");
            entity.Property(e => e.Title).HasColumnName("title");
        });

        modelBuilder.Entity<Contact>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__contact__3213E83F8FB752DF");

            entity.ToTable("contact");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Address).HasColumnName("address");
            entity.Property(e => e.BranchDetails).HasColumnName("branch_details");
            entity.Property(e => e.Email).HasColumnName("email");
            entity.Property(e => e.Image).HasColumnName("image");
        });

        modelBuilder.Entity<Course>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__courses__3213E83FFE70C623");

            entity.ToTable("courses");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Duration).HasColumnName("duration");
            entity.Property(e => e.Fees)
                .HasMaxLength(50)
                .HasColumnName("fees");
            entity.Property(e => e.Image).HasColumnName("image");
            entity.Property(e => e.InstructorName).HasColumnName("instructor_name");
            entity.Property(e => e.Title).HasColumnName("title");
        });

        modelBuilder.Entity<EntranceStudent>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__entrance__3213E83F1F3E7A52");

            entity.ToTable("entrance_students");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Address).HasColumnName("address");
            entity.Property(e => e.CourseId).HasColumnName("course_id");
            entity.Property(e => e.Email).HasColumnName("email");
            entity.Property(e => e.ExamId).HasColumnName("exam_id");
            entity.Property(e => e.FeeStatus)
                .HasMaxLength(50)
                .HasDefaultValueSql("('pending')")
                .HasColumnName("fee_status");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.PhoneNo)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("phone_no");
            entity.Property(e => e.Result)
                .HasMaxLength(50)
                .HasDefaultValueSql("('pending')")
                .HasColumnName("result");

            entity.HasOne(d => d.Course).WithMany(p => p.EntranceStudents)
                .HasForeignKey(d => d.CourseId)
                .HasConstraintName("FK_entrance_students_ToCourse");

            entity.HasOne(d => d.Exam).WithMany(p => p.EntranceStudents)
                .HasForeignKey(d => d.ExamId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_entrance_students_ToExam");
        });

        modelBuilder.Entity<Exam>(entity =>
        {
            entity.HasKey(e => e.ExamId).HasName("PK__exam__9C8C7BE9E16B1FA1");

            entity.ToTable("exam");

            entity.Property(e => e.ExamId).HasColumnName("exam_id");
            entity.Property(e => e.Date)
                .HasColumnType("date")
                .HasColumnName("date");
            entity.Property(e => e.ResultDate)
                .HasColumnType("date")
                .HasColumnName("result_date");
            entity.Property(e => e.Time).HasColumnName("time");
            entity.Property(e => e.Venue)
                .HasMaxLength(50)
                .HasColumnName("venue");
        });

        modelBuilder.Entity<Fee>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__fees__3213E83F27DE92C5");

            entity.ToTable("fees");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CourseId).HasColumnName("course_id");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasColumnName("status");
            entity.Property(e => e.StdId).HasColumnName("std_id");

            entity.HasOne(d => d.Course).WithMany(p => p.FeesNavigation)
                .HasForeignKey(d => d.CourseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_fees_ToCourse");

            entity.HasOne(d => d.Std).WithMany(p => p.Fees)
                .HasForeignKey(d => d.StdId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_fees_ToRegStudents");
        });

        modelBuilder.Entity<RegisteredStudent>(entity =>
        {
            entity.ToTable("registered_students");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Address).HasColumnName("address");
            entity.Property(e => e.CourseId).HasColumnName("course_id");
            entity.Property(e => e.Email).HasColumnName("email");
            entity.Property(e => e.FeeStatus)
                .HasMaxLength(50)
                .HasDefaultValueSql("('pending')")
                .HasColumnName("fee_status");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.PhoneNo)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("phone_no");

            entity.HasOne(d => d.Course).WithMany(p => p.RegisteredStudents)
                .HasForeignKey(d => d.CourseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_registered_students_ToCourse");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__users__3214EC07C4718072");

            entity.ToTable("users");

            entity.Property(e => e.Email).HasColumnName("email");
            entity.Property(e => e.Password).HasColumnName("password");
            entity.Property(e => e.Username).HasColumnName("username");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
