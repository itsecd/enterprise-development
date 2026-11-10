using VetClinic.Domain.Entities;
using VetClinic.Domain.Enums;

namespace VetClinic.Domain.Interfaces;

/// <summary>
/// Сервис выполнения аналитических запросов по данным ветеринарной клиники
/// </summary>
public interface IVetClinicService
{
    /// <summary>
    /// Получить список всех ветеринаров, специализирующихся на выбранном виде животных
    /// </summary>
    /// <param name="species">Биологический вид животного</param>
    /// <returns>Коллекция ветеринаров с соответствующей специализацией</returns>
    public Task<IEnumerable<Veterinarian>> GetVeterinariansBySpeciesAsync(AnimalSpecies species);

    /// <summary>
    /// Получить список всех питомцев, записанных на прием к врачу, упорядоченных по кличке
    /// </summary>
    /// <param name="doctorId">Идентификатор ветеринара</param>
    /// <returns>Коллекция уникальных питомцев, отсортированная по кличке</returns>
    public Task<IEnumerable<Pet>> GetPetsByDoctorOrderedByNameAsync(int doctorId);

    /// <summary>
    /// Получить количество повторных приемов для выбранной породы животного
    /// </summary>
    /// <param name="breedId">Идентификатор породы</param>
    /// <returns>Количество повторных приемов</returns>
    public Task<int> GetFollowUpAppointmentsCountByBreedAsync(int breedId);

    /// <summary>
    /// Получить список владельцев, имеющих более одного питомца, упорядоченных по ФИО
    /// </summary>
    /// <returns>Коллекция владельцев с несколькими питомцами</returns>
    public Task<IEnumerable<Owner>> GetOwnersWithMultiplePetsOrderedByNameAsync();

    /// <summary>
    /// Получить список приемов за указанный месяц, проходящих в выбранном кабинете
    /// </summary>
    /// <param name="roomNumber">Номер кабинета</param>
    /// <param name="currentDate">Дата, определяющая расчетный месяц и год</param>
    /// <returns>Коллекция записей на прием</returns>
    public Task<IEnumerable<Appointment>> GetCurrentMonthAppointmentsByRoomAsync(int roomNumber, DateTime currentDate);
}