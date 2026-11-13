namespace VeterinaryClinic.Application.Contracts.Pets;

/// <summary>
/// Данные для создания или обновления питомца
/// </summary>
/// <param name="Name">Название</param>
/// <param name="BreedId">Идентификатор породы</param>
/// <param name="BirthDate">Дата рождения</param>
/// <param name="Weight">Вес питомца</param>
/// <param name="OwnerId">Идентификатор владельца, если назначен</param>
public record SavePetRequest(string Name, int BreedId, DateOnly BirthDate, double Weight, int? OwnerId);
