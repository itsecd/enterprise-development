using FitnessClub.Domain.Interfaces;
using FitnessClub.Domain.Entities;
using FitnessClub.Contracts.Interfaces;
using FitnessClub.Contracts.Dtos.Edit;
using FitnessClub.Contracts.Dtos.Read;
using FitnessClub.Application.Mapping;
using FitnessClub.Application.Exceptions;
using Microsoft.Extensions.Logging;

namespace FitnessClub.Application.Services;

/// <summary>
/// Управление залами
/// </summary>
public class HallService(
    IRepository<Hall> hallRepository, 
    IRepository<Booking> bookingsRepository,
    ILogger<HallService> logger) : ICrudService<HallDto, HallEditDto>
{
    /// <summary>
    /// Получить список залов
    /// </summary>
    public async Task<IReadOnlyList<HallDto>> GetAllAsync()
    {
        var halls = await hallRepository.GetAllAsync();
        return [.. halls.Select(h => h.ToDto())];
    }
    /// <summary>
    /// Получить зал по идентификатору
    /// </summary>
    public async Task<HallDto?> GetByIdAsync(int id)
    {
        var hall = await hallRepository.GetByIdAsync(id);
        return hall?.ToDto();
    }
    /// <summary>
    /// Добавить зал
    /// </summary>
    public async Task<HallDto> AddAsync(HallEditDto editDto)
    {
        var hall = await hallRepository.AddAsync(editDto.ToEntity());
        if (logger.IsEnabled(LogLevel.Information))
            logger.LogInformation("Создан зал {HallId}: {HallName}", hall.Id, hall.Name);
        return hall.ToDto();
    }
    /// <summary>
    /// Обновить данные зала
    /// </summary>
    public async Task<HallDto?> UpdateAsync(int id, HallEditDto editDto)
    {
        var hall = await hallRepository.GetByIdAsync(id);
        if (hall is null)
        {
            logger.LogWarning("Зал {HallId} не найден для обновления", id);
            return null;
        }
        hall.UpdateEntity(editDto);
        await hallRepository.UpdateAsync(hall);
        if (logger.IsEnabled(LogLevel.Information))
            logger.LogInformation("Обновлен зал {HallId}: {HallName}", hall.Id, hall.Name);
        return hall.ToDto();
    }
    /// <summary>
    /// Удалить зал при отсутствии бронирований
    /// </summary>
    public async Task<bool> DeleteAsync(int id)
    {
        var allBookings = await bookingsRepository.GetAllAsync();
        if (allBookings.Any(b => b.Hall.Id == id))
            throw new ConflictException("Невозможно удалить зал, так как существуют бронирования для него.");
        var deleted = await hallRepository.DeleteAsync(id);
        if (deleted)
        {    
            if (logger.IsEnabled(LogLevel.Information))
                logger.LogInformation("Удален зал {HallId}", id);
        }
        else
            logger.LogWarning("Зал {HallId} не найден для удаления", id);
        return deleted;
    }
}
