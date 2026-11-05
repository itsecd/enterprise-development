namespace VetClinic.Contracts.Appointments;

public interface IAppointmentService
{
    public Task<IReadOnlyList<AppointmentDto>> GetAllAsync(CancellationToken cancellationToken = default);

    public Task<AppointmentDto> GetByIdAsync(int id, CancellationToken cancellationToken = default);

    public Task<IReadOnlyList<AppointmentDto>> GetByVeterinarianAsync(int veterinarianId, CancellationToken cancellationToken = default);

    public Task<IReadOnlyList<AppointmentDto>> GetByPetAsync(int petId, CancellationToken cancellationToken = default);

    public Task<IReadOnlyList<AppointmentDto>> GetCurrentMonthAsync(string roomNumber, CancellationToken cancellationToken = default);

    public Task<FollowUpCountDto> CountFollowUpsAsync(int breedId, CancellationToken cancellationToken = default);

    public Task<AppointmentDto> CreateAsync(AppointmentEditDto dto, CancellationToken cancellationToken = default);

    public Task<AppointmentDto> UpdateAsync(int id, AppointmentEditDto dto, CancellationToken cancellationToken = default);

    public Task DeleteAsync(int id, CancellationToken cancellationToken = default);
}
