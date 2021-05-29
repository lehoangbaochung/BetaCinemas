﻿using System.Configuration;
using Microsoft.EntityFrameworkCore;

namespace BetaCinemas.Models
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
        public virtual DbSet<Member> Members { get; set; }
        public virtual DbSet<Movie> Movies { get; set; }
        public virtual DbSet<Post> Posts { get; set; }
        public virtual DbSet<Room> Rooms { get; set; }
        public virtual DbSet<Seat> Seats { get; set; }
        public virtual DbSet<Showtime> Showtimes { get; set; }
        public virtual DbSet<Ticket> Tickets { get; set; }
        public virtual DbSet<TicketPrice> TicketPrices { get; set; }

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

                entity.Property(e => e.SentContent)
                    .IsRequired()
                    .HasColumnType("ntext");

                entity.Property(e => e.MemberId).IsRequired();

                entity.Property(e => e.SentTime).HasColumnType("datetime");

                entity.HasOne(d => d.Member)
                    .WithMany(p => p.Contacts)
                    .HasForeignKey(d => d.MemberId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Contact__MemberI__725BF7F6");
            });

            modelBuilder.Entity<Member>(entity =>
            {
                entity.ToTable("User");
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
            });

            modelBuilder.Entity<Room>(entity =>
            {
                entity.ToTable("Room");

                entity.Property(e => e.About).HasColumnType("ntext");
            });

            modelBuilder.Entity<Seat>(entity =>
            {
                entity.ToTable("Seat");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.RowIndex)
                    .IsRequired()
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.HasOne(d => d.Room)
                    .WithOne(p => p.Seat)
                    .HasForeignKey<Seat>(d => d.Id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Seat__Id__7720AD13");
            });

            modelBuilder.Entity<Showtime>(entity =>
            {
                entity.ToTable("Showtime");

                entity.Property(e => e.ShowTime)
                    .HasColumnType("datetime")
                    .HasColumnName("ShowTime");

                entity.HasOne(d => d.Movie)
                    .WithMany(p => p.Showtimes)
                    .HasForeignKey(d => d.MovieId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Showtime__MovieI__6754599E");

                entity.HasOne(d => d.Room)
                    .WithMany(p => p.Showtimes)
                    .HasForeignKey(d => d.RoomId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Showtime__RoomId__66603565");
            });

            modelBuilder.Entity<Ticket>(entity =>
            {
                entity.ToTable("Ticket");

                entity.HasIndex(e => e.MemberId, "IX_Ticket_MemberId");

                entity.HasIndex(e => e.ShowtimeId, "IX_Ticket_ShowtimeId");

                entity.HasIndex(e => e.TicketPriceId, "IX_Ticket_TicketPriceId");

                entity.HasIndex(e => e.MemberId, "IX_Ticket_UserId");

                entity.Property(e => e.MemberId).IsRequired();

                entity.Property(e => e.SoldTime).HasColumnType("datetime");

                entity.HasOne(d => d.Member)
                    .WithMany(p => p.Tickets)
                    .HasForeignKey(d => d.MemberId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Ticket__MemberId__00AA174D");

                entity.HasOne(d => d.Showtime)
                    .WithMany(p => p.Tickets)
                    .HasForeignKey(d => d.ShowtimeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Ticket__ShowtimeId__019E3B86");

                entity.HasOne(d => d.TicketPrice)
                    .WithMany(p => p.Tickets)
                    .HasForeignKey(d => d.TicketPriceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Ticket__TicketPr__6D0D32F4");
            });

            modelBuilder.Entity<TicketPrice>(entity =>
            {
                entity.ToTable("TicketPrice");

                entity.Property(e => e.DateType)
                    .IsRequired()
                    .HasMaxLength(20);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
