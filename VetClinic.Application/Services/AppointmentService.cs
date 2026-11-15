using Microsoft.Extensions.Logging;
using VetClinic.Application.Contracts.Appointments;
using VetClinic.Domain.Entities;
using VetClinic.Domain.Interfaces;

namespace VetClinic.Application.Services;

/// <summary>
/// Прикладной сервис для управления журналом записи на приемы.
/// Проверяет доступность питомца, врача и услуги, а также предоставляет статистику визитов.
/// </summary>
/// <param name="appointmentRepository">Специализированный репозиторий для работы с записями на прием.</param>
/// <param name="petRepository">Репозиторий питомцев для проверки существования пациента.</param>
/// <param name="vetRepository">Репозиторий врачей для проверки существования специалиста.</param>
/// <param name="serviceRepository">Репозиторий услуг для получения актуальной стоимости и названия процедуры.</param>
/// <param name="logger">Логгер</param>
public class AppointmentAppService(
    IAppointmentRepository appointmentRepository,
    IRepository<Pet> petRepository,
    IRepository<Vet> vetRepository,
    IRepository<Service> serviceRepository,
    ILogger<AppointmentAppService> logger) : IAppointmentAppService
{
    ///<inheritdoc/>
    public async Task<List<AppointmentDto>> GetAll()
    {
        logger.LogInformation("Запрос: Выгрузка полного журнала записей на приемы");
        var list = await appointmentRepository.GetAll();
        logger.LogInformation("Из репозитория извлечено {Count} приемов. Начинается маппинг в DTO.", list.Count);
        return list.Select(x => new AppointmentDto(
            x.Id, x.DateTime, x.Diagnosis, x.Pet.Name, x.Vet.FullName,
            x.Service.Title, x.Service.Price, x.RoomNumber, x.RepeatVisitIndicator.ToString())).ToList();
    }

    ///<inheritdoc/>
    public async Task<AppointmentDto?> Get(int id)
    {
        logger.LogInformation("Запрос: Поиск записи приема с Id {Id}", id);
        var x = await appointmentRepository.Get(id);
        if (x == null)
        {
            logger.LogWarning("Запись приема с Id {Id} отсутствует в журнале клиники.", id);
            return null;
        }
        return new AppointmentDto(
            x.Id, x.DateTime, x.Diagnosis, x.Pet.Name, x.Vet.FullName,
            x.Service.Title, x.Service.Price, x.RoomNumber, x.RepeatVisitIndicator.ToString());
    }

    ///<inheritdoc/>
    public async Task<AppointmentDto> Create(AppointmentCreateUpdateDto dto)
    {
        logger.LogInformation("Запрос: Оформление нового приема для питомца с Id {PetId}", dto.PetId);

        var pet = await petRepository.Get(dto.PetId) ?? throw new Exception("Питомец не найден");
        var vet = await vetRepository.Get(dto.VetId) ?? throw new Exception("Врач не найден");
        var service = await serviceRepository.Get(dto.ServiceId) ?? throw new Exception("Услуга не найдена");

        var entity = new Appointment
        {
            DateTime = dto.DateTime,
            Diagnosis = dto.Diagnosis,
            RoomNumber = dto.RoomNumber,
            RepeatVisitIndicator = dto.RepeatVisitIndicator,
            PetId = dto.PetId,
            Pet = pet,
            VetId = dto.VetId,
            Vet = vet,
            ServiceId = dto.ServiceId,
            Service = service
        };

        await appointmentRepository.Create(entity);
        logger.LogInformation("Запись приема успешно создана в кабинете №{Room} с Id {Id}", entity.RoomNumber, entity.Id);
        return new AppointmentDto(
            entity.Id, entity.DateTime, entity.Diagnosis, pet.Name, vet.FullName,
            service.Title, service.Price, entity.RoomNumber, entity.RepeatVisitIndicator.ToString());
    }

    ///<inheritdoc/>
    public async Task<AppointmentDto> Update(AppointmentCreateUpdateDto dto, int id)
    {
        logger.LogInformation("Запрос: Изменение параметров приема с Id {Id}", id);

        var pet = await petRepository.Get(dto.PetId) ?? throw new Exception("Питомец не найден");
        var vet = await vetRepository.Get(dto.VetId) ?? throw new Exception("Врач не найден");
        var service = await serviceRepository.Get(dto.ServiceId) ?? throw new Exception("Услуга не найдена");

        var entity = new Appointment
        {
            Id = id,
            DateTime = dto.DateTime,
            Diagnosis = dto.Diagnosis,
            RoomNumber = dto.RoomNumber,
            RepeatVisitIndicator = dto.RepeatVisitIndicator,
            PetId = dto.PetId,
            Pet = pet,
            VetId = dto.VetId,
            Vet = vet,
            ServiceId = dto.ServiceId,
            Service = service
        };

        await appointmentRepository.Update(entity);
        logger.LogInformation("Запись приема с Id {Id} успешно обновлена в базе данных.", id);
        return new AppointmentDto(
            entity.Id, entity.DateTime, entity.Diagnosis, pet.Name, vet.FullName,
            service.Title, service.Price, entity.RoomNumber, entity.RepeatVisitIndicator.ToString());
    }

    ///<inheritdoc/>
    public async Task<bool> Delete(int id)
    {
        logger.LogInformation("Запрос: Отмена/удаление приема с Id {Id}", id);
        var exists = await appointmentRepository.Get(id);
        if (exists == null)
        {
            logger.LogWarning("Отмена удаления: Запись приема с Id {Id} не найдена в расписании.", id);
            return false;
        }

        await appointmentRepository.Delete(id);
        logger.LogInformation("Запись приема с Id {Id} успешно удалена из системы.", id);
        return true;
    }

    ///<inheritdoc/>
    public async Task<List<AppointmentDto>> GetAppointmentsByPetId(int petId)
    {
        logger.LogInformation("Аналитический запрос: Выгрузка истории приемов для питомца с Id {PetId}", petId);
        var list = await appointmentRepository.GetAppointmentsByPetId(petId);
        logger.LogInformation("Сбор истории завершен: для питомца с Id {PetId} найдено {Count} записей.", petId, list.Count);
        return list.Select(x => new AppointmentDto(
            x.Id, x.DateTime, x.Diagnosis, x.Pet.Name, x.Vet.FullName,
            x.Service.Title, x.Service.Price, x.RoomNumber, x.RepeatVisitIndicator.ToString())).ToList();
    }
}
