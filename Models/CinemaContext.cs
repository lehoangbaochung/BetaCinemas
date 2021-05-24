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

        public virtual DbSet<Bill> Bills { get; set; }
        public virtual DbSet<BillDetail> BillDetails { get; set; }
        public virtual DbSet<Contact> Contacts { get; set; }
        public virtual DbSet<Movie> Movies { get; set; }
        public virtual DbSet<Room> Rooms { get; set; }
        public virtual DbSet<Seat> Seats { get; set; }
        public virtual DbSet<Showtime> Showtimes { get; set; }
        public virtual DbSet<Ticket> Tickets { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS;Database=Cinema;Trusted_Connection=True;MultipleActiveResultSets=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Vietnamese_CI_AS");

            modelBuilder.Entity<Bill>(entity =>
            {
                entity.ToTable("Bill");

                entity.HasIndex(e => e.MemberId, "IX_Bill_MemberId");

                entity.Property(e => e.MemberId).IsRequired();

                entity.HasOne(d => d.Member)
                    .WithMany(p => p.Bills)
                    .HasForeignKey(d => d.MemberId);
            });

            modelBuilder.Entity<BillDetail>(entity =>
            {
                entity.ToTable("BillDetail");

                entity.HasIndex(e => e.BillId, "IX_BillDetail_BillId");

                entity.HasOne(d => d.Bill)
                    .WithMany(p => p.BillDetails)
                    .HasForeignKey(d => d.BillId);
            });

            modelBuilder.Entity<Contact>(entity =>
            {
                entity.ToTable("Contact");

                entity.HasIndex(e => e.MemberId, "IX_Contact_MemberId");

                entity.Property(e => e.MemberId).IsRequired();

                entity.HasOne(d => d.Member)
                    .WithMany(p => p.Contacts)
                    .HasForeignKey(d => d.MemberId);
            });

            modelBuilder.Entity<Movie>(entity =>
            {
                entity.ToTable("Movie");
            });

            modelBuilder.Entity<Room>(entity =>
            {
                entity.ToTable("Room");
            });

            modelBuilder.Entity<Seat>(entity =>
            {
                entity.ToTable("Seat");

                entity.HasIndex(e => e.RoomId, "IX_Seat_RoomId")
                    .IsUnique();

                entity.HasOne(d => d.Room)
                    .WithOne(p => p.Seat)
                    .HasForeignKey<Seat>(d => d.RoomId);
            });

            modelBuilder.Entity<Showtime>(entity =>
            {
                entity.ToTable("Showtime");

                entity.HasIndex(e => e.RoomId, "IX_Showtime_RoomId")
                    .IsUnique();

                entity.Property(e => e.ShowTime1).HasColumnName("ShowTime");

                entity.HasOne(d => d.Room)
                    .WithOne(p => p.Showtime)
                    .HasForeignKey<Showtime>(d => d.RoomId);
            });

            modelBuilder.Entity<Ticket>(entity =>
            {
                entity.ToTable("Ticket");

                entity.HasIndex(e => e.MemberId, "IX_Ticket_MemberId");

                entity.HasIndex(e => e.MovieId, "IX_Ticket_MovieId");

                entity.HasIndex(e => e.RoomId, "IX_Ticket_RoomId");

                entity.Property(e => e.MemberId).IsRequired();

                entity.HasOne(d => d.Member)
                    .WithMany(p => p.Tickets)
                    .HasForeignKey(d => d.MemberId);

                entity.HasOne(d => d.Movie)
                    .WithMany(p => p.Tickets)
                    .HasForeignKey(d => d.MovieId);

                entity.HasOne(d => d.Room)
                    .WithMany(p => p.Tickets)
                    .HasForeignKey(d => d.RoomId);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
