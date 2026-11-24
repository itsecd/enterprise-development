using System.ComponentModel.DataAnnotations;
using Museum.Domain.Enums;

namespace Museum.Contracts;

/// <summary>Данные для создания или изменения выставки.</summary>
public class ExhibitionRequest
{
    /// <summary>Название выставки.</summary>
    [Required]
    public required string Name { get; set; }

    /// <summary>Тематика выставки.</summary>
    public ExhibitionTheme Theme { get; set; }

    /// <summary>Номер зала.</summary>
    [Range(1, int.MaxValue)]
    public int HallNumber { get; set; }

    /// <summary>Дата начала выставки.</summary>
    public DateOnly StartDate { get; set; }

    /// <summary>Дата окончания выставки.</summary>
    public DateOnly EndDate { get; set; }
}
