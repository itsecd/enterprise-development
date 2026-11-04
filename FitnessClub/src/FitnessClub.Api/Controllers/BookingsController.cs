using FitnessClub.Contracts.Dtos.Edit;
using FitnessClub.Contracts.Dtos.Read;
using FitnessClub.Contracts.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FitnessClub.Api.Controllers;

/// <summary>
/// Управление бронями
/// </summary>
public class BookingsController(ICrudService<BookingDto, BookingEditDto> service) : CrudControllerBase<BookingDto, BookingEditDto>(service);