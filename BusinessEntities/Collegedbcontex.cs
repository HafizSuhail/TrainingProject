using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Stanford_University.BusinessEntities;

public partial class Collegedbcontex : DbContext
{
    public Collegedbcontex()
    {
    }

    public Collegedbcontex(DbContextOptions<Collegedbcontex> options)
        : base(options)
    {
    }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Country> Countries { get; set; }

    public virtual DbSet<Course> Courses { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=DC-MUHAMMEDSUHA;Initial Catalog=College;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PK__Categori__6DB28136160D575A");

            entity.HasIndex(e => e.CategoryName, "UQ__Categori__8517B2E052E3DCC1").IsUnique();

            entity.Property(e => e.CategoryId).HasColumnName("Category_id");
            entity.Property(e => e.CategoryName).HasMaxLength(60);
        });

        modelBuilder.Entity<Country>(entity =>
        {
            entity.HasKey(e => e.CountryId).HasName("PK__Country__7E8CD055CEBF2AA0");

            entity.ToTable("Country");

            entity.HasIndex(e => e.CountryName, "UQ__Country__0756ED8C7EBE8BA5").IsUnique();

            entity.Property(e => e.CountryId).HasColumnName("country_id");
            entity.Property(e => e.CountryName)
                .HasMaxLength(60)
                .HasColumnName("countryName");
        });

        modelBuilder.Entity<Course>(entity =>
        {
            entity.HasKey(e => e.CourseId).HasName("PK__Courses__37E005DB4FD5FC92");

            entity.Property(e => e.CourseId).HasColumnName("Course_Id");
            entity.Property(e => e.Price).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.Title).HasMaxLength(50);
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(e => e.StudentId).HasName("PK__Students__A2F4E98C8B9C2754");

            entity.HasIndex(e => e.RollNo, "UQ__Students__7886D5A10C71A767").IsUnique();

            entity.Property(e => e.StudentId).HasColumnName("Student_Id");
            entity.Property(e => e.CategoryId).HasColumnName("Category_id");
            entity.Property(e => e.CountryId).HasColumnName("country_id");
            entity.Property(e => e.CourseId).HasColumnName("Course_Id");
            entity.Property(e => e.Dob)
                .HasColumnType("date")
                .HasColumnName("DOB");
            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.Gender)
                .HasMaxLength(6)
                .IsUnicode(false);
            entity.Property(e => e.MobileNumber)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.RollNo)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.StudentName).HasMaxLength(100);

            entity.HasOne(d => d.Category).WithMany(p => p.Students)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("FK_Categories");

            entity.HasOne(d => d.Country).WithMany(p => p.Students)
                .HasForeignKey(d => d.CountryId)
                .HasConstraintName("FK_Country");

            entity.HasOne(d => d.Course).WithMany(p => p.Students)
                .HasForeignKey(d => d.CourseId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Courses");

            entity.HasOne(d => d.User).WithMany(p => p.Students)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_Users");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CC4CB222A66C");

            entity.HasIndex(e => e.Password, "UQ__Users__87909B15BD355058").IsUnique();

            entity.HasIndex(e => e.Email, "UQ__Users__A9D10534A849AF73").IsUnique();

            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.Password).HasMaxLength(50);
            entity.Property(e => e.Role).HasMaxLength(25);
            entity.Property(e => e.UserName).HasMaxLength(100);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
