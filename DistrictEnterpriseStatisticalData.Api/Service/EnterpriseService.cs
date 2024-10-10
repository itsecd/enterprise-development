using AutoMapper;
using DistrictEnterpriseStatisticalData.Api.DTO;
using DistrictEnterpriseStatisticalData.Domain.Entity;
using DistrictEnterpriseStatisticalData.Domain.Repository;

namespace DistrictEnterpriseStatisticalData.Api.Service;

/// <summary>
/// Сервис для предприятий
/// </summary>
public class EnterpriseService(EnterpriseRepository repository, IMapper mapper)
{
    /// <summary>
    /// Получение всех предприятий
    /// </summary>
    public IEnumerable<EnterpriseDto> GetAll()
    {
        return repository.GetAll().Select(mapper.Map<EnterpriseDto>);
    }

    /// <summary>
    /// Получение предприятия по регистрационному номеру
    /// </summary>
    public EnterpriseDto? GetByRegistrationNumber(int id)
    {
        return mapper.Map<EnterpriseDto>(repository.GetById(id));
    }

    /// <summary>
    /// Создание предприятия
    /// </summary>
    public EnterpriseDto Create(EnterpriseCreateDto enterprise)
    {
        return mapper.Map<EnterpriseDto>(repository.Create(mapper.Map<Enterprise>(enterprise)));
    }

    /// <summary>
    /// Обновление информации о предприятии
    /// </summary>
    public EnterpriseDto Update(EnterpriseDto enterprise)
    {
        return mapper.Map<EnterpriseDto>(repository.Update(mapper.Map<Enterprise>(enterprise)));
    }

    /// <summary>
    /// Удаление предприятия
    /// </summary>
    public void Delete(int id)
    {
        var enterprise = repository.GetById(id);
        if (enterprise != null)
            repository.Delete(enterprise);
    }

    /// <summary>
    /// Получение топ 5 предприятий по количеству поставок
    /// </summary>
    public IEnumerable<EnterpriseDto> FiveMostSuppliedEnterprises()
    {
        return repository.FiveMostSuppliedEnterprises().Select(mapper.Map<EnterpriseDto>);
    }
}