namespace VetClinic.Application.Contracts.Pets;
/// <summary>
/// DTO исходящих данных для отображения данных о питомце
/// </summary>
/// <param name="Id">Id питомца</param>
/// <param name="Name">Кличка</param>
/// <param name="SpeciesTitle">Название вида</param>
/// <param name="BreedTitle">Название породы</param>
/// <param name="OwnerName">ФИО владельца</param>
/// <param name="Weight">Вес животного</param>
public record PetDto(int Id, string Name, string SpeciesTitle, string BreedTitle, string OwnerName, decimal? Weight);
