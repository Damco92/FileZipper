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
        public virtual DbSet<File> Files { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DocumentTypes>(entity =>
            {
                entity.Property(e => e.Id).IsRequired().ValueGeneratedOnAdd();

                entity.Property(e => e.DocumentName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

            });

            modelBuilder.Entity<File>(entity =>
            {
                entity.Property(e => e.Id).IsRequired().ValueGeneratedOnAdd();

                entity.Property(e => e.Data)
                    .HasMaxLength(8000);
                    

                entity.Property(e => e.FileName).HasMaxLength(200);

                entity.Property(e => e.Created).HasDefaultValue()
                .IsRequired().HasColumnType("SMALLDATETIME");

                entity.Property(e => e.Creator).IsRequired();

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

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Id).IsRequired().ValueGeneratedOnAdd();

                entity.Property(e => e.ZipPassword)
                    .IsRequired()
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Name).IsRequired()
                .HasMaxLength(200).IsUnicode(false);

                entity.Property(e => e.Created).HasDefaultValue()
               .IsRequired().HasColumnType("SMALLDATETIME");

                entity.Property(e => e.Creator).IsRequired();

                entity.Property(e => e.IsAdmin)
                .IsRequired()
                .HasDefaultValue(true);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
