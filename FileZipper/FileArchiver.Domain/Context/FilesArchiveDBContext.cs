using FileArchiver.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace FileArchiver.Domain.Context
{
    public partial class FilesArchiveDBContext : DbContext
    {
        public FilesArchiveDBContext()
        {
        }

        public FilesArchiveDBContext(DbContextOptions<FilesArchiveDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<DocumentTypes> DocumentTypes { get; set; }
        public virtual DbSet<Files> Files { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source=QUIDFRVTSKCW01\\SQL2016;Trusted_Connection=True;Database=FilesArchiveDB");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DocumentTypes>(entity =>
            {
                entity.Property(e => e.DocumentName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FileExtensionMask)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("(CONVERT([bit],(0)))");
            });

            modelBuilder.Entity<Files>(entity =>
            {
                entity.HasIndex(e => e.DocumentTypeId);

                entity.HasIndex(e => e.UserId);

                entity.Property(e => e.Created).HasColumnType("smalldatetime");

                entity.Property(e => e.FileName).HasMaxLength(200);

                entity.Property(e => e.UploadNote).IsUnicode(false);

                entity.HasOne(d => d.DocumentType)
                    .WithMany(p => p.Files)
                    .HasForeignKey(d => d.DocumentTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DocumentTypeId");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Files)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserId");
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.Property(e => e.Created).HasColumnType("smalldatetime");

                entity.Property(e => e.IsAdmin)
                    .IsRequired()
                    .HasDefaultValueSql("(CONVERT([bit],(1)))");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ZipPassword)
                    .IsRequired()
                    .HasMaxLength(128)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
