using Microsoft.EntityFrameworkCore;
using VetClinic.Domain.Models;
using VetClinic.Domain.Seeds;

namespace VetClinic.Infrastructure.SqlServer;

/// <summary>
/// Контекст базы данных ветеринарной клиники
/// </summary>
/// <param name="options">Параметры контекста</param>
public class VetClinicDbContext(DbContextOptions<VetClinicDbContext> options) : DbContext(options)
{
    /// <summary>
    /// Владельцы питомцев
    /// </summary>
    public DbSet<Owner> Owners => Set<Owner>();

    /// <summary>
    /// Питомцы
    /// </summary>
    public DbSet<Pet> Pets => Set<Pet>();

    /// <summary>
    /// Породы
    /// </summary>
    public DbSet<Breed> Breeds => Set<Breed>();

    /// <summary>
    /// Ветеринары
    /// </summary>
    public DbSet<Vet> Vets => Set<Vet>();

    /// <summary>
    /// Специализации врачей
    /// </summary>
    public DbSet<Specialization> Specializations => Set<Specialization>();

    /// <summary>
    /// Записи на прием
    /// </summary>
    public DbSet<Appointment> Appointments => Set<Appointment>();

    /// <summary>
    /// Пользователи API
    /// </summary>
    public DbSet<User> Users => Set<User>();

    /// <inheritdoc/>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(VetClinicDbContext).Assembly);
        SeedData(modelBuilder);
    }

    /// <summary>
    /// Начальные данные из Domain, связи передаются через внешние ключи
    /// </summary>
    /// <param name="modelBuilder">Построитель модели</param>
    private static void SeedData(ModelBuilder modelBuilder)
    {
        var seed = new VetClinicSeed();

        modelBuilder.Entity<Owner>().HasData(seed.Owners.Select(o => new
        {
            o.Id,
            o.FullName,
            o.Address,
            o.Phone
        }));

        modelBuilder.Entity<Breed>().HasData(seed.Breeds.Select(b => new
        {
            b.Id,
            b.Name,
            b.Species
        }));

        modelBuilder.Entity<Specialization>().HasData(seed.Specializations.Select(s => new
        {
            s.Id,
            s.Name,
            s.FocusSpecies
        }));

        modelBuilder.Entity<Vet>().HasData(seed.Vets.Select(v => new
        {
            v.Id,
            v.PassportNumber,
            v.FullName,
            v.BirthYear,
            v.WorkExperienceYears,
            SpecializationId = v.Specialization?.Id
        }));

        modelBuilder.Entity<Pet>().HasData(seed.Pets.Select(p => new
        {
            p.Id,
            p.Nickname,
            p.Species,
            p.BirthDate,
            p.Weight,
            BreedId = p.Breed?.Id,
            OwnerId = p.Owner!.Id
        }));

        modelBuilder.Entity<Appointment>().HasData(seed.Appointments.Select(a => new
        {
            a.Id,
            a.DateTime,
            a.RoomNumber,
            a.IsRepeat,
            PetId = a.Pet!.Id,
            VetId = a.Vet!.Id
        }));
    }
}
