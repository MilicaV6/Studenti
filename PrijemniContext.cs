using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace PrijemniMVC.Models
{
    public partial class PrijemniContext : DbContext
    {
        public PrijemniContext()
        {
        }

        public PrijemniContext(DbContextOptions<PrijemniContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Kurs> Kurs { get; set; } = null!;
        public virtual DbSet<StatusStudenta> StatusStudenta { get; set; } = null!;
        public virtual DbSet<Student> Students { get; set; } = null!;
        public virtual DbSet<StudentSTP> StudentSTPs { get; set; } = null!;
        public virtual DbSet<StudentKurs> StudentKurs { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=Prijemni;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Kurs>(entity =>
            {
                entity.HasKey(e => e.PkKursId);

                entity.Property(e => e.PkKursId).HasColumnName("PK Kurs ID");

                entity.Property(e => e.NazivKursa)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Naziv Kursa");
            });

            modelBuilder.Entity<StatusStudenta>(entity =>
            {
                entity.HasKey(e => e.PkStatusStudentaId);

                entity.ToTable("Status studenta");

                entity.Property(e => e.PkStatusStudentaId).HasColumnName("PK Status Studenta ID");

                entity.Property(e => e.NazivStatusa)
                    .HasMaxLength(10)
                    .HasColumnName("Naziv Statusa")
                    .IsFixedLength();
            });

            modelBuilder.Entity<Student>(entity =>
            {
                entity.HasKey(e => e.PkStudentId);

                entity.ToTable("Student");

                entity.Property(e => e.PkStudentId).HasColumnName("PK Student ID");

                entity.Property(e => e.BrojIndeksa)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Broj indeksa");

                entity.Property(e => e.Ime)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Prezime)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.StatusStudenta).HasColumnName("Status Studenta");

              
            });

            modelBuilder.Entity<StudentKurs>(entity =>
            {
                entity.HasKey(e => new { e.PkStudentId, e.PkKursId });

                entity.Property(e => e.PkStudentId).HasColumnName("PK Student ID");
                entity.HasOne(sk => sk.Student)
                .WithMany(s => s.KurseviStudenta)
                .HasForeignKey(sk => sk.PkStudentId);
                entity.HasOne(sk => sk.Kurs)
                .WithMany(k => k.StudentiNaKursu)
                .HasForeignKey(sk => sk.PkKursId);

                entity.Property(e => e.PkKursId).HasColumnName("PK Kurs ID");
            });

            modelBuilder.Entity<StudentSTP>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.PkStudentId).HasColumnName("PK Student ID");

                entity.Property(e => e.BrojIndeksa)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Broj indeksa");

                entity.Property(e => e.Ime)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Prezime)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PkStatusStudentaId).HasColumnName("PK Status Studenta ID");

                entity.Property(e => e.NazivStatusa)
                    .HasMaxLength(10)
                    .HasColumnName("Naziv Statusa")
                    .IsFixedLength();
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
