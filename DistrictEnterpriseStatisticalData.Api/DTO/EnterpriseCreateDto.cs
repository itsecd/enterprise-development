using System.Text.Json.Serialization;

namespace DistrictEnterpriseStatisticalData.Api.DTO;

public class EnterpriseCreateDto
{
    /// <summary>
    ///     Тип отрасли
    /// </summary>
    [JsonPropertyName("enterprise_type_id")]
    public required int Type { get; set; }

    /// <summary>
    ///     Наименование
    /// </summary>
    [JsonPropertyName("name")]
    public required string Name { get; set; }

    /// <summary>
    ///     Адрес
    /// </summary>
    [JsonPropertyName("address")]
    public required string Address { get; set; }

    /// <summary>
    ///     Телефон
    /// </summary>
    [JsonPropertyName("phone_number")]
    public required string PhoneNumber { get; set; }

    /// <summary>
    ///     Форма собственности
    /// </summary>
    [JsonPropertyName("form_of_ownership_id")]
    public required int FormId { get; set; }

    /// <summary>
    ///     Количество работающих
    /// </summary>
    [JsonPropertyName("employees_number")]
    public required int EmployeesNumber { get; set; }

    /// <summary>
    ///     Общая площадь
    /// </summary>
    [JsonPropertyName("total_area")]
    public required int TotalArea { get; set; }
}