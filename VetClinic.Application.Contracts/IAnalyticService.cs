using VetClinic.Application.Contracts.Appointments;
using VetClinic.Application.Contracts.Owners;
using VetClinic.Application.Contracts.Pets;
using VetClinic.Application.Contracts.Vets;
using VetClinic.Domain.Shared.Enums;

namespace VetClinic.Application.Contracts;
/// <summary>
/// Интерфейс сервиса со сложными запросами
/// </summary>
public interface IAnalyticService
{
    /// <summary>
    /// Ищет врачей, специализирующихся на определённом виде животных
    /// </summary>
    /// <param name="species">Тип животных, на которых специализируется врач</param>
    /// <returns>Список врачей</returns>
    public Task<List<VetDto>> GetAllVetsBySpecies(Species species);

    /// <summary>
    /// Ищет питомцы, записанных к указанному врачу
    /// </summary>
    /// <param name="vetId">Интересуемый врач</param>
    /// <returns>Список питомцев, упорядоченных по кличке</returns>
    public Task<List<PetDto>> GetAllPetsByVetOrderedByName(int vetId);

    /// <summary>
    /// Находит количество повторных приемов для выбранной породы животного
    /// </summary>
    /// <param name="breedId">Интересуемая порода</param>
    /// <returns>Число повторных приёмов</returns>
    public Task<int> FindNumberOfSecondaryVisitForSpecificAnimalBreed(int breedId);

    /// <summary>
    /// Ищет владельцев, имеющих более одного питомца
    /// </summary>
    /// <returns>Список владельцев, упорядоченных по ФИО</returns>
    public Task<List<OwnerDto>> GetAllOwnersWithMultiplePets();

    /// <summary>
    /// Ищет приемы за текущий месяц, проходящие в выбранном кабинете
    /// </summary>
    /// <param name="roomNumber">Интересуемый кабинет</param>
    /// <returns>Список записей на приём</returns>
    public Task<List<AppointmentDto>> GetAllAppointmentsForCurrentMonthByRoom(int roomNumber, int Month, int Year);
}
