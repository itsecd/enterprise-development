using FitnesClub.Api.Contracts.Bookings;

namespace FitnesClub.Api.Services;

/// <summary>
/// Сервис валидации бизнес-правил бронирования тренировок.
/// </summary>
public interface IBookingValidationService
{
	/// <summary>
	/// Проверяет существование тренера и клиента, активность абонемента и отсутствие наложений в расписании.
	/// </summary>
	/// <param name="dto">Данные записи.</param>
	/// <param name="currentBookingId">ID текущей записи (при обновлении).</param>
	/// <returns>Текст ошибки валидации или null, если проверка пройдена.</returns>
	string? Validate(BookingCreateUpdateDto dto, Guid? currentBookingId = null);
}