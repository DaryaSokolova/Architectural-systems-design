using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace DAL
{
    public partial class bakaContext : DbContext
    {
        public bakaContext()
        {
        }

        public bakaContext(DbContextOptions<bakaContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Game> Games { get; set; }
        public virtual DbSet<Session> Sessions { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=DESKTOP-HO5676A\\SQL_EXPRESS;Database=baka;Trusted_Connection=True;Encrypt=True;TrustServerCertificate=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Game>(entity =>
            {
                entity.ToTable("Game");

                entity.Property(e => e.Id).HasColumnName("ID");
            });

            modelBuilder.Entity<Session>(entity =>
            {
                entity.ToTable("Session");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.HasOne(d => d.Game)
                    .WithMany(p => p.Sessions)
                    .HasForeignKey(d => d.GameId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Session__GameId__4F7CD00D");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(500)
                    .IsFixedLength(true);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
