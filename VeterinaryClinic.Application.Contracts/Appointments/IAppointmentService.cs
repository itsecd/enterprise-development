using VeterinaryClinic.Application.Contracts.Appointments;
using VeterinaryClinic.Application.Contracts.Breeds;
using VeterinaryClinic.Application.Contracts.Owners;
using VeterinaryClinic.Application.Contracts.Pets;
using VeterinaryClinic.Application.Contracts.Specializations;
using VeterinaryClinic.Application.Contracts.Veterinarians;
namespace VeterinaryClinic.Application.Contracts.Appointments;

/// <summary>
/// Операции сервиса приёмов
/// </summary>
public interface IAppointmentService
{
    /// <summary>
    /// Возвращает все записи на приём.
    /// </summary>
    public Task<IReadOnlyList<AppointmentDto>> AppointmentsAsync(CancellationToken cancellationToken = default);
    /// <summary>
    /// Находит приём по идентификатору.
    /// </summary>
    public Task<AppointmentDto?> GetAsync(int id, CancellationToken cancellationToken = default);
    /// <summary>
    /// Создаёт приём на основе переданных данных.
    /// </summary>
    public Task<AppointmentDto> CreateAsync(SaveAppointmentRequest request, CancellationToken cancellationToken = default);
    /// <summary>
    /// Обновляет приём по идентификатору.
    /// </summary>
    public Task<AppointmentDto> UpdateAsync(int id, SaveAppointmentRequest request, CancellationToken cancellationToken = default);
    /// <summary>
    /// Удаляет приём по идентификатору.
    /// </summary>
    public Task DeleteAppointmentAsync(int id, CancellationToken cancellationToken = default);
    /// <summary>
    /// Возвращает записи на приём за указанный месяц и для заданного кабинета.
    /// </summary>
    public Task<IReadOnlyList<AppointmentDto>> AppointmentsThisMonthAsync(int room, DateTime today, CancellationToken cancellationToken = default);
}
