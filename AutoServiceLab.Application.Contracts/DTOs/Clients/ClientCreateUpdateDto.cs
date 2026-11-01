namespace AutoServiceLab.Application.Contracts.DTOs.Clients;

/// <summary>
/// DTO для создания или обновления клиента
/// </summary>
/// <param name="FullName">ФИО клиента</param>
/// <param name="Phone">Номер телефона клиента</param>
public record ClientCreateUpdateDto(
    string FullName,
    string Phone
);
