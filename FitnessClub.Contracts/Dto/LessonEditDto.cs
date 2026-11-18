using System.ComponentModel.DataAnnotations;

namespace FitnessClub.Contracts.Dto;

/// <summary>
/// Занятие (edit-модель)
/// </summary>
public class LessonEditDto
{
    /// <summary>
    /// Идентификатор клиента
    /// </summary>
    [Required(ErrorMessage = "Идентификатор клиента обязателен")]
    [Range(0, int.MaxValue, ErrorMessage = "Укажите идентификатор клиента")]
    public int? ClientId { get; set; }

    /// <summary>
    /// Идентификатор тренера
    /// </summary>
    [Required(ErrorMessage = "Идентификатор тренера обязателен")]
    [Range(0, int.MaxValue, ErrorMessage = "Укажите идентификатор тренера")]
    public int? TrainerId { get; set; }

    /// <summary>
    /// Дата и время начала
    /// </summary>
    [Required(ErrorMessage = "Дата и время начала обязательны")]
    public DateTime DateStart { get; set; }

    /// <summary>
    /// Дата и время окончания
    /// </summary>
    public DateTime? DateEnd { get; set; }

    /// <summary>
    /// Название зала
    /// </summary>
    [MaxLength(100, ErrorMessage = "Название зала не должно превышать 100 символов")]
    public string? Hall { get; set; }

    /// <summary>
    /// Пробное занятие или нет
    /// </summary>
    public bool IsTrial { get; set; }
}