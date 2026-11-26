using FitnessClub.Contracts.Dtos.Edit;
using FitnessClub.Contracts.Dtos.Read;
using FitnessClub.Contracts.Interfaces;

namespace FitnessClub.Api.Controllers;

/// <summary>
/// Управление бронями
/// </summary>
/// <param name="service">Сервис управления бронированиями</param>
public class BookingsController(ICrudService<BookingDto, BookingEditDto> service) : CrudControllerBase<BookingDto, BookingEditDto>(service);