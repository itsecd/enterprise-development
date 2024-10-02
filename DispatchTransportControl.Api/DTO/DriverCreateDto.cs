using System.Text.Json.Serialization;

namespace DispatchTransportControl.Api.DTO;

/// <summary>
///     DTO для создания водителя
/// </summary>
public class DriverCreateDto
{
    /// <summary>
    ///     Имя
    /// </summary>
    [JsonPropertyName("name")]
    public required string Name { get; set; }

    /// <summary>
    ///     Фамилия
    /// </summary>
    [JsonPropertyName("surname")]
    public required string Surname { get; set; }

    /// <summary>
    ///     Отчество
    /// </summary>
    [JsonPropertyName("patronymic")]
    public required string Patronymic { get; set; }

    /// <summary>
    ///     Паспорт
    /// </summary>
    [JsonPropertyName("passport")]
    public required string Passport { get; set; }

    /// <summary>
    ///     Водительское удостоверение
    /// </summary>
    [JsonPropertyName("driver_license")]
    public required string DriverLicense { get; set; }

    /// <summary>
    ///     Адрес
    /// </summary>
    [JsonPropertyName("address")]
    public required string Address { get; set; }

    /// <summary>
    ///     Телефон
    /// </summary>
    [JsonPropertyName("phone")]
    public required string Phone { get; set; }
}