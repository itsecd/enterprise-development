using HotelSystem.Domain.Data;
using HotelSystem.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelSystem.Infrastructure.EfCore.Data;

/// <summary>
/// Определяет наборы сущностей, их связи и начальные данные
/// </summary>
public class HotelDbContext(DbContextOptions<HotelDbContext> options)
    : DbContext(options)
{
    /// <summary>
    /// Набор клиентов гостиницы
    /// </summary>
    public DbSet<Client> Clients => Set<Client>();

    /// <summary>
    /// Набор типов номеров гостиницы
    /// </summary>
    public DbSet<RoomType> RoomTypes => Set<RoomType>();

    /// <summary>
    /// Набор номеров гостиницы
    /// </summary>
    public DbSet<Room> Rooms => Set<Room>();

    /// <summary>
    /// Набор бронирований гостиницы
    /// </summary>
    public DbSet<Booking> Bookings => Set<Booking>();

    /// <summary>
    /// Настраивает сущности, связи между ними и начальное заполнение базы данных
    /// </summary>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Client>(entity =>
        {
            entity.HasKey(client => client.Id);

            entity.Property(client => client.PassportNumber)
                .IsRequired();

            entity.Property(client => client.LastName)
                .IsRequired();

            entity.Property(client => client.FirstName)
                .IsRequired();

            entity.Property(client => client.Citizenship)
                .IsRequired();

            entity.Property(client => client.BirthDate)
                .HasColumnType("timestamp without time zone");

            entity.HasData(HotelTestData.Clients);

        });

        modelBuilder.Entity<RoomType>(entity =>
        {
            entity.HasKey(roomType => roomType.Id);

            entity.HasData(HotelTestData.RoomTypes);
        });

        modelBuilder.Entity<Room>(entity =>
        {
            entity.HasKey(room => room.Id);

            entity.HasOne(room => room.RoomType)
                .WithMany()
                .HasForeignKey("RoomTypeId")
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasData(
                HotelTestData.Rooms.Select(room => new
                {
                    room.Id,
                    room.RoomNumber,
                    room.Floor,
                    room.HasBalcony,
                    RoomTypeId = room.RoomType.Id
                }));
        });

        modelBuilder.Entity<Booking>(entity =>
        {
            entity.HasKey(booking => booking.Id);

            entity.HasOne(booking => booking.Client)
                .WithMany()
                .HasForeignKey("ClientId")
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(booking => booking.Room)
                .WithMany()
                .HasForeignKey("RoomId")
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            entity.Ignore(booking => booking.TotalCost);

            entity.Property(booking => booking.CheckInDate)
                .HasColumnType("timestamp without time zone");

            entity.HasData(
                HotelTestData.Bookings.Select(booking => new
                {
                    booking.Id,
                    ClientId = booking.Client.Id,
                    RoomId = booking.Room.Id,
                    booking.CheckInDate,
                    booking.NumberOfDays
                }));
        });


    }
}