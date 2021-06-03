using BetaCinemas.Models;
using Microsoft.EntityFrameworkCore;
using System.Configuration;

namespace BetaCinemas.Data.Contexts
{
    public partial class CinemaContext : DbContext
    {
        public CinemaContext()
        {
        }

        public CinemaContext(DbContextOptions<CinemaContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Contact> Contacts { get; set; }
        public virtual DbSet<Movie> Movies { get; set; }
        public virtual DbSet<Post> Posts { get; set; }
        public virtual DbSet<Room> Rooms { get; set; }
        public virtual DbSet<Showtime> Showtimes { get; set; }
        public virtual DbSet<Ticket> Tickets { get; set; }
        public virtual DbSet<Member> Members { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Vietnamese_CI_AS");

            modelBuilder.Entity<Contact>(entity =>
            {
                entity.ToTable("Contact");

                entity.HasIndex(e => e.MemberId, "IX_Contact_MemberId");

                entity.Property(e => e.MemberId).IsRequired();

                entity.Property(e => e.ReplyContent).HasColumnType("ntext");

                entity.Property(e => e.ReplyTime).HasColumnType("datetime");

                entity.Property(e => e.SentContent)
                    .IsRequired()
                    .HasColumnType("ntext");

                entity.Property(e => e.SentTime).HasColumnType("datetime");

                entity.HasOne(d => d.Member)
                    .WithMany(p => p.Contacts)
                    .HasForeignKey(d => d.MemberId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Contact__MemberI__725BF7F6");
            });

            modelBuilder.Entity<Movie>(entity =>
            {
                entity.ToTable("Movie");

                entity.Property(e => e.About).HasColumnType("ntext");

                entity.Property(e => e.Actor).HasColumnType("ntext");

                entity.Property(e => e.Country).HasMaxLength(50);

                entity.Property(e => e.Director).HasMaxLength(50);

                entity.Property(e => e.Genre).HasMaxLength(50);

                entity.Property(e => e.Lang).HasMaxLength(20);

                entity.Property(e => e.PosterUrl).HasColumnType("text");

                entity.Property(e => e.ReleaseDate).HasColumnType("date");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.TrailerUrl).HasColumnType("text");
            });

            modelBuilder.Entity<Post>(entity =>
            {
                entity.ToTable("Post");

                entity.Property(e => e.AttachedUrl).HasColumnType("text");

                entity.Property(e => e.Content)
                    .IsRequired()
                    .HasColumnType("ntext");

                entity.Property(e => e.Genre)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.ImageUrl).HasColumnType("text");

                entity.Property(e => e.PostTime).HasColumnType("datetime");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(250);
            });

            modelBuilder.Entity<Room>(entity =>
            {
                entity.ToTable("Room");

                entity.Property(e => e.About).HasColumnType("ntext");
            });

            modelBuilder.Entity<Showtime>(entity =>
            {
                entity.ToTable("Showtime");

                entity.Property(e => e.Times).HasColumnType("datetime");

                entity.HasOne(d => d.Movie)
                    .WithMany(p => p.Showtimes)
                    .HasForeignKey(d => d.MovieId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Showtime__MovieI__09A971A2");

                entity.HasOne(d => d.Room)
                    .WithMany(p => p.Showtimes)
                    .HasForeignKey(d => d.RoomId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Showtime__RoomId__08B54D69");
            });

            modelBuilder.Entity<Ticket>(entity =>
            {
                entity.ToTable("Ticket");

                entity.HasIndex(e => e.MemberId, "IX_Ticket_MemberId");

                entity.HasIndex(e => e.ShowtimeId, "IX_Ticket_ShowtimeId");

                entity.Property(e => e.MemberId).IsRequired();

                entity.Property(e => e.SoldTime).HasColumnType("datetime");

                entity.HasOne(d => d.Member)
                    .WithMany(p => p.Tickets)
                    .HasForeignKey(d => d.MemberId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Ticket__MemberId__00AA174D");
            });

            modelBuilder.Entity<Member>(entity =>
            {
                entity.HasIndex(e => e.NormalizedEmail, "EmailIndex");

                entity.HasIndex(e => e.NormalizedUserName, "UserNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedUserName] IS NOT NULL)");

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.NormalizedEmail).HasMaxLength(256);

                entity.Property(e => e.NormalizedUserName).HasMaxLength(256);

                entity.Property(e => e.UserName).HasMaxLength(256);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}