using Hotel.Domain.Data;
using Hotel.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Hotel.Infrastructure;

public class HotelDbContext(DbContextOptions<HotelDbContext> options) : DbContext(options)
{
    public DbSet<Client> Clients => Set<Client>();
    public DbSet<Room> Rooms => Set<Room>();
    public DbSet<RoomType> RoomTypes => Set<RoomType>();
    public DbSet<Booking> Bookings => Set<Booking>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        var bookings = modelBuilder.Entity<Booking>();
        bookings.HasOne(b => b.Client)
                .WithMany()
                .HasForeignKey("ClientId")
                .OnDelete(DeleteBehavior.Cascade);
        bookings.HasOne(b => b.Room)
                .WithMany()
                .HasForeignKey("RoomId")
                .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Room>()
                .HasOne(r => r.Type)
                .WithMany()
                .HasForeignKey("RoomTypeId")
                .OnDelete(DeleteBehavior.Cascade);

        bookings.Navigation(b => b.Client).AutoInclude();
        bookings.Navigation(b => b.Room).AutoInclude();
        modelBuilder.Entity<Room>().Navigation(r => r.Type).AutoInclude();

        bookings.HasIndex("RoomId", nameof(Booking.CheckInDate))
                .IsUnique()
                .HasDatabaseName("idx_Bookings_Room_CheckInDate");

        modelBuilder.Entity<Client>()
                .HasIndex(c => c.PassportNumber)
                .IsUnique();

        Seed(modelBuilder);
    }

    private static void Seed(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<RoomType>().HasData(TestData.RoomTypes);

        modelBuilder.Entity<Room>().HasData(
            TestData.Rooms.Select(room => new
            {
                room.Id,
                room.Number,
                room.Floor,
                room.HasBalcony,
                RoomTypeId = room.Type.Id
            })
        );

        modelBuilder.Entity<Client>().HasData(TestData.Clients);

        modelBuilder.Entity<Booking>().HasData(
            TestData.Bookings.Select(booking => new
            {
                booking.Id,
                booking.CheckInDate,
                booking.DaysOfStay,
                ClientId = booking.Client.Id,
                RoomId = booking.Room.Id
            })
        );
    }
}