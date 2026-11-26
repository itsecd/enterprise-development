namespace VetClinic.Application.Contracts.Pets;

/// <summary>
/// Интерфейс прикладного сервиса для ведения картотеки питомцев клиники
/// </summary>
public interface IPetAppService : IApplicationService<PetDto, PetCreateUpdateDto, int>
{
    /// <summary>
    /// Возвращает список всех зарегистрированных питомцев, отсортированный по кличке в алфавитном порядке
    /// </summary>
    /// <returns>Отсортированный список DTO питомцев</returns>
    public Task<List<PetDto>> GetSortedByName();
}
