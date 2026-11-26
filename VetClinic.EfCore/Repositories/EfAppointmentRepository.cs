using Microsoft.EntityFrameworkCore;
using VetClinic.Domain.Entities;
using VetClinic.Domain.Interfaces;

namespace VetClinic.EfCore.Repositories;

/// <summary>
/// EF Core реализация специализированного репозитория для работы с журналом приемов
/// </summary>
/// <param name="context">Контекст базы данных ветеринарной клиники</param>
public class EfAppointmentRepository(VetClinicDbContext context) : IAppointmentRepository
{
    /// <inheritdoc />
    public Task<List<Appointment>> GetAll() =>
        context.Appointments
            .Include(x => x.Pet).ThenInclude(p => p.Breed)
            .Include(x => x.Pet).ThenInclude(p => p.Owner)
            .Include(x => x.Vet).ThenInclude(v => v.Specialization)
            .Include(x => x.Service)
            .ToListAsync();

    /// <inheritdoc />
    public Task<Appointment?> Get(int id) =>
        context.Appointments
            .Include(x => x.Pet).ThenInclude(p => p.Breed)
            .Include(x => x.Pet).ThenInclude(p => p.Owner)
            .Include(x => x.Vet).ThenInclude(v => v.Specialization)
            .Include(x => x.Service)
            .FirstOrDefaultAsync(x => x.Id == id);

    /// <inheritdoc />
    public async Task Create(Appointment entity)
    {
        entity.Pet = null!;
        entity.Vet = null!;
        entity.Service = null!;
        await context.Appointments.AddAsync(entity);
        await context.SaveChangesAsync();
    }

    /// <inheritdoc />
    public async Task Update(Appointment entity)
    {
        var existing = await context.Appointments.FindAsync(entity.Id)
            ?? throw new KeyNotFoundException($"Прием с ID {entity.Id} не найден.");

        existing.DateTime = entity.DateTime;
        existing.Diagnosis = entity.Diagnosis;
        existing.RoomNumber = entity.RoomNumber;
        existing.RepeatVisitIndicator = entity.RepeatVisitIndicator;
        existing.PetId = entity.PetId;
        existing.VetId = entity.VetId;
        existing.ServiceId = entity.ServiceId;

        await context.SaveChangesAsync();
    }

    /// <inheritdoc />
    public async Task Delete(int id)
    {
        var entity = await context.Appointments.FindAsync(id);
        if (entity is not null)
        {
            context.Appointments.Remove(entity);
            await context.SaveChangesAsync();
        }
    }

    /// <inheritdoc />
    public Task<List<Appointment>> GetAppointmentsByPetId(int petId) =>
        context.Appointments
            .Include(x => x.Pet)
            .Include(x => x.Vet)
            .Include(x => x.Service)
            .Where(a => a.PetId == petId)
            .ToListAsync();
}
