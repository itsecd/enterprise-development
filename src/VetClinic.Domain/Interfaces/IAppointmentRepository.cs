using VetClinic.Domain.Entities;

namespace VetClinic.Domain.Interfaces;

/// <summary>
/// Хранилище записей на приём
/// </summary>
public interface IAppointmentRepository
{
    public Task<IReadOnlyList<Appointment>> GetAllAsync(CancellationToken cancellationToken = default);

    public Task<Appointment?> FindByIdAsync(int id, CancellationToken cancellationToken = default);

    public Task<IReadOnlyList<Appointment>> GetByVeterinarianAsync(int veterinarianId, CancellationToken cancellationToken = default);

    public Task<IReadOnlyList<Appointment>> GetByPetAsync(int petId, CancellationToken cancellationToken = default);

    public Task<IReadOnlyList<Appointment>> GetByRoomAsync(string roomNumber, DateTime from, DateTime to, CancellationToken cancellationToken = default);

    public Task<int> CountFollowUpsAsync(int breedId, CancellationToken cancellationToken = default);

    public Task<Appointment> AddAsync(
        int petId,
        int veterinarianId,
        DateTime visitDateTime,
        string roomNumber,
        bool isFollowUp,
        CancellationToken cancellationToken = default);

    public Task<Appointment> UpdateAsync(
        int id,
        int petId,
        int veterinarianId,
        DateTime visitDateTime,
        string roomNumber,
        bool isFollowUp,
        CancellationToken cancellationToken = default);

    public Task RemoveAsync(int id, CancellationToken cancellationToken = default);
}
