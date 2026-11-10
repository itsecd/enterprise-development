using System.ComponentModel.DataAnnotations;

namespace TransportCompany.Contracts.Dto;

/// <summary>
/// DTO для создания или редактирования транспортного средства
/// </summary>
public class VehicleEditDto
{
    /// <summary>
    /// Государственный регистрационный номер
    /// </summary>
    /// <example>А123БВ77</example>
    [Required(ErrorMessage = "Гос. номер обязателен")]
    [MaxLength(9, ErrorMessage = "Гос. номер не может превышать 9 символов")]
    public required string LicensePlate { get; init; }

    /// <summary>
    /// Грузоподъёмность в тоннах
    /// </summary>
    [Range(0.1, 100, ErrorMessage = "Грузоподъемность должна быть от 0.1 до 100 тонн")]
    public required double LoadCapacity { get; init; }

    /// <summary>
    /// Идентификатор модели
    /// </summary>
    [Required(ErrorMessage = "Модель ТС обязательна")]
    public required int ModelId { get; init; }
}